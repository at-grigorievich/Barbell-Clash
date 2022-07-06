using UnityEngine;

namespace Barbell
{
    public interface ICrushable
    {
        public uint MaxPlateId { get; }
        Transform CollidersContainer { get; }
        
        void SetCrushCollider(bool enabled);
    }
}