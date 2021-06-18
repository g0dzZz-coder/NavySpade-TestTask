using System;
using TMPro;
using UnityEngine;

namespace NavySpade.UI
{
    using Core;

    public class PlayerUI : UIElement
    {
        [SerializeField] private TMP_Text scoreText = null;

        private void Start()
        {
            Show();
        }

        private void OnEnable()
        {
            UpdateScore(Player.Score);

            Player.ScoreUpdated += UpdateScore;
        }

        private void OnDisable()
        {
            Player.ScoreUpdated -= UpdateScore;
        }

        private void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }

        private void OnValidate()
        {
            if (scoreText == null)
                throw new NullReferenceException();
        }
    }
}