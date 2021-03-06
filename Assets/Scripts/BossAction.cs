﻿using SAE_Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Project
{

    public class BossAction : MonoBehaviour
    {
        public float _speed;

        public float _startWaitTime;

        private float _waitTime;

        public Transform[] _moveSpots;

        private int _randomSpot;

        private float _timeBtwShots;

        public float _startTimeBtwShots;

        public GameObject _projectile;

        public Transform _player;

        [SerializeField]
        Animator animator;
        //Sound Effect
        public AudioSource Attack;
        public AudioSource Laugh;
        public AudioSource MoveSoundCue;

        [SerializeField]
        private float _waitSecond;


        void Start()
        {
            _waitTime = _startWaitTime;
            _randomSpot = Random.Range(0, _moveSpots.Length);
            gameObject.GetComponent<AudioSource>();

        }

        IEnumerator SoundCue()
        {
            MoveSoundCue.Play();
            yield return new WaitForSeconds(_waitSecond);
        }

        void Update()
        {
            Laugh.Play();
            transform.position = Vector2.MoveTowards(transform.position, _moveSpots[_randomSpot].position, _speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _moveSpots[_randomSpot].position) < 0.2f)
            {
                if (_waitTime <= 0)
                {
                    StartCoroutine(SoundCue());
                    _randomSpot = Random.Range(0, _moveSpots.Length);
                    _waitTime = _startWaitTime;


                }
                else
                {
                    _waitTime -= Time.deltaTime;

                }

            }

            Casting();
            FlipSprite();
        }

        public void FlipSprite()
        {
            if (_player.position.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

        }

        public void Casting()
        {
            if (_timeBtwShots <= 0)
            {
                Attack.Play();
                Instantiate(_projectile, transform.position, Quaternion.identity);
                _timeBtwShots = _startTimeBtwShots;
                animator.SetTrigger("Casting");
            }
            else
            {
                _timeBtwShots -= Time.deltaTime;
            }
        }


    }

}