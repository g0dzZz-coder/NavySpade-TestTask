using UnityEngine;

namespace NavySpade.Entities
{
    public class Crystal : EntityBase<CrystalData>
    {
        [SerializeField] private float _initialHeight = 0.5f;

        protected override void Awake()
        {
            base.Awake();

            transform.position = new Vector3(transform.position.x, _initialHeight, transform.position.z);
        }
    }
}