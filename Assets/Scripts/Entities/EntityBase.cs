using UnityEngine;
using System;
using DG.Tweening;

namespace NavySpade.Entities
{
    [RequireComponent(typeof(Collider))]
    public class EntityBase<T> : MonoBehaviour
    {
        public T data;

        public event Action<EntityBase<T>> Destroyed;

        private Collider _collider;

        protected virtual void Start()
        {
            _collider = GetComponent<Collider>();

            var targetScale = transform.localScale;
            transform.localScale = Vector3.zero;
            transform.DOScale(targetScale, 0.5f);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public virtual void Destroy()
        {
            _collider.isTrigger = true;
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject));
        }
    }
}