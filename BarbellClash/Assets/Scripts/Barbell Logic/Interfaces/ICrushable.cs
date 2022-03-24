using UnityEngine;

namespace Barbell
{
    public interface ICrushable
    {
        public uint MaxPlateId { get; }
        Collider[] Colliders { get; }
        
        void SetCrushCollider(bool enabled);
    }
}