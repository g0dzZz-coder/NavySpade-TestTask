using UnityEngine;

namespace NavySpade
{
    using Entities;
    using Map;

    public class ObstacleSpawner : Spawner<Obstacle, SpawnableEntityData>
    {
        protected override Obstacle Spawn(Tile parent)
        {
            var obstacle = base.Spawn(parent);
            parent.SetChild(obstacle.transform);

            if (Application.isEditor)
                obstacle.SetRandomHeight();

            return obstacle;
        }
    }
}