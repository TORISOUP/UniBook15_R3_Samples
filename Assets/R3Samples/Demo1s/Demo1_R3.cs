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
    public sealed class Demo1_R3 : MonoBehaviour
    {
        private void Start()
        {
            // Update()をイベント化
            // 毎フレームイベントが発行される
            this.UpdateAsObservable()
                // Aを押している間のみメッセージを通過させる
                .Where(_ => Input.GetKey(KeyCode.A))
                // メッセージのスロットリングを行う
                .ThrottleFirst(async (_, ct) =>
                {
                    // Shiftを押している間は攻撃間隔が短くなる
                    var interval = Input.GetKey(KeyCode.LeftShift)
                        ? 0.1f
                        : 0.5f;

                    // 一定時間待機する
                    await Task.Delay(TimeSpan.FromSeconds(interval), ct);
                    // （UniTaskを導入しているならUniTask.Delayを使ったほうが良い）
                })
                // メッセージがここまで届いたらAttack()を実行する
                .Subscribe(_ => Attack())
                .AddTo(this);
        }

        private void Attack()
        {
            Debug.Log("Attack");
        }
    }
}