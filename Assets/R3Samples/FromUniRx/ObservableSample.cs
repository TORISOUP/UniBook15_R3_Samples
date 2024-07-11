using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class ObservableSample : MonoBehaviour
    {
        // Observableを公開するときは Observable<T> を使う
        public Observable<string> OnGameEvent => _eventSubject;

        // イベント発行の主体であるSubject
        private readonly Subject<string> _eventSubject = new();
        
        private void Start()
        {
            _eventSubject.OnNext("Game Start!");
        }

        private void OnDestroy()
        {
            _eventSubject.OnNext("Game End!");
        }
    }
}