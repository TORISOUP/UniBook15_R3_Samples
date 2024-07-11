using ObservableCollections;
using R3;
using UnityEngine;

namespace R3Samples.FromUniRx
{
    public sealed class ObservableCollectionsSample : MonoBehaviour
    {
        private void Start()
        {
            {
                // ほぼUniRx.ReactiveCollectionと同じ
                var observableList = new ObservableList<int>();

                // IReadOnlyReactiveCollectionはないが
                // IObservableCollectionを使うことでReadOnlyにできる
                IObservableCollection<int> readOnly = observableList;

                // 各種コレクションに起きた変化をイベントとして購読できる
                // UniRxとの違いはCancellationTokenの指定ができる
                readOnly.ObserveAdd(destroyCancellationToken)
                    .Subscribe(x => Debug.Log($"Add: {x}"))
                    .AddTo(this);
                readOnly.ObserveRemove(destroyCancellationToken)
                    .Subscribe(x => Debug.Log($"Remove: {x}"))
                    .AddTo(this);
                readOnly.ObserveMove(destroyCancellationToken)
                    .Subscribe(x => Debug.Log($"Move: {x}"))
                    .AddTo(this);
                readOnly.ObserveReplace(destroyCancellationToken)
                    .Subscribe(x => Debug.Log($"Replace: {x}"))
                    .AddTo(this);
                readOnly.ObserveReset(destroyCancellationToken)
                    .Subscribe(_ => Debug.Log("Reset"))
                    .AddTo(this);
                
                observableList.Add(1);
                observableList.Add(2);
                observableList.Remove(1);
                observableList[0] = 3;
                observableList.Clear();
            }

            {
                // ほぼUniRx.ReactiveDictionaryと同じ
                var observableDictionary = new ObservableDictionary<int, string>();

                // IReadOnlyReactiveDictionaryはないが
                // IReadOnlyObservableDictionaryを使うことでReadOnlyにできる
                IReadOnlyObservableDictionary<int, string> readOnly = observableDictionary;
                
                // 各種コレクションに起きた変化をイベントとして購読できる
                // ObservableDictionaryを扱う場合は「ObserverDictionary***」を使うと便利
                readOnly.ObserveDictionaryAdd(destroyCancellationToken)
                    .Subscribe(x => Debug.Log($"Add: {x.Key} - {x.Value}"))
                    .AddTo(this);
                readOnly.ObserveDictionaryRemove(destroyCancellationToken)
                    .Subscribe(x => Debug.Log($"Remove: {x.Key} - {x.Value}"))
                    .AddTo(this);
                readOnly.ObserveDictionaryReplace(destroyCancellationToken)
                    .Subscribe(x => Debug.Log($"Replace: {x.Key} - {x.NewValue}"))
                    .AddTo(this);
                readOnly.ObserveReset(destroyCancellationToken)
                    .Subscribe(_ => Debug.Log("Reset"))
                    .AddTo(this);
                
                observableDictionary.Add(1, "one");
                observableDictionary.Add(2, "two");
                observableDictionary.Remove(1);
                observableDictionary[2] = "three";
                observableDictionary.Clear();
            }
        }
    }
}