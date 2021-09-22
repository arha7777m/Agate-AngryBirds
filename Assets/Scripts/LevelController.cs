using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private int _sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(_sceneIndex + 1);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
