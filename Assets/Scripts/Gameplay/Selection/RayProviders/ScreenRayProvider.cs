using UnityEngine;

namespace NavySpade.Gameplay
{
    public class ScreenRayProvider : MonoBehaviour, IRayProvider
    {
        public Ray CreateRay()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                return Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            else
                return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        public bool IsInteracted()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                return Input.touchCount > 0 ? true : false;
            else
                return Input.GetMouseButtonDown(0);
        }
    }
}