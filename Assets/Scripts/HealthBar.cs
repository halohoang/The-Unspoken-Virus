using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SAE_Project.Assets.Scripts
{
    public class HealthBar : MonoBehaviour
    {



        [SerializeField]
        private Image _foreground;

        [SerializeField]
        private BossHealth _health;

        private void OnEnable()
        {
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(float newHealth)
        {
            _foreground.fillAmount = newHealth;
        }


    }
}
