using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UniRx;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    
    public sealed class AltToUniTask : MonoBehaviour
    {
        private async UniTaskVoid Start()
        {
            Debug.Log("---UniRx---");
            await UniRxWaitSampleAsync(
                UniRx.Observable.Range(0, 10),
                destroyCancellationToken);

            Debug.Log("---R3---");
            await R3WaitSampleAsync(
                R3.Observable.Range(0, 10),
                destroyCancellationToken);
            
        }

        // UniRxの場合
        private async UniTask UniRxWaitSampleAsync(
            IObservable<int> observable,
            CancellationToken ct)
        {
            // Observableの最初のメッセージの到着を待つ
            // Take(1)をつけたのとだいたい同じ
            var result1 = await observable.ToUniTask(
                useFirstValue: true,
                cancellationToken: ct);
            Debug.Log($"{result1}");

            // Observableが完了するのを待ち、最後のメッセージを取り出す
            // useFirstValue: false
            var result2 = await observable.ToUniTask(cancellationToken: ct);
            Debug.Log($"{result2}");

            // また実はUniRxのObservableは直接awaitもできる
            // この場合はObservableが完了するのを待ち、最後のメッセージを取り出す
            var result3 = await observable;
            Debug.Log($"{result3}");
        }

        // R3の場合
        private async UniTask R3WaitSampleAsync(
            Observable<int> observable,
            CancellationToken ct)
        {
            // Observableの最初のメッセージの到着を待つ
            var result1 = await observable.FirstAsync(cancellationToken: ct);
            Debug.Log($"{result1}");

            // Observableが完了するのを待ち、最後のメッセージを取り出す
            var result2 = await observable.LastAsync(cancellationToken: ct);
            Debug.Log($"{result2}");
        }
    }
}