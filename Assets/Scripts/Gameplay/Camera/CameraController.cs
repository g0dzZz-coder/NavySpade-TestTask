using System.Collections.Generic;
using UnityEngine;

namespace NavySpade.Gameplay.Cameras
{
    using Core;

    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] List<CameraState> states = new List<CameraState>();

        private CameraState currentState;
        private int currentStateIndex;

        private void Awake()
        {
            enabled = true;
            SetNextState();

            Game.Restarted += SetNextState;
            Game.Ended += win => SetNextState();
        }

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

        private void SetNextState()
        {
            if (currentState == null)
            {
                SetState(0);
                return;
            }

            var nextIndex = currentStateIndex < states.Count - 1 ? currentStateIndex + 1 : 0;
            SetState(nextIndex);
        }

        private void SetState(int index)
        {
            if (index < 0 || index > states.Count - 1)
                return;

            currentStateIndex = index;
            currentState = states[currentStateIndex];
        }

        private void LookAt(Transform target)
        {
            transform.LookAt(target);

            if (currentState == null)
                return;

            var desiredPosition = target.position + currentState.offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, currentState.smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}