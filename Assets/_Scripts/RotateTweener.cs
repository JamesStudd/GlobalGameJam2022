using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Animations;

namespace _Scripts
{
    public class RotateTweener : MonoBehaviour
    {
        [SerializeField] private float _time;
        [SerializeField] private Ease _ease;
        [SerializeField] private Axis _affectedAxis = Axis.Z;
        
        [Range(-1, 1)]
        [SerializeField] private float _direction = 1f;

        private Tweener _tweener;

        private void Awake()
        {
            var rotation = new Vector3(
                GetDegreeForAxis(Axis.X),
                GetDegreeForAxis(Axis.Y),
                GetDegreeForAxis(Axis.Z)
            );

            _tweener = transform.DOLocalRotate(rotation, _time, RotateMode.LocalAxisAdd)
                .SetEase(_ease)
                .SetLoops(-1, LoopType.Incremental);
        }

        private void LateUpdate()
        {
            if (_tweener == null)
            {
                return;
            }

            if (!gameObject.activeSelf && _tweener.IsPlaying())
            {
                _tweener.Pause();
            }
            else if (!_tweener.IsPlaying())
            {
                _tweener.Play();
            }
        }

        private void OnDestroy()
        {
            _tweener?.Kill();
        }

        private float GetDegreeForAxis(Axis axis)
        {
            return (_affectedAxis.HasFlag(axis) ? 360 : 0) * _direction;
        }
    }
}