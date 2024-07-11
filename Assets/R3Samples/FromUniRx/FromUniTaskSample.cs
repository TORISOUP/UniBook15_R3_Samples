using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class FromUniTaskSample : MonoBehaviour
    {
        private Observable<T> ConvertFromUniTask<T>(UniTask<T> uniTask)
        {
            // UniTask -> ValueTask はノーコストで実行可能
            // ValueTask -> Observable はR3がサポートしている
            return uniTask.AsValueTask().ToObservable();
        }
    }
}