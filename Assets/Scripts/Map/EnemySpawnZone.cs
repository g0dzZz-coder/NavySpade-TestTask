using UnityEngine;

namespace NavySpade.Map
{
    public class EnemySpawnZone : Tile
    {
        public override bool IsFree => false;
    }
}