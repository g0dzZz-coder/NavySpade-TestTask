using UnityEngine;

namespace NavySpade.Entities
{
    public class Crystal : EntityBase<CrystalData>
    {
        [SerializeField] private float initialHeight = 0.5f;

        protected override void Awake()
        {
            base.Awake();
            transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);
        }
    }
}