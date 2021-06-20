using UnityEngine;

namespace NavySpade.Map
{
    using Entities;

    public class Tile : SpawnZone, ISelectable
    {
        public bool IsFreeToMove()
        {
            if (Child.GetComponent<Obstacle>())
                return false;

            return true;
        }
    }
}