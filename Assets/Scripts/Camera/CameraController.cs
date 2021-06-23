using UnityEngine;

namespace NavySpade.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target = null;
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private Vector3 _offset = new Vector3(10, 10, 10);

        private void LateUpdate()
        {
            if (_target == null)
                return;

            LookAt(_target);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void LookAt(Transform target)
        {
            transform.LookAt(target);

            var desiredPosition = target.position + _offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}