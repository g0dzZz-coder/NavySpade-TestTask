using UnityEngine;

namespace NavySpade.Gameplay.Map
{
    [CreateAssetMenu(fileName = "MapSettings", menuName = "Settings/Map", order = 51)]
    public class MapSettings : ScriptableObject
    {
        public Vector2 mapSize = new Vector2(20, 20);
        public Tile tilePrefab = null;
        [Range(0f, 1f)]
        public float outlinePrecent = 0.1f;
    }
}