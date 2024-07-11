using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class FirstAsyncSample : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            // FirstAsyncはObservableの最初のメッセージの到着を待つ
            // ToUniTask(useFirstValue: true) とだいたい同じ意味となる
            var result = await Observable.Range(0, 10)
                .FirstAsync(destroyCancellationToken);
            
            Debug.Log(result);
            
            // ToUniTask(useFirstValue: false) とだいたい同じ意味
            var result2 = await Observable.Range(0, 10)
                .LastAsync(destroyCancellationToken);
            
            Debug.Log(result2);
        }
    }
}