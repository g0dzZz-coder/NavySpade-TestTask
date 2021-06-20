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
        [SerializeField] private CrystalSpawner crystalContainer = null;
        [SerializeField] private LevelInfoUI crystalInfo = null;

        [SerializeField] private EnemySpawner enemyContainer = null;
        [SerializeField] private LevelInfoUI enemyInfo = null;

        private void LateUpdate()
        {
            var heroPosition = Level.Instance.Hero.transform.position;
            OnCrystalDistanceChanged(crystalContainer.SpawnedObjects.GetClosestDistance(heroPosition));
            OnEnemyDistanceChanged(enemyContainer.SpawnedObjects.GetClosestDistance(heroPosition));
        }

        private void Start()
        {
            OnCrystalCountChanged(null, null);
            OnEnemyCountChanged(null, null);

            crystalContainer.SpawnedObjects.CollectionChanged += OnCrystalCountChanged;
            enemyContainer.SpawnedObjects.CollectionChanged += OnEnemyCountChanged;
            Level.Instance.Restarted += Show;
            Level.Instance.GameEnded += Hide;
        }

        private void OnCrystalCountChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            crystalInfo.countText.text = $"x{crystalContainer.SpawnedObjects.Count}";
        }

        private void OnEnemyCountChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            enemyInfo.countText.text = $"x{enemyContainer.SpawnedObjects.Count}";
        }

        private void OnCrystalDistanceChanged(float distance)
        {
            crystalInfo.distanceText.text = PrepareDistanceValue(distance).ToString();
        }

        private void OnEnemyDistanceChanged(float distance)
        {
            enemyInfo.distanceText.text = PrepareDistanceValue(distance).ToString();
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