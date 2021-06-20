using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace NavySpade.Map
{
    using Entities;
    using Utils;

    public class EnityCollection: List<Crystal>
    {
        public event Action<int> CountChanged;

        public new void Add(Crystal crystal)
        {
            base.Add(crystal);
            CountChanged?.Invoke(Count);
        }

        public new void Remove(Crystal crystal)
        {
            base.Add(crystal);
            CountChanged?.Invoke(Count);
        }
    }

    public class CrystalSpawner : Spawner<Crystal, CrystalData>
    {
        private Coroutine spawnCoroutine;

        private void Awake()
        {
            generator.MapUpdated += spawnZones => OnMapUpdated(spawnZones, spawnableEntity.startAmount);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        protected override void Spawn(SpawnZone parent)
        {
            base.Spawn(parent);

            SpawnedObjects[SpawnedObjects.Count - 1].Destroyed += OnCrystalDestroyed;
        }

        private IEnumerator SpawnMissing()
        {
            while (SpawnedObjects.Count < spawnableEntity.startAmount)
            {
                var delay = spawnableEntity.GetSpawnDelay();

                var freePlace = generator.Tiles.Random();
                if (freePlace == null || freePlace.IsPlaceTaken)
                    continue;

                yield return new WaitForSeconds(delay);

                Spawn(freePlace);
                spawnCoroutine = null;
            }
        }

        private void OnCrystalDestroyed(EntityBase<CrystalData> crystal)
        {
            if (gameObject.activeInHierarchy == false)
                return;

            SpawnedObjects.Remove(crystal as Crystal);

            if (SpawnedObjects.Count < spawnableEntity.startAmount)
            {
                if (spawnCoroutine != null)
                    return;

                spawnCoroutine = StartCoroutine(SpawnMissing());
            }
        }
    }
}