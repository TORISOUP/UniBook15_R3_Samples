using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class ForEachAsyncSample1 : MonoBehaviour
    {
        private void Start()
        {
            SampleAsync(destroyCancellationToken).Forget();
        }

        private async UniTask SampleAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                // 1秒おきに1m前進する を 10回繰り返す
                await Observable.Interval(TimeSpan.FromSeconds(1), ct)
                    .Take(10)
                    .ForEachAsync(_ =>
                        {
                            transform.position += Vector3.forward;
                        },
                        cancellationToken: ct);

                // 10回繰り返したらObservableが完了するため、ここに到達する
                // 3秒ほど待ってから処理を再開させる
                await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: ct);
            }
        }
    }
}