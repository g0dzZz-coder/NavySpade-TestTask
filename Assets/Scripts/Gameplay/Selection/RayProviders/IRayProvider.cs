using UnityEngine;

namespace NavySpade.Gameplay
{
    public interface IRayProvider
    {
        Ray CreateRay();
        bool IsInteracted();
    }
}