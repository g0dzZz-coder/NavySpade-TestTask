using UnityEngine;

namespace NavySpade.Map
{
    using Entities;

    public class Tile : MonoBehaviour, ISelectable
    {
        public virtual bool IsFree => child == null;

        private Transform child = null;

        private void Awake()
        {
            if (transform.childCount > 0)
                child = GetComponentInChildren<Transform>();
        }

        public void SetChild(Transform child)
        {
            this.child = child;
            child.SetParent(transform);
        }

        public void UnsetChild()
        {
            child = null; 
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsFree ? Color.green:  Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;

            var center = new Vector3(0, transform.localScale.y/2f, 0f);
            var size = new Vector3(transform.localScale.x, 0, transform.localScale.z);

            Gizmos.DrawWireCube(center, size);
        }
    }
}