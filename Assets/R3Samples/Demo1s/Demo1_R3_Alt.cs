using System;
using System.Threading.Tasks;
using R3;
using R3.Triggers;
using UnityEngine;

namespace R3Samples
{
    /// <summary>
    /// Aを押している間は一定間隔で攻撃
    /// Shiftを押している間は攻撃間隔が短くなる
    /// </summary>
    public sealed class Demo1_R3_Alt : MonoBehaviour
    {
        private void Start()
        {
            // Update()をイベント化
            // 毎フレームイベントが発行される
            this.UpdateAsObservable()
                // Aを押している間のみメッセージを通過させる
                .Where(_ => Input.GetKey(KeyCode.A))
                // AwaitOperation.Dropを指定することで、
                // 非同期処理が終わるまではメッセージの受付を行わないモードになる
                .SubscribeAwait(async (_, ct) =>
                {
                    // 攻撃実行
                    Attack();

                    // 一定時間待つ
                    var interval = Input.GetKey(KeyCode.LeftShift)
                        ? 0.1f
                        : 0.5f;
                    await Task.Delay(TimeSpan.FromSeconds(interval), ct);
                }, AwaitOperation.Drop)
                .AddTo(this);
        }

        private void Attack()
        {
            Debug.Log("Attack");
        }
    }
}