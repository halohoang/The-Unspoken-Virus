using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SAE_Project.Assets.Scripts
{
    class RestartButton : MonoBehaviour
    {
        //Variables
        [SerializeField] private GameObject _restart;
        public int Index;

        public void Restart()
        {

            SceneManager.LoadScene(Index);

            _restart.SetActive(true);
        }


    }
}
