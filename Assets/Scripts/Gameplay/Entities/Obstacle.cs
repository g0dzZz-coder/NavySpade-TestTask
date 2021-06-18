﻿using UnityEngine;

namespace NavySpade.Gameplay
{
    public class Obstacle : MonoBehaviour
    {
        [MinMaxSlider(0f, 1f)]
        [SerializeField] private Vector2 minMaxScales = new Vector2(0.1f, 0.5f);

        private void Awake()
        {
            SetRandomHeight();
        }

        private void SetRandomHeight()
        {
            var height = Random.Range(minMaxScales.x, minMaxScales.y);
            transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, height / 2f, transform.position.z);
        }
    }
}