using UnityEngine;

namespace R3Samples
{
    /// <summary>
    /// Aを押している間は一定間隔で攻撃
    /// Shiftを押している間は攻撃間隔が短くなる
    /// </summary>
    public sealed class Demo1 : MonoBehaviour
    {
        private float _timer = 0;
        private float _attackInterval = 0.5f;

        private void Update()
        {
            _timer += Time.deltaTime;
            
            // Aを押している間は一定間隔で攻撃
            if (Input.GetKey(KeyCode.A))
            {
                if (_timer > _attackInterval)
                {
                    _timer = 0;
                    Attack();
                }
            }
            
            // Shiftを押している間は攻撃間隔が短くなる
            _attackInterval = Input.GetKey(KeyCode.LeftShift) ? 0.1f : 0.5f;
        }

        private void Attack()
        {
            Debug.Log("Attack");
        }
    }
}