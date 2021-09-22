using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController TrailController;

    public List<Bird> Birds;
    public List<Enemy> Enemies;

    public BoxCollider2D TapCollider;
    
    public int _stage;
    public Text _status;
    public GameObject _endPanel;
    public GameObject _nextButton;

    private Bird _shotBird;
    private bool _showPanel = false;
    private bool _isGameEnded = false;
    private int _sceneIndex;

    void Start()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }

    private void Update()
    {
        //Jika burung sudah habis tapi enemy masih ada,
        //maka kalah dan keluarkan panel 
        if(Birds.Count == 0 && Enemies.Count != 0 && !_showPanel)
        {
            _showPanel = true;
            StartCoroutine(SetEndGame());
        }
    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;

        if(_isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }
    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }
        if (Enemies.Count == 0)
        {
            _isGameEnded = true;
            StartCoroutine(SetEndGame());
        }
    }

    IEnumerator SetEndGame()
    {
        yield return new WaitForSeconds(1f);
        if (_isGameEnded)
        {
            if(SceneManager.GetActiveScene().buildIndex == _stage)
            {
                Destroy(_nextButton);
            }
            _status.text = "YOU WIN!";
        }
        else
        {
            _status.text = "TRY AGAIN!";
            Destroy(_nextButton);
        }
        _endPanel.gameObject.SetActive(true);
    }
}