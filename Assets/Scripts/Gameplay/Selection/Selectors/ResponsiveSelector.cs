using UnityEngine;

namespace NavySpade.Gameplay
{
    public class ResponsiveSelector : MonoBehaviour, ISelector
    {
        [SerializeField] private Selectable[] selectables = null;
        [SerializeField] private float threshold = 0.97f;

        private Transform selection;

        private void Awake()
        {
            selectables = FindObjectsOfType<Selectable>();
        }

        public void Check(Ray ray)
        {
            selection = null;

            var closest = 0f;
            for (var i = 0; i < selectables.Length; i++)
            {
                var vector1 = ray.direction;
                var vector2 = selectables[i].transform.position - ray.origin;

                var lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);
                selectables[i].LookPercentage = lookPercentage;

                if (lookPercentage > threshold && lookPercentage > closest)
                {
                    closest = lookPercentage;
                    selection = selectables[i].transform;
                }
            }
        }

        public Transform GetSelection()
        {
            return selection;
        }
    }
}