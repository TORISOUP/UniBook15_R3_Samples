using R3;
using UnityEngine;
using UnityEngine.UI;

namespace R3Samples.FromUniRx
{
    public sealed class RegisterToSample : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            // UniRx同様にComponentに寿命を紐づける場合はAddToを使う
            _button
                .OnClickAsObservable()
                .Subscribe(_ => Debug.Log("Button Clicked!"))
                .AddTo(this);

            // CancellationTokenに紐づける場合はRegisterToを使う
            _button
                .OnClickAsObservable()
                .Subscribe(_ => Debug.Log("Button Clicked 2!"))
                .RegisterTo(destroyCancellationToken);
        }
    }
}