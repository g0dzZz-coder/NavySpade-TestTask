using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NavySpade.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapSettings settings = null;
        [SerializeField] private Transform root = null;

        public List<Tile> Tiles { get; private set; } = new List<Tile>();
        public List<EnemySpawnZone> SpawnZones { get; private set; } = new List<EnemySpawnZone>();

        private Transform Root => root ? root : transform;

        public event Action<List<Tile>> MapUpdated;

        public void OnRestarted()
        {
            Tiles = Root.GetComponentsInChildren<Tile>().ToList();
            SpawnZones = Root.GetComponentsInChildren<EnemySpawnZone>().ToList();

            MapUpdated?.Invoke(Tiles);
        }

        public List<Tile> GetFreeTiles()
        {
            if (Tiles == null || Tiles.Count == 0)
                Tiles = Root.GetComponentsInChildren<Tile>().ToList();

            var freeTiles = Tiles.Where(x => x.IsFree).ToList();

            return freeTiles;
        }

        public void GenerateMap()
        {
            if (IsValid())
                return;

            Clear();

            if (settings == null)
                return;

            var i = 0;
            for (var x = 0; x < settings.mapSize.x; x++)
            {
                for (var z = 0; z < settings.mapSize.y; z++)
                {
                    var prefab = settings.GetRandomPrefab();
                    if (prefab is Tile)
                        CreateTile(prefab as Tile, x, z);
                    else
                        CreateSpawnZone(prefab as EnemySpawnZone, x, z);

                    i++;
                }
            }

            MapUpdated?.Invoke(Tiles);

            Debug.Log($"Map Generated. Number of Tiles = {i}");

            //if (Application.isPlaying)
            //    NavMeshBuilder.BuildNavMesh();
        }

        public void Clear()
        {
            if (Application.isEditor)
            {
                Tiles = Root.GetComponentsInChildren<Tile>().ToList();
                SpawnZones = Root.GetComponentsInChildren<EnemySpawnZone>().ToList();
            }

            foreach (Tile tile in Tiles)
            {
                if (Application.isEditor)
                    DestroyImmediate(tile.gameObject);
                else
                    Destroy(tile.gameObject);
            }

            Tiles.Clear();

            foreach (EnemySpawnZone zone in SpawnZones)
            {
                if (Application.isEditor)
                    DestroyImmediate(zone.gameObject);
                else
                    Destroy(zone.gameObject);
            }

            SpawnZones.Clear();
        }

        private void CreateTile(Tile prefab, int x, int z)
        {
            var scale = prefab.transform.localScale;
            var position = new Vector3(-settings.mapSize.x / 2f + scale.x + x, -scale.y / 2f, -settings.mapSize.y / 2f + scale.z + z);
            var tile = Instantiate(prefab, position, Quaternion.identity, Root);
            tile.gameObject.name += $": [{x};{z}]";

            Tiles.Add(tile);
        }

        private void CreateSpawnZone(EnemySpawnZone prefab, int x, int z)
        {
            var scale = prefab.transform.localScale;
            var position = new Vector3(-settings.mapSize.x / 2f + scale.x + x, -scale.y / 2f, -settings.mapSize.y / 2f + scale.z + z);
            var spawnZone = Instantiate(prefab, position, Quaternion.identity, Root);
            spawnZone.gameObject.name += $": [{x};{z}]";

            SpawnZones.Add(spawnZone);
        }

        private bool IsValid()
        {
            if (Tiles.Count + SpawnZones.Count < settings.mapSize.x * settings.mapSize.y)
                return false;

            return true;
        }
    }
}