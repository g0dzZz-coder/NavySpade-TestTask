using UnityEngine;

namespace NavySpade.Gameplay
{
    using Map;

    public abstract class Spawner<T> : MonoBehaviour
    {
        [SerializeField] protected MapGenerator generator = null;
        [SerializeField] protected T spawnableEntity;
        [SerializeField] protected Transform root = null;

        protected Tile[] SpawnZones { get; private set; }

        protected abstract void Spawn(Transform parent);
    }
}