using System;
using UnityEngine;

namespace NavySpade.Entities
{
    public class HeroInputReceiver : MonoBehaviour
    {
        [SerializeField] private Camera _camera = null;

        public Controls Controls
        {
            get
            {
                if (controls == null)
                    controls = new Controls();

                return controls;
            }
        }
        private Controls controls;

        public event Action<Vector3> TargetSelected;

        private void OnEnable()
        {
            Controls.Hero.Click.performed += context => OnClick();
            Controls.Hero.Enable();
        }

        private void OnDisable()
        {
            Controls.Hero.Click.performed -= context => OnClick();
            Controls.Hero.Disable();
        }

        private void OnClick()
        {
            var screenPosition = Controls.Hero.PointerPosition.ReadValue<Vector2>();
            var ray = _camera.ScreenPointToRay(screenPosition);
            Check(ray);
        }

        private void Check(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit) == false)
                return;

            if (hit.transform.TryGetComponent(out ISelectable selectable))
                TargetSelected?.Invoke(hit.transform.position);
        }
    }
}