using UnityEngine;

namespace NavySpade.Entities
{
    public class Obstacle : EntityBase<SpawnableEntityData>
    {
        [MinMaxSlider(0f, 3f)]
        [SerializeField] private Vector2 _minMaxScales = new Vector2(0.1f, 2f);

        public void SetRandomHeight()
        {
            var height = Random.Range(_minMaxScales.x, _minMaxScales.y);
            transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, height / 2f, transform.position.z);
        }
    }
}