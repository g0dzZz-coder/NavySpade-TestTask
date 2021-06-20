﻿using TMPro;
using UnityEngine;

namespace NavySpade.UI
{
    using Player;

    public class PlayerUI : UIElement
    {
        [SerializeField] private TMP_Text scoreText = null;

        private void Start()
        {
            UpdateScore(Player.Score);

            Level.Instance.Restarted += Show;
            Level.Instance.GameEnded += Hide;
            Player.ScoreUpdated += UpdateScore;
        }

        private void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}