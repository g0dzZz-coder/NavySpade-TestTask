using System.Linq;

namespace NavySpade
{
    using Entities;

    public class ObstacleSpawner : Spawner<Obstacle, SpawnableEntityData>
    {
        private void Awake()
        {
            generator.MapUpdated += spawnZones => OnMapUpdated(spawnZones, spawnableEntity.startAmount);
        }

        protected override void Spawn(SpawnZone parent)
        {
            base.Spawn(parent);

            var last = SpawnedObjects.Last();
            if (last == null)
                return;

            parent.SetChild(last.gameObject);
            last.SetRandomHeight();
        }
    }
}