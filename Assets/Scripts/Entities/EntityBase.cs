using System;
using UnityEngine;

namespace NavySpade.Entities
{
    using Animation;

    [RequireComponent(typeof(Collider))]
    public class EntityBase<T> : MonoBehaviour
    {
        public T data;

        [SerializeField] private bool _ignoreRaycasts = false;

        public event Action<EntityBase<T>> Destroyed;

        protected Collider Collider { get; private set; }

        protected virtual void Awake()
        {
            Collider = GetComponent<Collider>();

            if (_ignoreRaycasts)
                gameObject.layer = 2;
        }

        private void OnEnable()
        {
            AnimationExtensions.Show(transform);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public virtual void Destroy()
        {
            Collider.isTrigger = true;
            AnimationExtensions.Hide(transform, () => Destroy(gameObject));
        }
    }
}