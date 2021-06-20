using System.Collections;
using UnityEngine;

namespace NavySpade.Map
{
    using Entities;
    using Utils;

    public class CrystalSpawner : Spawner<Crystal, CrystalData>
    {
        private Coroutine spawnCoroutine;

        private void Start()
        {
            OnMapUpdated(generator.GetFreeTiles(), spawnableEntity.startAmount);

            generator.MapUpdated += spawnZones => OnMapUpdated(spawnZones, spawnableEntity.startAmount);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        protected override void Spawn(Tile parent)
        {
            base.Spawn(parent);

            SpawnedObjects[SpawnedObjects.Count - 1].Destroyed += OnCrystalDestroyed;
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