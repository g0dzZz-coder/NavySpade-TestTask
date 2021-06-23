using UnityEditor.AI;
using UnityEngine;

namespace NavySpade.Map
{
    public class RandomMapGenerator : MapGenerator
    {
        public override void GenerateMap()
        {
            if (IsValid())
                return;

            base.GenerateMap();

            if (Application.isPlaying)
                NavMeshBuilder.BuildNavMesh();
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