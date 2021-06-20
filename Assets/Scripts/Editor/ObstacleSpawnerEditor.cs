using NavySpade.Map;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NavySpade.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(ObstacleSpawner))]
    public class ObstacleSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var spawner = target as ObstacleSpawner;

            if (GUILayout.Button("Spawn"))
            {
                var tiles = FindObjectsOfType<Tile>();
                spawner.OnMapUpdated(tiles.ToList(), spawner.Data.startAmount);
            }

            if (GUILayout.Button("Clear"))
            {
                spawner.RemoveAllObjects();
            }
        }
    }
}