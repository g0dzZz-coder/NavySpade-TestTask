﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NavySpade.Map
{
    [Serializable]
    public struct TileType
    {
        public Tile prefab;
        [Range(0, 10)]
        public int priority;
    }

    [CreateAssetMenu(fileName = "MapSettings", menuName = "Settings/Map", order = 51)]
    public class MapSettings : ScriptableObject
    {
        public Vector2 mapSize = new Vector2(20, 20);

        [SerializeField] private Tile tilePrefab = null;
        [SerializeField] private EnemySpawnZone spawnZonePrefab = null;
        [Range(0f, 1f)]
        [SerializeField] private float spawnZoneFraquency = 0.2f;

        public Tile GetRandomPrefab()
        {
            if (Random.Range(0f, 1f) > spawnZoneFraquency)
                return tilePrefab;

            return spawnZonePrefab;
        }
    }
}