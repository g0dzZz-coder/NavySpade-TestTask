﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

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

        public UnityEvent<T> EntitySpawned;
        public UnityEvent<T> EntityDestroed;

        public K Data => spawnableEntity;
        public ObservableCollection<T> SpawnedObjects { get; private set; } = new ObservableCollection<T>();

        protected virtual void Spawn(Tile parent)
        {
            var entity = Instantiate(spawnableEntity.prefab, parent.transform).GetComponent<T>();
            entity.transform.SetParent(root);

            entity.Destroyed += OnEntityDestroyed;

            parent.SetChild(entity.transform);
            entity.Destroyed += x => parent.UnsetChild();

            SpawnedObjects.Add(entity);

            EntitySpawned?.Invoke(entity);
        }

        private void OnEntityDestroyed(EntityBase<K> entity)
        {
            SpawnedObjects.Remove(entity as T);
        }

        public void OnMapUpdated(List<Tile> tiles, int amount)
        {
            RemoveAllObjects();
            SpawnMissing(tiles, amount);
        }

        public void RemoveAllObjects()
        {
            if (Application.isEditor)
            {
                foreach (var child in transform.parent.GetComponentsInChildren<T>())
                    DestroyImmediate(child.gameObject);
            }
            else
            {
                foreach (var obj in SpawnedObjects)
                {
                    try { Destroy(obj.gameObject); }
                    catch { }
                }
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
    }
}