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
            var freeTiles = generator.GetFreeTiles();

            if (freeTiles.Count > 0)
            {
                while (SpawnedObjects.Count < spawnableEntity.startAmount)
                {
                    var freePlace = freeTiles.Random();

                    yield return new WaitForSeconds(spawnableEntity.GetSpawnDelay());

                    Spawn(freePlace);
                }
            }
        }
    }
}