using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class PulseComponent : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _scaleAmount;
        [SerializeField] private float _scaleTime;
        
        private void Awake()
        {
            var pos = transform.localScale;
            var endVal = new Vector3(pos.x * _scaleAmount, pos.y * _scaleAmount, pos.z * _scaleAmount);
            
            _root.DOScale(endVal, _scaleTime)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
    }
}