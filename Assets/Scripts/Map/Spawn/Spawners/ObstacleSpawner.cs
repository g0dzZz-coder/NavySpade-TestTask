using System.Linq;
using UnityEngine;

namespace NavySpade
{
    using Entities;
    using Map;

    public class ObstacleSpawner : Spawner<Obstacle, SpawnableEntityData>
    {
        private void Awake()
        {
            generator.MapUpdated += spawnZones => OnMapUpdated(spawnZones, spawnableEntity.startAmount);
        }

        protected override void Spawn(Tile parent)
        {
            base.Spawn(parent);

            if (Application.isEditor == false)
                return;

            var last = SpawnedObjects.Last();
            if (last == null)
                return;

            last.SetRandomHeight();

            Debug.Log(1);
        }
    }
}