using R3;
using UnityEngine;

namespace R3Samples.R3Unity
{
    public sealed class UnityFrameProviderSample : MonoBehaviour
    {
        private void Start()
        {
            // デフォルトではUpdate間隔
            Observable.EveryUpdate(destroyCancellationToken)
                .Subscribe();
            
            // FixedUpdate間隔に変更
            Observable.EveryUpdate(
                    UnityFrameProvider.FixedUpdate,
                    destroyCancellationToken)
                .Subscribe();
            
            // 「スペースキーが押されていたフレーム数」をカウントし、
            // その集計結果を毎フレームのFixedUpdateて取り出す
            Observable.EveryUpdate(destroyCancellationToken)
                .Where(_ => Input.GetKey(KeyCode.Space))
                // FixedUpdateが1回来るまでバッファに詰める
                .ChunkFrame(1, UnityFrameProvider.FixedUpdate)
                .Where(xs=> xs.Length > 0)
                .Subscribe(xs =>
                {
                    Debug.Log($"Space key pressed for {xs.Length} frames");
                });
        }
    }
}