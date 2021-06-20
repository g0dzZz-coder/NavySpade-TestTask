﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.AI;
using UnityEngine;

namespace NavySpade.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapSettings settings = null;
        [SerializeField] private Transform root = null;

        public event Action<List<Tile>> MapUpdated;

        private Transform Root => root ? root : transform;

        private List<Tile> tiles = new List<Tile>();

        public List<Tile> GetTiles()
        {
            if (tiles == null || tiles.Count == 0)
                return GetComponentsInChildren<Tile>().ToList();

            return tiles;
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
                    tiles.Add(CreateTile(x, z));
                    i++;
                }
            }

            MapUpdated?.Invoke(tiles);

            Debug.Log($"Map Generated. Number of tiles = {i}");

            if (Application.isPlaying)
                NavMeshBuilder.BuildNavMesh();
        }

        private Tile CreateTile(int x, int z)
        {
            var tileScale = settings.tilePrefab.transform.localScale;
            var position = new Vector3(-settings.mapSize.x / 2f + tileScale.x + x, -tileScale.y / 2f, -settings.mapSize.y / 2f + tileScale.z + z);

            var newTile = Instantiate(settings.tilePrefab, position, Quaternion.identity, Root);
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

            tiles.Clear();
        }

        private bool IsValid()
        {
            if (tiles.Count < settings.mapSize.x * settings.mapSize.y)
                return false;

            return true;
        }
    }
}