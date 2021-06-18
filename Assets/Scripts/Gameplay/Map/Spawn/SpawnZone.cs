using UnityEngine;

namespace NavySpade.Gameplay
{
    public abstract class SpawnZone : MonoBehaviour
    {
        public bool IsPlaceTaken { get; protected set; }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsPlaceTaken ? Color.red : Color.green;
            Gizmos.matrix = transform.localToWorldMatrix;

            var center = new Vector3(0, transform.localScale.y/2f, 0f);
            var size = new Vector3(transform.localScale.x, 0, transform.localScale.z);

            Gizmos.DrawWireCube(center, size);
        }
    }
}