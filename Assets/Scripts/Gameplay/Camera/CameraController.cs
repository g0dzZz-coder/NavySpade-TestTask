using UnityEngine;

namespace NavySpade.Gameplay.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] private float smoothSpeed = 0.125f;
        [SerializeField] private Vector3 offset = new Vector3(10, 10, 10);

        private void LateUpdate()
        {
            if (target == null)
                return;

            LookAt(target);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        private void LookAt(Transform target)
        {
            transform.LookAt(target);

            var desiredPosition = target.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}