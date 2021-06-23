using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace NavySpade
{
    using Entities;
    using Map;
    using Utils;

    public abstract class Spawner<T, K> : MonoBehaviour where T : EntityBase<K> where K : EntityData
    {
        [SerializeField] protected MapGenerator generator = null;
        [SerializeField] protected K spawnableEntity;
        [SerializeField] protected Transform root = null;

        public K Data => spawnableEntity;
        public ObservableCollection<T> SpawnedObjects { get; private set; } = new ObservableCollection<T>();

        protected virtual T Spawn(Tile parent)
        {
            var entity = Instantiate(spawnableEntity.prefab, parent.transform).GetComponent<T>();
            entity.transform.SetParent(root);

            entity.Destroyed += OnEntityDestroyed;
            entity.Destroyed += x => parent.UnsetChild();

            SpawnedObjects.Add(entity);

            return entity;
        }

        public virtual void OnMapUpdated(List<Tile> tiles, int amount)
        {
            RemoveAllObjects();
            SpawnMissing(tiles, amount);
        }

        public void RemoveAllObjects()
        {
            if (Application.isPlaying)
            {
                foreach (var obj in SpawnedObjects)
                {
                    try { Destroy(obj.gameObject); }
                    catch { }
                }
            }
            else
            {
                foreach (var child in transform.parent.GetComponentsInChildren<T>())
                    DestroyImmediate(child.gameObject);
            }

            SpawnedObjects.Clear();
        }

        protected void SpawnMissing(List<Tile> tiles, int requiredQuantity)
        {
            if (tiles == null || tiles.Count == 0)
                return;

            while (SpawnedObjects.Count < requiredQuantity)
            {
                var freePlace = tiles.Random();
                if (freePlace == null || freePlace.IsFree == false)
                    continue;

                Spawn(freePlace);
            }
        }

        private void OnEntityDestroyed(EntityBase<K> entity)
        {
            SpawnedObjects.Remove(entity as T);
        }
    }
}