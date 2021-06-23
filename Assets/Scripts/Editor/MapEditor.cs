using UnityEditor;
using UnityEngine;

namespace NavySpade.Editor
{
    using Map.Generation;
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(MapGenerator), true)]
    public class MapEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Application.isPlaying)
                return;

            var map = target as MapGenerator;

            if (GUILayout.Button("Generate"))
            {
                map.GenerateMap();
            }

            if (GUILayout.Button("Clear"))
            {
                map.Clear();
            }
        }
    }
}