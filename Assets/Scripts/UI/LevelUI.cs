using System;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;

namespace NavySpade.UI
{
    using Map;
    using Utils;

    [Serializable]
    public class LevelInfoUI
    {
        public TMP_Text countText = null;
        public TMP_Text distanceText = null;
    }

    public class LevelUI : UIElement
    {
        [SerializeField] private CrystalSpawner _crystalContainer = null;
        [SerializeField] private LevelInfoUI _crystalInfo = null;

        [SerializeField] private EnemySpawner _enemyContainer = null;
        [SerializeField] private LevelInfoUI _enemyInfo = null;

        private void LateUpdate()
        {
            var heroPosition = Level.Instance.Hero.transform.position;
            OnCrystalDistanceChanged(_crystalContainer.SpawnedObjects.GetClosestDistance(heroPosition));
            OnEnemyDistanceChanged(_enemyContainer.SpawnedObjects.GetClosestDistance(heroPosition));
        }

        private void Start()
        {
            OnCrystalCountChanged(null, null);
            OnEnemyCountChanged(null, null);

            _crystalContainer.SpawnedObjects.CollectionChanged += OnCrystalCountChanged;
            _enemyContainer.SpawnedObjects.CollectionChanged += OnEnemyCountChanged;

            Level.Instance.Restarted += Show;
            Level.Instance.GameEnded += Hide;
        }

        private void OnCrystalCountChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _crystalInfo.countText.text = $"x{_crystalContainer.SpawnedObjects.Count}";
        }

        private void OnEnemyCountChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _enemyInfo.countText.text = $"x{_enemyContainer.SpawnedObjects.Count}";
        }

        private void OnCrystalDistanceChanged(float distance)
        {
            _crystalInfo.distanceText.text = PrepareDistanceValue(distance).ToString();
        }

        private void OnEnemyDistanceChanged(float distance)
        {
            _enemyInfo.distanceText.text = PrepareDistanceValue(distance).ToString();
        }

        private int PrepareDistanceValue(float distance)
        {
            int value;
            if (distance > int.MaxValue)
                value = 0;
            else
                value = Mathf.RoundToInt(distance);

            return value;
        }
    }
}