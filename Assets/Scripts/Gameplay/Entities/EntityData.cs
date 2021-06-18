using UnityEngine;

namespace NavySpade.Gameplay.Entities
{
    public abstract class EntityData : ScriptableObject
    {
        public GameObject prefab = null;
        public Sprite icon = null;
    }
}