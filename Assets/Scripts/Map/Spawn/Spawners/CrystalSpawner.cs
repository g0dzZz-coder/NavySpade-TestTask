using System.Collections;
using UnityEngine;

namespace NavySpade.Map
{
    using Entities;
    using Utils;

    public class CrystalSpawner : Spawner<Crystal, CrystalData>
    {
        private Coroutine spawnCoroutine;

        private void Awake()
        {
            Level.Instance.Restarted += () => OnMapUpdated(generator.GetFreeTiles(), spawnableEntity.startAmount);
            Level.Instance.GameEnded += StopAllCoroutines;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        protected override Crystal Spawn(Tile parent)
        {
            var crystal = base.Spawn(parent);
            parent.SetChild(crystal.transform);
            crystal.Destroyed += OnCrystalDestroyed;

            return crystal;
        }

        private IEnumerator PeriodicSpawn()
        {
            var freeTiles = generator.GetFreeTiles();

            if (freeTiles.Count > 0)
            {
                while (SpawnedObjects.Count < spawnableEntity.startAmount)
                {
                    var delay = spawnableEntity.GetSpawnDelay();
                    var freePlace = freeTiles.Random();

                    yield return new WaitForSeconds(delay);

                    Spawn(freePlace);
                }

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

                spawnCoroutine = StartCoroutine(PeriodicSpawn());
            }
        }
    }
}