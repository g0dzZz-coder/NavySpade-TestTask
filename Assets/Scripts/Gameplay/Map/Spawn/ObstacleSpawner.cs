using System.Collections.Generic;
using UnityEngine;

namespace NavySpade.Gameplay
{
    using Entities;
    using Map;
    using System.Collections;
    using Utils;

    public class ObstacleSpawner : Spawner<SpawnableEntityData>
    {
        private List<Obstacle> obstacles = new List<Obstacle>();

        private void Awake()
        {
            generator.MapUpdated += spawnZones => StartCoroutine(SpawnAllObstacles(spawnZones));
        }

        protected override void Spawn(Transform parent)
        {
            var obstacle = Instantiate(spawnableEntity.prefab, parent).GetComponent<Obstacle>();
            obstacle.transform.SetParent(root);
            obstacle.name = nameof(Obstacle);

            obstacles.Add(obstacle);
        }

        private IEnumerator SpawnAllObstacles(List<Tile> tiles)
        {
            RemoveAllObstacles();

            var amount = 0;
            while (amount < spawnableEntity.maxCount)
            {
                var freePlace = tiles.Random();
                if (freePlace == null)
                    yield return null;

                Spawn(freePlace.transform);

                amount++;
                if (amount >= spawnableEntity.maxCount)
                    yield return null;
            }
        }

        private void RemoveAllObstacles()
        {
            foreach (var obstacle in obstacles)
            {
                Destroy(obstacle.gameObject);
            }
        }
    }
}