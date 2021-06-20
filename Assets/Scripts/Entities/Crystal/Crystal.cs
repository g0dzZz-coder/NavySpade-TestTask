using UnityEngine;

namespace NavySpade.Entities
{
    public class Crystal : EntityBase<CrystalData>
    {
        [SerializeField] private float initialHeight = 0.5f;

        private void Awake()
        {
            transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);
        }
    }
}