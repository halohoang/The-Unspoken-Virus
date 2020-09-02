using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SAE_Project.Assets.Scripts
{
    class HoverSound : MonoBehaviour
    {
        //variables
        public AudioSource Effect;
        public AudioClip HoverEffect;


        //Functions
        public void HoverEffectSound()
        {
            Effect.PlayOneShot(HoverEffect);
        }

    }
}
