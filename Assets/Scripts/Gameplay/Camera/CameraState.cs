using UnityEngine;

namespace NavySpade.Gameplay.Cameras
{
    [CreateAssetMenu(fileName = "CameraState", menuName = "Settings/CameraState", order = 51)]
    public class CameraState : ScriptableObject
    {
        public float smoothSpeed = 0.125f;
        public Vector3 offset = new Vector3(5, 5, 5);
    }
}