using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class EveryUpdate : MonoBehaviour
    {
        private void Start()
        {
            // CancellationTokenが指定可能に
            // キャンセル時にOnCompletedが発行される
            Observable.EveryUpdate(destroyCancellationToken)
                .Subscribe(_ => Debug.Log("Update!"));

            // UnityFrameProvider でPlayerLoopTimingを指定可能
            Observable.EveryUpdate(
                    UnityFrameProvider.FixedUpdate,
                    destroyCancellationToken)
                .Subscribe(_ => Debug.Log("FixedUpdate!"));
        }
    }
}