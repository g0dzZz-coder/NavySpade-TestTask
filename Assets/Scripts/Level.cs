using System;
using UnityEngine;

namespace NavySpade
{
    using Cameras;
    using Entities.Hero;
    using Map;
    using Map.Generation;
    using Utils;

    public class Level : MonoSingleton<Level>
    {
        [SerializeField] private Hero _hero = null;
        [SerializeField] private MapGenerator _map = null;
        [SerializeField] private CameraController _camera = null;
        [SerializeField] private Tile _heroStartPoint = null;

        public Hero Hero => _hero;

        public event Action Restarted;
        public event Action GameEnded;

        protected override void Start()
        {
            base.Start();

            _camera.SetTarget(_hero.transform);
            Restart();

            _hero.HealthController.Died += EndGame;
        }

        public void Restart()
        {
            Player.Player.ResetScore();
            _map.GenerateMap();

            Restarted?.Invoke();
        }

        public void EndGame()
        {
            GameEnded?.Invoke();
        }

        public Tile GetHeroStartPoint()
        {
            if (_heroStartPoint == null)
                _heroStartPoint = _map.GetFreeTiles().Random();

            return _heroStartPoint;
        }
    }
}