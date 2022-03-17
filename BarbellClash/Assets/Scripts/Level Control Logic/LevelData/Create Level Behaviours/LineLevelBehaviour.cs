using System.Collections.Generic;
using UnityEngine;

namespace ATG.LevelControl
{
    public class LineLevelBehaviour : ICreateLevelBehaviour
    {
        public T[] InstantiateBlocks<T>(EnvironmentBlock[] blocks, GameObject blocksParent)
        {
            var arr = new List<T>();

            Vector3 lastPos = Vector3.zero;

            for (int i = 0; i < blocks.Length; i++)
            {
                var beh = blocks[i];
                var b = GameObject.Instantiate(beh);

                if (b.transform.TryGetComponent(out T block))
                {
                    arr.Add(block);

                    Vector3 middle = (i > 0 ? beh.Size / 2f : Vector3.zero);

                    b.transform.position = lastPos + middle;
                    b.transform.rotation = Quaternion.identity;
                    b.transform.SetParent(blocksParent.transform);

                    lastPos = b.transform.position + beh.Size / 2f;
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