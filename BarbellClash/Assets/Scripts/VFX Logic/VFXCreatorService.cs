using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace VFXLogic
{
    public class VFXCreatorService : MonoBehaviour, IVFXControllable
    {
        [Inject] private VFXData _prefabData;

        private Dictionary<VFXType, List<ParticleSystem>> _activeData = new Dictionary<VFXType, List<ParticleSystem>>();


        public ParticleSystem PlayVFX(VFXType type, Vector3 position, Vector3 lookTO)
        {
            if (!_activeData.ContainsKey(type))
            {
                ParticleSystem newPs = Instantiate(_prefabData.GetParticlePrefabByType(type));
                List<ParticleSystem> newType = new List<ParticleSystem>(){newPs};

                _activeData.Add(type,newType);
            }
            
            var waitingVFX = _activeData[type].FirstOrDefault(p => !p.isPlaying);
            if (waitingVFX == null)
            {
                waitingVFX = Instantiate(_prefabData.GetParticlePrefabByType(type));
                _activeData[type].Add(waitingVFX);
            }

            waitingVFX.transform.position = position;
            //waitingVFX.transform.LookAt(lookTO);
            waitingVFX.transform.rotation = Quaternion.Euler(lookTO);

            waitingVFX.Play();
            
            return waitingVFX;
        }
        public ParticleSystem PlayVFX(VFXType type, Vector3 position, Transform lookTo)
        {
            if (!_activeData.ContainsKey(type))
            {
                ParticleSystem newPs = Instantiate(_prefabData.GetParticlePrefabByType(type));
                List<ParticleSystem> newType = new List<ParticleSystem>(){newPs};

                _activeData.Add(type,newType);
            }
            
            var waitingVFX = _activeData[type].FirstOrDefault(p => !p.isPlaying);
            if (waitingVFX == null)
            {
                waitingVFX = Instantiate(_prefabData.GetParticlePrefabByType(type));
                _activeData[type].Add(waitingVFX);
            }

            waitingVFX.transform.position = position;
            waitingVFX.transform.LookAt(lookTo);

            waitingVFX.Play();
            
            return waitingVFX;
        }
        public ParticleSystem PlayVFXLoop(VFXType type, Vector3 position, Vector3 lookTo)
        {
            ParticleSystem ps;
            
            if (!_activeData.ContainsKey(type))
            {
                ParticleSystem newPs = Instantiate(_prefabData.GetParticlePrefabByType(type));
                List<ParticleSystem> newType = new List<ParticleSystem>(){newPs};

                _activeData.Add(type,newType);

                ps = newPs;
            }
            else
            {
                ps = _activeData[type][0];
            }
            
            ps.transform.position = position;
            //ps.transform.LookAt(lookTo);
            ps.transform.rotation = Quaternion.Euler(lookTo);

            ps.Play();

            return ps;
        }
        public ParticleSystem PlayVFXLoop(VFXType type, Vector3 position, Transform lookTo)
        {
            ParticleSystem ps;
            
            if (!_activeData.ContainsKey(type))
            {
                ParticleSystem newPs = Instantiate(_prefabData.GetParticlePrefabByType(type));
                List<ParticleSystem> newType = new List<ParticleSystem>(){newPs};

                _activeData.Add(type,newType);

                ps = newPs;
            }
            else
            {
                ps = _activeData[type][0];
            }
            
            ps.transform.position = position;
            ps.transform.LookAt(lookTo);

            ps.Play();
            
            return ps;
        }
    }
}
    
