using System;
using System.Collections.Generic;
using UnityEngine;

namespace NavySpade.Gameplay.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapSettings settings = null;
        [SerializeField] private Transform root = null;

        public List<Tile> Tiles { get; private set; } = new List<Tile>();

        public event Action<List<Tile>> MapUpdated;

        private Transform Root => root ? root : transform;

        private void Start()
        {
            GenerateMap();
        }

        public void GenerateMap()
        {
            Clear();

            if (settings == null)
                return;

            var i = 0;
            for (var x = 0; x < settings.mapSize.x; x++)
            {
                for (var z = 0; z < settings.mapSize.y; z++)
                {
                    Tiles.Add(CreateTile(x, z));
                    i++;
                }
            }

            MapUpdated?.Invoke(Tiles);

            Debug.Log($"Map Generated. Number of tiles = {i}");
        }

        private Tile CreateTile(int x, int z)
        {
            var tileScale = settings.tilePrefab.transform.localScale;
            var position = new Vector3(-settings.mapSize.x / 2f + tileScale.x + x, -tileScale.y / 2f, -settings.mapSize.y / 2f + tileScale.z + z);

            var newTile = Instantiate(settings.tilePrefab, position, Quaternion.identity, Root);
            newTile.transform.localScale = Vector3.one * (1f - settings.outlinePrecent);
            newTile.gameObject.name += $": [{x};{z}]";

            return newTile;
        }

        private void Clear()
        {
            for (var i = 0; i < Root.childCount; i++)
            {
                if (Application.isPlaying)
                    Destroy(Root.GetChild(i).gameObject);
                else
                    DestroyImmediate(Root.GetChild(i).gameObject);
            }

            Tiles.Clear();
        }
    }
}