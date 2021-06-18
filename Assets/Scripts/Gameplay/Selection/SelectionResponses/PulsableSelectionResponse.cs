using DG.Tweening;
using UnityEngine;

namespace NavySpade.Gameplay
{
    public class PulsableSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        [Range(0f, 2f)]
        [SerializeField] private float endScale = 1f;
        [Range(0.1f, 1f)]
        [SerializeField] private float speed = 0.5f;

        public void OnSelect(Transform transform)
        {
            transform.DOScale(endScale, speed).SetLoops(-1, LoopType.Yoyo);
        }

        public void OnDeselect(Transform transform)
        {
            DOTween.Kill(transform, true);
            transform.localScale = Vector3.one;
        }
    }
}