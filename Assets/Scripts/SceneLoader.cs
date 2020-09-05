using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int Index;

    

    public void LoadLevel()
    {
        SceneManager.LoadScene(Index);
    }
}
