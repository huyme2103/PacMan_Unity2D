using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private Transform connection;
    [SerializeField] private Transform player;
    public GameObject playButton;
    public GameObject win;

    public static Controller instance;
    static int score = 0;
    bool gameOver = false;
    public Text scoreText;
    public Text liveText;
    int live = 5;

    private void Awake()
    {
        playButton.SetActive(false);
        win.SetActive(false);
        instance = this;
    }
    public void InscrementScore()
    {
        score++;
        scoreText.text = score.ToString();
        if (score == 240)
        {
            StartCoroutine(ReloadScreenWithDelay());
        }

    }
    public void DecreaseLife()
    {



        if (live > 0)
        {
            if (player.CompareTag("Player"))
            {
                player.transform.position = new Vector3(connection.position.x, connection.position.y, player.transform.position.z);
                live--;
                liveText.text = live.ToString();
            }


        }
        if (live <= 0)
        {
            ShowPlayButton();
            gameOver = true;

        }
    }

    public bool GameOver
    {
        get { return gameOver; }
    }
    private IEnumerator ReloadScreenWithDelay()
    {
        win.SetActive(true);
        yield return new WaitForSeconds(5f);  //5s


        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        win.SetActive(false);
    }


    private void ShowPlayButton()
    {
        playButton.SetActive(true);
    }


    public void OnPlayButtonClick()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        score = 0;
        scoreText.text = score.ToString();
    }
}
