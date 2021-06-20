using System;
using UnityEngine;

namespace NavySpade.Entities
{
    using Input;

    public class HeroInputReceiver : MonoBehaviour
    {
        [SerializeField] private Camera _camera = null;

        public event Action<Vector3> TargetSelected;

        private void Start()
        {
            InputManager.Instance.Controls.Hero.Click.performed += context => OnClick();
        }

        private void OnClick()
        {
            var screenPosition = InputManager.Instance.Controls.Hero.PointerPosition.ReadValue<Vector2>();
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