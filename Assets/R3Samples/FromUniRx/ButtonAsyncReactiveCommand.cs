using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine.UI;

namespace R3Samples.FromUniRx
{
    /// <summary>
    /// UnityEngine.UI.ButtonとReactivePropertyを組み合わせて制御するAsyncReactiveCommandの再現
    /// スレッドセーフ性などは考慮していない上、Dispose時の挙動も若干不安
    /// あくまで参考程度にしてもらって、そのままプロダクトで使うのは避けたほうが良い
    /// （使用にした場合に発生したトラブルの責任は負いません）
    /// </summary>
    public sealed class ButtonAsyncReactiveCommand : IDisposable
    {
        private readonly ReactiveProperty<bool> _gate;
        private readonly Func<CancellationToken, UniTask> _asyncAction;
        private bool _isDisposed;
        private readonly CancellationTokenSource _cts = new();

        public ButtonAsyncReactiveCommand(Button button,
            ReactiveProperty<bool> gate,
            Func<CancellationToken, UniTask> action)
        {
            _asyncAction = action;
            _gate = gate;
            _gate.SubscribeToInteractable(button).RegisterTo(_cts.Token);
        }

        public async UniTask ExecuteAsync(CancellationToken ct)
        {
            if (_isDisposed) return;
            if (!_gate.CurrentValue) return;
            if (_asyncAction == null) return;
            _gate.Value = false;

            try
            {
                var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct, _cts.Token);
                await _asyncAction(linkedCts.Token);
            }
            finally
            {
                _gate.Value = true;
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            _cts.Cancel();
            _cts.Dispose();
            _isDisposed = true;
        }
    }

    public static class GateAsyncReactiveCommandExt
    {
        public static void BindToOnClick(this Button button,
            ReactiveProperty<bool> gate,
            Func<CancellationToken, UniTask> action,
            CancellationToken ct)
        {
            var command = new ButtonAsyncReactiveCommand(button, gate, action);
            button.OnClickAsObservable()
                .SubscribeAwait(async (_, token)
                    => await command.ExecuteAsync(token))
                .RegisterTo(ct);
            command.RegisterTo(ct);
        }
    }
}