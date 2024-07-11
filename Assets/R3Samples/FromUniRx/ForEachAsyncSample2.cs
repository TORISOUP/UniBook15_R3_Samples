using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class ForEachAsyncSample2 : MonoBehaviour
    {
        // 失敗時に指定回数までObservableをリトライする
        private async UniTask RetryAsync<T>(
            // OnErrorResumeが発行されるかもしれないObservable
            Observable<T> mayBeErrorObservable,
            int maxRetryCount = 3,
            CancellationToken ct = default)
        {
            var retryCount = maxRetryCount;

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    await mayBeErrorObservable
                        // OnErrorResume を OnCompleted(Exception) に変換
                        .OnErrorResumeAsFailure()
                        // ForEachAsync で待機
                        // OnCompleted(Exception)が発行された場合、例外が飛ぶ
                        .ForEachAsync(x =>
                        {
                            // do something...
                        }, cancellationToken: ct);

                    // 正常終了した場合は何もせず終わり
                    break;
                }
                catch (Exception)
                {
                    // 例外が発行された場合はリトライ数までリトライ
                    // 上限に達した場合は例外を再度投げて終了
                    if (--retryCount > 0) continue;
                    throw;
                }
            }
        }
    }
}