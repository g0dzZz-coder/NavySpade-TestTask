using UnityEngine;

namespace NavySpade.Gameplay
{
    public interface ISelectionResponse
    {
        void OnSelect(Transform transform);
        void OnDeselect(Transform transform);
    }
}