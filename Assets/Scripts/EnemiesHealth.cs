using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SAE_Project
{
    public class EnemiesHealth : MonoBehaviour, IDamageable
    {   //Variables
        [SerializeField]
        private int _maxHealth;
        [SerializeField]
        private int _currentHealth;
        [SerializeField]
        private Animator _animator;

        public AudioSource Dead;
        void Start()
        {
            _currentHealth = _maxHealth;
        }

        // Functions
        public void DealDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
                Dead.Play();
            _animator.SetBool("Dead", true);
                StartCoroutine(Deactive());

            }
        }

        void Die()
        {

            //yield return new WaitForSeconds(3);
            //Destroy(gameObject);
            //_animator.SetBool("Dead", true);
        }
        IEnumerator Deactive()
        {
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }

        public Faction GetFaction()
        {
            return Faction.Enemy;
        }
    }
}