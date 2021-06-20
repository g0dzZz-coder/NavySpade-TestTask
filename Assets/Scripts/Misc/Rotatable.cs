using UnityEngine;
using DG.Tweening;

namespace NavySpade.Misc
{
    using Animation;

    public class Rotatable : MonoBehaviour
    {
        [SerializeField] private LoopAnimationSettings settings = null;
        [SerializeField] private Vector3 direction = new Vector3(0, 1, 0);
        [SerializeField] private float angle = 90f;

        private void Start()
        {
            StartAnimation();
        }

        //private void FixedUpdate()
        //{
        //    transform.Rotate((Vector3.up + Vector3.right) * speed * Time.fixedDeltaTime);
        //}

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }

        private void StartAnimation()
        {
            transform.DORotate(direction * angle, settings.duration, RotateMode.Fast)
                .SetLoops(-1, settings.loopType)
                .SetEase(settings.ease);
        }
    }
}