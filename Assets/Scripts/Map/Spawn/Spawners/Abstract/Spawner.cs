using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NavySpade
{
    using Entities;
    using Map;
    using System;
    using Utils;

    public abstract class Spawner<T, K> : MonoBehaviour where T : EntityBase<K> where K : EntityData
    {
        public class EnityCollection : List<T>
        {
            public event Action<int> CountChanged;

            public new void Add(T crystal)
            {
                base.Add(crystal);
                CountChanged?.Invoke(Count);
            }

            public new void Remove(T crystal)
            {
                base.Add(crystal);
                CountChanged?.Invoke(Count);
            }
        }

        [SerializeField] protected MapGenerator generator = null;
        [SerializeField] protected K spawnableEntity;
        [SerializeField] protected Transform root = null;

        public UnityEvent EntitySpanwed;

        public K Data => spawnableEntity;
        public EnityCollection SpawnedObjects { get; private set; } = new EnityCollection();
        protected Tile[] SpawnZones { get; private set; }

        protected virtual void Spawn(SpawnZone parent)
        {
            var entity = Instantiate(spawnableEntity.prefab, parent.transform).GetComponent<T>();
            entity.transform.SetParent(root);
            parent.SetChild(entity.gameObject);

            SpawnedObjects.Add(entity);

            EntitySpanwed?.Invoke();
        }

        public void OnMapUpdated(List<Tile> tiles, int amount)
        {
            RemoveAllObjects();
            SpawnMissing(tiles, amount);
        }

        protected void SpawnMissing(List<Tile> tiles, int requiredQuantity)
        {
            while (SpawnedObjects.Count < requiredQuantity)
            {
                var freePlace = tiles.Random();
                if (freePlace == null || freePlace.IsPlaceTaken)
                    continue;

                Spawn(freePlace);

                Debug.Log(1);
            }
        }

        protected void RemoveAllObjects()
        {
            foreach (var obj in SpawnedObjects)
            {
                if (Application.isPlaying)
                    Destroy(obj.gameObject);
                else
                    DestroyImmediate(obj.gameObject);
            }

            SpawnedObjects.Clear();
        }
    }
}