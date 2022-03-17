using UnityEngine;

namespace ATG.LevelControl
{
    public interface ICreateLevelBehaviour
    {
        T[] InstantiateBlocks<T>(EnvironmentBlock[] blocks, GameObject blocksParent);
    }
}