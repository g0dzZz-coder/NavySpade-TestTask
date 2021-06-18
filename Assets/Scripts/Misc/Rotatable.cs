using UnityEngine;
using DG.Tweening;

namespace NavySpade.Misc
{
    public class Rotatable : MonoBehaviour
    {
        [Range(0f, 100f)]
        [SerializeField] float duration = 1f;
        [SerializeField] LoopType loopType = LoopType.Incremental;
        [SerializeField] Ease ease = Ease.InCubic;

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
            transform.DORotate(new Vector3(0, 180, 0), duration, RotateMode.Fast).SetLoops(-1, loopType).SetEase(ease);
        }
    }
}