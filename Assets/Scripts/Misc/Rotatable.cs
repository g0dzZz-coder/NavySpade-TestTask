using UnityEngine;
using DG.Tweening;

namespace NavySpade.Misc
{
    using Animation;

    public class Rotatable : MonoBehaviour
    {
        [SerializeField] private LoopAnimationSettings settings = null;

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
            transform.DORotate(Vector3.up * 90, settings.duration, RotateMode.Fast)
                .SetLoops(-1, settings.loopType)
                .SetEase(settings.ease);
        }
    }
}