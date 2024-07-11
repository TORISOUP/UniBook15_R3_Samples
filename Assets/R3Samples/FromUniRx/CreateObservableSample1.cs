using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class CreateObservableSample1 : MonoBehaviour
    {
        private void TaskToObservable()
        {
            // CancellationTokenを指定せずに単に変換するとき(非推奨)
            var observable1 = SampleAsync(default).ToObservable();

            // Observableの購読中断時に非同期処理もキャンセルしたい場合
            var observable2 = Observable.FromAsync(async ct => await SampleAsync(ct));
        }
        

        // 非同期メソッド
        private async UniTask<int> SampleAsync(CancellationToken token)
        {
            await UniTask.Delay(1000, cancellationToken: token);
            return 1;
        }
    }
}