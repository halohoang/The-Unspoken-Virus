using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SAE_Project.Assets.Scripts
{
    public class FadeOut : MonoBehaviour
    {
        //variables
        public float FadeDelay;
        private float _FadeDelaySeconds;
        public GameObject Fade;
        public bool GenerateAfterImages = false;



        //functions
        private void Start()
        {
            _FadeDelaySeconds = FadeDelay;
        }

        private void Update()
        {
            if (GenerateAfterImages)
            {

                if (_FadeDelaySeconds > 0)
                {
                    _FadeDelaySeconds -= Time.deltaTime;
                }
                else
                {
                    //Generate after iamge effect
                    
                    GameObject _currentFade = Instantiate(Fade, transform.position , transform.rotation);
                    Sprite _currentSprite = GetComponent<SpriteRenderer>().sprite;
                    _currentFade.transform.localScale = this.transform.localScale;
                    _currentFade.GetComponent<SpriteRenderer>().sprite = _currentSprite;
                    _FadeDelaySeconds = FadeDelay;
                    Destroy(_currentFade, 1f);
                }
            }
        }
    }
}
