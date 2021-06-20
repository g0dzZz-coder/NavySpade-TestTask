﻿using System.Collections;
using UnityEngine;

namespace NavySpade.Map
{
    using Entities;
    using Utils;

    public class EnemySpawner : Spawner<Enemy, EnemyData>
    {
        private void Awake()
        {
            Level.Instance.Restarted += StartSpawn;
            Level.Instance.GameEnded += StopSpawn;
        }

        protected override Enemy Spawn(Tile parent)
        {
            var enemy = base.Spawn(parent);
            enemy.transform.SetParent(root ? root : transform);

            return enemy;
        }

        private void StartSpawn()
        {
            StopSpawn();
            StartCoroutine(PeriodicSpawn());
        }

        private void StopSpawn()
        {
            RemoveAllObjects();
            StopAllCoroutines();
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