using UnityEngine;
using UnityEngine.UI;

namespace NavySpade.UI
{
    using Gameplay.Entities;

    public class EntityIconUI : MonoBehaviour
    {
        [SerializeField] private EntityData data = null;
        [SerializeField] private Image image = null;
        [SerializeField] private Canvas canvas = null;

        private Camera _camera;

        private void Awake()
        {
            if (data == null)
                Destroy(gameObject);

            _camera = Camera.main;
            canvas.worldCamera = _camera;

            UpdateIcon(data.icon);
        }

        private void Update()
        {
            LookAt();
        }

        private void UpdateIcon(Sprite icon)
        {
            if (image == null || icon == null)
                return;

            image.sprite = icon;
        }

        private void LookAt()
        {
            canvas.transform.LookAt(_camera.transform);
        }
    }
}