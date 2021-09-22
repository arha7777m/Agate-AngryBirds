using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private int _sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Play(int level)
    {
        SceneManager.LoadScene(_sceneIndex + level);
    }
}
