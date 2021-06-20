using UnityEngine;
using System;

namespace NavySpade.Entities
{
    public class EntityBase<T> : MonoBehaviour
    {
        [SerializeField] public T data;

        public event Action<EntityBase<T>> Destroyed;

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}