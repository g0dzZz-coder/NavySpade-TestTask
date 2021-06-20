using System;
using UnityEngine;

namespace NavySpade
{
    using Cameras;
    using Entities;
    using Map;
    using Utils;

    public class Level : MonoSingleton<Level>
    {
        [SerializeField] private Hero hero = null;
        [SerializeField] private MapGenerator map = null;
        [SerializeField] private CameraController _camera = null;

        public Hero Hero => hero;

        public event Action Restarted;
        public event Action GameEnded;

        protected override void Start()
        {
            base.Start();

            _camera.SetTarget(hero.transform);
            Restart();

            hero.HealthController.Died += EndGame;
        }

        public void Restart()
        {
            Player.Player.ResetScore();
            map.OnRestarted();

            Restarted?.Invoke();
        }

        public void EndGame()
        {
            GameEnded?.Invoke();
        }
    }
}