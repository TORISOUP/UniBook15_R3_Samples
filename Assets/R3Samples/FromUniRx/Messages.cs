using System;
using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class Messages : MonoBehaviour
    {
        private void Start()
        {
            using var subject = new Subject<int>();
            
            // OnNextは変わらず
            subject.OnNext(1);

            // 例外はOnErrorResumeで通知できる
            subject.OnErrorResume(new Exception("Error!"));

            // 引数なしのOnCompletedは正常終了という意味
            subject.OnCompleted();

            // 例外を伴うOnCompletedは異常終了という意味
            subject.OnCompleted(new Exception("Error!"));
            
            Observable.Return("Hi!")
                // ここで例外発生
                // この例外は OnErrorResume として発行される
                .Select(int.Parse) 
                .Subscribe(
                    onNext: x => Debug.Log(x),
                    onErrorResume: error => Debug.LogError(error),
                    onCompleted: result =>
                    {
                        if (result.IsSuccess)
                        {
                            Debug.Log("Completed!");
                        }
                        else
                        {
                            Debug.LogError(result.Exception);
                        }
                    });
        }
    }
}