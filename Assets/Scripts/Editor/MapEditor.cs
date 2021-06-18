using UnityEditor;

namespace NavySpade.Editor
{
    using Gameplay.Map;
    using UnityEngine;
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(MapGenerator))]
    public class MapEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Application.isPlaying)
                return;

            var map = target as MapGenerator;
            map.GenerateMap();
        }
    }
}