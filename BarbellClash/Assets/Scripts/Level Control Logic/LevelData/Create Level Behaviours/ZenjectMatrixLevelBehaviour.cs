using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ATG.LevelControl
{
    public class ZenjectMatrixLevelBehaviour<K> : ICreateLevelBehaviour
        where K : EnvironmentBlock
    {
        private readonly List<float> _lastVertical = new List<float>();
        private readonly PlaceholderFactory<GameObject, K> _factory;

        public ZenjectMatrixLevelBehaviour(PlaceholderFactory<GameObject, K> factory)
        {
            _factory = factory;
        }

        public T[] InstantiateBlocks<T>(EnvironmentBlock[] blocks, GameObject blocksParent)
        {
            var arr = new List<T>();

            Vector3 lastHorizontal = Vector3.zero;

            for (int i = 0; i < blocks.Length; i++)
            {
                var beh = blocks[i];
                var b = _factory.Create(beh.gameObject);

                if (b.transform.TryGetComponent(out T block))
                {
                    arr[i] = block;

                    Vector3 middle =
                        (i > 0 ? new Vector3(0f, b.Size.y * 2f, b.Size.z) / 2f : Vector3.up * b.Size.y);

                    Vector3 vertical = _lastVertical.Count > i
                        ? (_lastVertical[i] + b.Size.x / 2f) * Vector3.right
                        : Vector3.zero;

                    b.transform.position = lastHorizontal + middle + vertical;
                    b.transform.rotation = Quaternion.identity;
                    b.transform.SetParent(blocksParent.transform);

                    if (_lastVertical.Count >= i + 1)
                    {
                        _lastVertical[i] = b.transform.position.x + b.Size.x / 2f;
                    }
                    else
                    {
                        _lastVertical.Add(b.transform.position.x + b.Size.x / 2f);
                    }

                    lastHorizontal = new Vector3(0f, 0f, b.transform.position.z) +
                                     new Vector3(0f, 0f, b.Size.z) / 2f;
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