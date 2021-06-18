using UnityEngine;

namespace NavySpade.Gameplay
{
    public interface ISelector
    {
        void Check(Ray ray);
        Transform GetSelection();
    }
}