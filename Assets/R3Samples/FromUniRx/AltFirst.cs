using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class AltFirst : MonoBehaviour
    {
        private void Start()
        {
            // Firstとだいたい同じになるが、
            // OnNextの発行数がゼロだったときの例外発火は再現できない
            Observable.Range(0, 10, destroyCancellationToken)
                .Where(x => x % 2 == 1)
                .Take(1)
                .Subscribe(x => Debug.Log(x)); // 1

            // FirstOrDefaultとだいたい同じ
            Observable.Empty<int>()
                .Where(x => x % 2 == 1)
                .Take(1)
                .DefaultIfEmpty(100)
                .Subscribe(x => Debug.Log(x)); // 100

            // Lastとだいたい同じになるが
            // OnNextの発行数がゼロだったときの例外発火は再現できない
            Observable.Range(0, 10, destroyCancellationToken)
                .Where(x => x % 2 == 0)
                .TakeLast(1)
                .Subscribe(x => Debug.Log(x)); // 8

            // LastOrDefaultとだいたい同じ
            Observable.Empty<int>()
                .Where(x => x % 2 == 0)
                .TakeLast(1)
                .DefaultIfEmpty(100)
                .Subscribe(x => Debug.Log(x)); // 100

            // Single, SingleOrDefault は簡単に再現できない
        }
    }
}