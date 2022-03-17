using System.Collections.Generic;
using UnityEngine;

namespace ATG.LevelControl
{
    public class MatrixLevelBehaviour : ICreateLevelBehaviour
    {
        private readonly List<float> _lastVertical = new List<float>();

        public T[] InstantiateBlocks<T>(EnvironmentBlock[] blocks, GameObject blocksParent)
        {
            var arr = new List<T>();

            Vector3 lastHorizontal = Vector3.zero;

            for (int i = 0; i < blocks.Length; i++)
            {
                var beh = blocks[i];
                var b = GameObject.Instantiate(beh);

                if (b.transform.TryGetComponent(out T block))
                {
                    arr.Add(block);

                    Vector3 middle =
                        (i > 0 ? new Vector3(0f, beh.Size.y * 2f, beh.Size.z) / 2f : Vector3.up * beh.Size.y);

                    Vector3 vertical = _lastVertical.Count > i
                        ? (_lastVertical[i] + beh.Size.x / 2f) * Vector3.right
                        : Vector3.zero;

                    b.transform.position = lastHorizontal + middle + vertical;
                    b.transform.rotation = Quaternion.identity;
                    b.transform.SetParent(blocksParent.transform);

                    if (_lastVertical.Count >= i + 1)
                    {
                        _lastVertical[i] = b.transform.position.x + beh.Size.x / 2f;
                    }
                    else
                    {
                        _lastVertical.Add(b.transform.position.x + beh.Size.x / 2f);
                    }

                    lastHorizontal = new Vector3(0f, 0f, b.transform.position.z) + new Vector3(0f, 0f, beh.Size.z) / 2f;
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