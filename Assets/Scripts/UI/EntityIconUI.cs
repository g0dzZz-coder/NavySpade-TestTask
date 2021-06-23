using UnityEngine;
using UnityEngine.UI;

namespace NavySpade.UI
{
    using Entities;

    public class EntityIconUI : MonoBehaviour
    {
        [SerializeField] private EntityData _data = null;
        [SerializeField] private Image _image = null;
        [SerializeField] private Canvas _canvas = null;

        private Camera _camera;

        private void Awake()
        {
            if (_data == null)
                Destroy(gameObject);

            _camera = Camera.main;
            _canvas.worldCamera = _camera;

            UpdateIcon(_data.icon);
        }

        private void Update()
        {
            LookAt();
        }

        private void UpdateIcon(Sprite icon)
        {
            if (_image == null || icon == null)
                return;

            _image.sprite = icon;
        }

        private void LookAt()
        {
            _canvas.transform.LookAt(_camera.transform);
        }
    }
}