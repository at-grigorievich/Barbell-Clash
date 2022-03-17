using System;
using System.Linq;
using UnityEngine;

namespace VFXLogic
{
    [Serializable]
    public class VFXDataContainer
    {
        [SerializeField] private VFXType vfxType;
        [SerializeField] private ParticleSystem vfxPrefab;

        public VFXType VFXInfo => vfxType;
        public ParticleSystem ParticlePrefab => vfxPrefab;
    }
    
    [CreateAssetMenu(fileName = "VFX data", menuName = "VFX/New VFX data", order = 0)]
    public class VFXData : ScriptableObject
    {
        [SerializeField] private VFXDataContainer[] data;

        public ParticleSystem GetParticlePrefabByType(VFXType vxfType)
        {
            VFXDataContainer select = data.FirstOrDefault(el => el.VFXInfo == vxfType);

            if (select == null)
                throw new NullReferenceException($"Cant find VFX with VFX Type {vxfType.ToString()}");

            return select.ParticlePrefab;
        }
    }
}