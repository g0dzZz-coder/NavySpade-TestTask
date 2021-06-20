using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavySpade
{
    using Core;
    using Entities;
    using Map;

    public class Level : MonoBehaviour
    {
        [SerializeField] private Hero hero = null;
        [SerializeField] private Tile heroStartPosition = null;

        private void Awake()
        {
            if (hero == null)
                return;

            hero.transform.position = new Vector3(heroStartPosition.transform.position.x, 0f, heroStartPosition.transform.position.z);
        }
    }
}