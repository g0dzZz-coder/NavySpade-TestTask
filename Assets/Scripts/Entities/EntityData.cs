using UnityEngine;

namespace NavySpade.Entities
{
    public abstract class EntityData : ScriptableObject
    {
        [Header("General")]
        public GameObject prefab = null;
        public Sprite icon = null;
    }
}