﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SAE_Project
{
    public interface IDamageable
    {
        void DealDamage(int damage);
        Faction GetFaction();
    }

    public class PlayersHealth : MonoBehaviour, IDamageable
    {
        //Variables 
        public int _currentHealth;
        [SerializeField]
        public int _maxHealth;
        [SerializeField]
        Animator animator;
        [SerializeField] private GameObject _reload;
        // Functions
        void Update()
        {   //When the game starts the players health will always be the same as the max Health
            _currentHealth = _maxHealth;
        }
        public void DealDamage(int damage)
        {
            //When dealing damage is going to reduce your health and if your health drops to 0 you die ;)
            _currentHealth -= damage;

                

            if (_currentHealth <= 0)
            {

                animator.SetTrigger("IsDead");
                Debug.Log(_currentHealth);

                gameObject.SetActive(false);

                //Relaod Scene when press try again
                _reload.SetActive(true);


            }

            

        }

       

        //when colliding with the traps is going to destroy the player 
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Spike"))
            {
                gameObject.SetActive(false);
                _reload.SetActive(true);
            }
        }

        public Faction GetFaction()
        {
            return Faction.Player;
        }
    }

}