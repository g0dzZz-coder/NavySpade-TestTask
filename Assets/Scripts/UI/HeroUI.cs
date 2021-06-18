using System;
using TMPro;
using UnityEngine;

namespace NavySpade.UI
{
    using Gameplay.Entities;
    using Core;

    public class HeroUI : MonoBehaviour
    {
        [SerializeField] HeroData hero = null;
        [SerializeField] TMP_Text livesText = null;
        [SerializeField] TMP_Text scoreText = null;

        private void Awake()
        {
            Hide();

            Game.Restarted += Show;
        }

        private void OnEnable()
        {
            UpdateLives(hero.Lives);

            hero.LivesUpdated += UpdateLives;
        }

        private void OnDisable()
        {
            hero.LivesUpdated -= UpdateLives;
        }

        private void UpdateLives(int livesCount)
        {
            livesText.text = livesCount.ToString();
        }

        private void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnValidate()
        {
            if (livesText == null || scoreText == null)
                throw new NullReferenceException();
        }
    }
}