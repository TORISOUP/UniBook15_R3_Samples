using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class ReactivePropertySample : MonoBehaviour
    {
        // ReactivePropertyはUniRxのReactivePropertyと同じように使える
        private ReactiveProperty<int> _value = new(0);
        
        // ReadOnlyに公開する場合は「ReadOnlyReactiveProperty」にキャストする
        public ReadOnlyReactiveProperty<int> Value => _value;
    }
}
