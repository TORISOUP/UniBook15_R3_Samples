using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class SerializableReactiveProperty : MonoBehaviour
    {
        // SerializableReactiveProperty を使うとUnityのInspectorで値を設定できる
        [SerializeField]
        private SerializableReactiveProperty<int> _serializableValue = new(0);
        
        public ReadOnlyReactiveProperty<int> SerializableValue => _serializableValue;
    }
}
