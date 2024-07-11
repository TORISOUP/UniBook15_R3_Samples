using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class ConvertObservable : MonoBehaviour
    {
        void Start()
        {
            // R3 の Subject
            using var r3Subject = new R3.Subject<int>();

            // R3 の Observable
            R3.Observable<int> r3Observable = r3Subject;

            // R3 の Observable を System の IObservable に変換
            System.IObservable<int> observable = r3Observable.AsSystemObservable();

            // System の IObservable を R3 の Observable に変換
            R3.Observable<int> r3Observable2 = observable.ToObservable();
        }
    }
}