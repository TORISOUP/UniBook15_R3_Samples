using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace R3Samples.FromUniRx
{
    public sealed class AltAsyncReactiveCommandSample : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            // ボタンを押したら非同期処理を実行
            // 非同期処理が完了するまでボタンを無効化する
            _button.OnClickAsObservable()
                .SubscribeAwait(async (_, ct) =>
                {
                    try
                    {
                        _button.interactable = false;
                        await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: ct);
                    }
                    finally
                    {
                        _button.interactable = true;
                    }
                }, AwaitOperation.Drop)
                .RegisterTo(destroyCancellationToken);
        }
    }
}