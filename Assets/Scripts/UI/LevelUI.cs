using System;
using TMPro;
using UnityEngine;

namespace NavySpade.UI
{
    using Map;

    [Serializable]
    public class InfoUI
    {
        public TMP_Text countText = null;
        public TMP_Text distanceText = null;
    }

    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private CrystalSpawner crystalSpawner = null;
        [SerializeField] private InfoUI crystalInfo = null;

        [SerializeField] private EnemySpawner enemySpawner = null;
        [SerializeField] private InfoUI enemyInfo = null;

        private void Awake()
        {
            crystalSpawner.EntitySpanwed.AddListener(UpdateCrystalInfo);
            enemySpawner.EntitySpanwed.AddListener(UpdateEnemyInfo);
        }

        private void UpdateCrystalInfo()
        {
            crystalInfo.countText.text = crystalSpawner.SpawnedObjects.Count.ToString();
            crystalInfo.distanceText.text = "";
        }

        private void UpdateEnemyInfo()
        {
            enemyInfo.countText.text = enemySpawner.SpawnedObjects.Count.ToString();
            enemyInfo.distanceText.text = "";
        }
    }
}