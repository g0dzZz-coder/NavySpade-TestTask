using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Map.Generation
{
    public class RandomMapGenerator : MapGenerator
    {
        public override void GenerateMap()
        {
            if (IsValid())
                return;

            base.GenerateMap();

            Tiles.LastOrDefault().GetComponent<NavMeshSurface>().BuildNavMesh();
        }

        protected override Tile GetPrefab(int x, int z)
        {
            return settings.GetRandomPrefab();
        }

        private bool IsValid()
        {
            if (Tiles.Count + SpawnZones.Count < settings.mapSize.x * settings.mapSize.y)
                return false;

            return true;
        }
    }
}