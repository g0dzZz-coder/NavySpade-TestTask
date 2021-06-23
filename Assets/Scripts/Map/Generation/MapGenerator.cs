using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NavySpade.Map.Generation
{
    public abstract class MapGenerator : MonoBehaviour
    {
        [SerializeField] protected MapSettings settings = null;
        [SerializeField] private Transform _root = null;

        public List<Tile> Tiles { get; protected set; } = new List<Tile>();
        public List<EnemySpawnZone> SpawnZones { get; protected set; } = new List<EnemySpawnZone>();
        protected Transform Root => _root ? _root : transform;

        public Action<List<Tile>> MapUpdated { get; set; }

        public virtual void GenerateMap()
        {
            Clear();

            if (settings == null)
                return;

            for (var x = 0; x < settings.mapSize.x; x++)
            {
                for (var z = 0; z < settings.mapSize.y; z++)
                {
                    CreateTile(x, z);
                }
            }

            MapUpdated?.Invoke(Tiles);

            Debug.Log($"Map Generated. Number of Tiles = {Tiles.Count}");
        }

        public void Clear()
        {
            if (Application.isEditor)
                Tiles = Root.GetComponentsInChildren<Tile>().ToList();

            foreach (Tile tile in Tiles)
            {
                if (Application.isEditor)
                    DestroyImmediate(tile.gameObject);
                else
                    Destroy(tile.gameObject);
            }

            Tiles.Clear();
            SpawnZones.Clear();
        }

        public List<Tile> GetFreeTiles()
        {
            if (Tiles == null || Tiles.Count == 0)
                Tiles = Root.GetComponentsInChildren<Tile>().ToList();

            var freeTiles = Tiles.Where(x => x.IsFree).ToList();

            return freeTiles;
        }

        protected abstract Tile GetPrefab(int x, int z);

        protected void CreateTile(int x, int z)
        {
            var prefab = GetPrefab(x, z);
            var scale = prefab.transform.localScale;
            var position = new Vector3(-settings.mapSize.x / 2f + scale.x + x, -scale.y / 2f, -settings.mapSize.y / 2f + scale.z + z);
            var tile = Instantiate(prefab, position, Quaternion.identity, Root);
            tile.gameObject.name += $": [{x};{z}]";

            Tiles.Add(tile);

            if (tile.TryGetComponent(out EnemySpawnZone spawnZone))
                SpawnZones.Add(spawnZone);
        }
    }
}