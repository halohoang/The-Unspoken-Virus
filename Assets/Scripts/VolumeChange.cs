using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace SAE_Project.Assets.Scripts
{
    class VolumeChange : MonoBehaviour
    {
        //Variables
        private AudioSource _audioSource;

        private float _musicVolume = 0.1859943f;

        //functions
        private void Start()
        {
            //Assign AudioSource component to control it
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            //Setting volume option of AudioSource to be equal to _musicVolume to control it;
            _audioSource.volume = _musicVolume;
        }

        public void SetVolume(float vol)
        {
            _musicVolume = vol; 
        }

    }
}
