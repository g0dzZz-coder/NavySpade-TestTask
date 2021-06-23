using UnityEngine;
using DG.Tweening;

namespace NavySpade.Misc
{
    using Animation;

    public class Rotatable : MonoBehaviour
    {
        [SerializeField] private LoopAnimationSettings _settings = null;
        [SerializeField] private Vector3 _direction = new Vector3(0, 1, 0);
        [SerializeField] private float _angle = 90f;

        private void Start()
        {
            StartAnimation();
        }

        //private void FixedUpdate()
        //{
        //    transform.Rotate((Vector3.up + Vector3.right) * _settings.duration * Time.fixedDeltaTime);
        //}

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }

        private void StartAnimation()
        {
            transform.DORotate(_direction * _angle, _settings.duration, RotateMode.Fast)
                .SetLoops(-1, _settings.loopType)
                .SetEase(_settings.ease);
        }
    }
}