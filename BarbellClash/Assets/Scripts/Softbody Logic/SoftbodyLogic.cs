using UnityEngine;
using Zenject;

namespace Softbody
{
    public abstract class SoftbodyLogic : MonoBehaviour
    {
        public class Factory: PlaceholderFactory<GameObject,SoftbodyLogic>{}
    }
}