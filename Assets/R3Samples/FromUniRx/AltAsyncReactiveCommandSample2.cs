using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace R3Samples.FromUniRx
{
    public sealed class AltAsyncReactiveCommandSample2 : MonoBehaviour
    {
        [SerializeField] private Button _button1;
        [SerializeField] private Button _button2;
        [SerializeField] private Button _button3;

        private readonly SerializableReactiveProperty<bool> _gate = new(true);

        private void Start()
        {
            // どれかのボタンを押すと非同期処理が実行開始
            // それに連動してすべてのボタンが一時無効化される
            // 非同期処理が完了すると解除
            _button1.BindToOnClick(_gate,
                async ct => await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: ct),
                destroyCancellationToken);
            
            _button2.BindToOnClick(_gate,
                async ct => await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: ct),
                destroyCancellationToken);
            
            _button3.BindToOnClick(_gate,
                async ct => await UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: ct),
                destroyCancellationToken);
        }
    }
}