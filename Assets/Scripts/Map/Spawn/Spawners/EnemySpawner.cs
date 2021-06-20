using System.Collections;
using UnityEngine;

namespace NavySpade.Map
{
    using Entities;
    using Utils;

    public class EnemySpawner : Spawner<Enemy, EnemyData>
    {
        private void Start()
        {
            StartCoroutine(PeriodicSpawn());
        }

        private IEnumerator PeriodicSpawn()
        {
            while (SpawnedObjects.Count < spawnableEntity.startAmount)
            {
                var freePlace = generator.GetTiles().Random();
                if (freePlace == null || freePlace.IsPlaceTaken)
                    continue;

                yield return new WaitForSeconds(spawnableEntity.GetSpawnDelay());

                Spawn(freePlace);
            }
        }
    }
}