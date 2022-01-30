using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class HoverTweener : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _time;
        [SerializeField] private float _amount;
        [SerializeField] private Ease _ease;

        private void Awake()
        {
            var position = _root.localPosition;
            
            var target = position + new Vector3(position.x, position.y + _amount, position.z);
            
            _root.DOLocalMove(target, _time)
                .SetEase(_ease)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
    }
}