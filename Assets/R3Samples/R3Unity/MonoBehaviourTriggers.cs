using R3;
using R3.Triggers;
using UnityEngine;

namespace R3Samples.R3Unity
{
    public sealed class MonoBehaviourTriggers : MonoBehaviour
    {
        private void Start()
        {
            // Update() を Observable として扱う
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    Debug.Log("Update!");
                });
            
            // FixedUpdate() を Observable として扱う
            this.FixedUpdateAsObservable()
                .Subscribe(_ =>
                {
                    Debug.Log("FixedUpdate!");
                });
            
            // OnCollisionEnter() を Observable として扱う
            this.OnCollisionEnterAsObservable()
                .Subscribe(collision =>
                {
                    Debug.Log("OnCollisionEnter: " + collision.gameObject.name);
                });

            // OnDestroy() を Observable として扱う
            this.OnDestroyAsObservable()
                .Subscribe(_ =>
                {
                    Debug.Log("OnDestroy!");
                });
            
            // 他にもMonoBehaviour上に定義されたUnityのイベント関数は
            // ほぼすべてObservableとして扱うことができる
        }
    }
}
