using UnityEngine;

namespace NavySpade.Entities
{
    [CreateAssetMenu(fileName = "Entity", menuName = "Settings/Entity", order = 51)]
    public class EntityData : ScriptableObject
    {
        [Header("General")]
        public GameObject prefab = null;
        public Sprite icon = null;
    }
}