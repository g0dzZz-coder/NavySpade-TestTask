using UnityEngine;

namespace NavySpade
{
    using Entities;
    using Map;

    public class ObstacleSpawner : Spawner<Obstacle, SpawnableEntityData>
    {
        private void Awake()
        {
            generator.MapUpdated += tiles => OnMapUpdated(generator.GetFreeTiles(), spawnableEntity.startAmount);
        }

        protected override Obstacle Spawn(Tile parent)
        {
            if (parent is EnemySpawnZone)
                return null;

            var obstacle = base.Spawn(parent);
            parent.SetChild(obstacle.transform);

            obstacle.SetRandomHeight();

            return obstacle;
        }
    }
}