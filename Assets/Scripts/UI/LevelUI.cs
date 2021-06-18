using System;
using TMPro;
using UnityEngine;

namespace NavySpade.UI
{
    [Serializable]
    public class InfoUI
    {
        public TMP_Text countText = null;
        public TMP_Text distanceText = null;
    }

    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private InfoUI crystalInfo = null;
        [SerializeField] private InfoUI enemyInfo = null;

        private void UpdateCrystalInfo()
        {
            crystalInfo.countText.text = "";
            crystalInfo.distanceText.text = "";
        }

        private void UpdateEnemyInfo()
        {
            enemyInfo.countText.text = "";
            enemyInfo.distanceText.text = "";
        }
    }
}