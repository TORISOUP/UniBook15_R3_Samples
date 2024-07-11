using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class CreateObservableSample2 : MonoBehaviour
    {
        // 0~1000msのランダムな時間間隔で値を発行し続けるObservableを生成
        public Observable<int> CreateCountUpObservable()
        {
            return Observable.Create<int>(async (observer, ct) =>
            {
                var count = 0;
                while (!ct.IsCancellationRequested)
                {
                    observer.OnNext(count++);
                    var rand = Random.Range(0, 1000);
                    await UniTask.Delay(rand, cancellationToken: ct);
                }
            });
        }
    }
}