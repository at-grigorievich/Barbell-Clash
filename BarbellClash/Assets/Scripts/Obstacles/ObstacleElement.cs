using DG.Tweening;
using UnityEngine;

namespace Obstacles
{
    public class ObstacleElement : MonoBehaviour
    {
        [SerializeField] private Vector3 _offVector;
        [SerializeField] private float _rotateDuration;

        public void OffObstacle()
        {
            transform.DORotate(_offVector, _rotateDuration);
        } 
    }
}