using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ATG.LevelControl
{
    public class ZenjectStaticLevelBehaviour<K> : ICreateLevelBehaviour
        where K : EnvironmentBlock
    {
        private readonly PlaceholderFactory<GameObject, K> _factory;

        public ZenjectStaticLevelBehaviour(PlaceholderFactory<GameObject, K> factory)
        {
            _factory = factory;
        }


        public T[] InstantiateBlocks<T>(EnvironmentBlock[] blocks, GameObject blocksParent)
        {
            var arr = new List<T>();

            for (int i = 0; i < blocks.Length; i++)
            {
                var beh = blocks[i];
                var spawnedBlock = _factory.Create(beh.gameObject);

                if (spawnedBlock.transform.TryGetComponent(out T block))
                {
                    spawnedBlock.transform.position = spawnedBlock.Size;
                    spawnedBlock.transform.rotation = Quaternion.identity;

                    arr[i] = block;
                }
                else
                {
                    Debug.LogWarning($"{spawnedBlock.transform.name} hasn't {typeof(T)} type!");
                }
            }

            return arr.ToArray();
        }
    }
}