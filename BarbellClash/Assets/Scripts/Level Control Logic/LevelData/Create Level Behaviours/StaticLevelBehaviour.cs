using System.Collections.Generic;
using UnityEngine;

namespace ATG.LevelControl
{
    public class StaticLevelBehaviour : ICreateLevelBehaviour
    {
        public T[] InstantiateBlocks<T>(EnvironmentBlock[] blocks, GameObject blocksParent)
        {
            var arr = new List<T>();

            for (int i = 0; i < blocks.Length; i++)
            {
                var beh = blocks[i];
                var spawnedBlock = GameObject.Instantiate(beh);

                if (spawnedBlock.transform.TryGetComponent(out T block))
                {
                    spawnedBlock.transform.position = beh.Size;
                    spawnedBlock.transform.rotation = Quaternion.identity;

                    arr.Add(block);
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