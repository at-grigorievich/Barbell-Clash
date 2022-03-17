using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ATG.LevelControl
{
    public class ZenjectLineLevelBehaviour<K> : ICreateLevelBehaviour
        where K : EnvironmentBlock
    {
        private readonly PlaceholderFactory<GameObject, K> _factory;

        public ZenjectLineLevelBehaviour(PlaceholderFactory<GameObject, K> factory)
        {
            _factory = factory;
        }

        public T[] InstantiateBlocks<T>(EnvironmentBlock[] blocks, GameObject blocksParent)
        {
            var arr = new List<T>();

            Vector3 lastPos = Vector3.zero;

            for (int i = 0; i < blocks.Length; i++)
            {
                var beh = blocks[i];
                var b = _factory.Create(beh.gameObject);

                if (b.transform.TryGetComponent(out T block))
                {
                    arr.Add(block);

                    Vector3 middle = (i > 0 ? b.Size / 2f : Vector3.zero);

                    b.transform.position = lastPos + middle;
                    b.transform.rotation = Quaternion.identity;
                    b.transform.SetParent(blocksParent.transform);

                    lastPos = b.transform.position + b.Size / 2f;
                }
                else
                {
                    Debug.LogWarning($"{b.transform.name} hasn't {typeof(T)} type!");
                }
            }

            return arr.ToArray();
        }
    }
}