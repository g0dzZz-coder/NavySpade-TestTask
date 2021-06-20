using UnityEngine;

namespace NavySpade.Entities
{
    using Core;

    public interface ICollectable
    {
        void OnCollect();
    }

    public class Crystal : EntityBase<CrystalData>, ICollectable
    {
        [SerializeField] private float initialHeight = 0.5f;

        private void Awake()
        {
            transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);
        }

        public void OnCollect()
        {
            Player.IncreaseScore(data.GetReward());
            Destroy(gameObject);
        }
    }
}