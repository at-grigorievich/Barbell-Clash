using Softbody.Interfaces;
using UnityEngine;

namespace Softbody
{
    public class DieInteractable: MonoBehaviour,IDieInteractable 
    {
        public virtual void AnimateDie(){}
    }
}