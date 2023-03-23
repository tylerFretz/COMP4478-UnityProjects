using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public TextMeshPro contentText;
    public GameObject playButton;
    public GameObject playButtonPressed;
    public GameObject quitButton;
    public AudioSource buttonClick;
    private bool isLoading = false;
    
    void Awake()
    {
        string gameResult = PlayerPrefs.GetString("GameResult");
        PlayerPrefs.DeleteKey("GameResult");
        ShowMainMenuContent(gameResult);
        isLoading = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (playButton.GetComponent<Collider2D>().OverlapPoint(mousePos) && !isLoading)
            {
                isLoading = true;
                buttonClick.PlayOneShot(buttonClick.clip);
                playButton.SetActive(false);
                playButtonPressed.SetActive(true);
                StartGame();
            }
            else if (quitButton.GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                buttonClick.PlayOneShot(buttonClick.clip);
                Quit();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            playButton.SetActive(true);
            playButtonPressed.SetActive(false);
        }
    }

    void ShowMainMenuContent(string gameResult)
    {
        if (string.IsNullOrEmpty(gameResult))
        {
            contentText.text = "Welcome to the Memory Game!\n\nMatch all of the tiles before time runs out to win!";
        }
        else if (gameResult.Equals("win"))
        {
            contentText.text = "Congratulations! You won!\n\nClick the play button to play again.";
        }
        else if (gameResult.Equals("lose"))
        {
            contentText.text = "You ran out of time!\n\nClick the play button to play again.";
        }
        
        string fastestTime = PlayerPrefs.GetString("FastestTime");
        if (!string.IsNullOrEmpty(fastestTime))
        {
            contentText.text += "\n\nFastest time: " + fastestTime + " seconds";
        }
    }

    void StartGame()
    {
        StartCoroutine(LoadGameScene());
    }

    void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Scene gameScene = SceneManager.GetSceneByName("GameScene");
        SceneManager.SetActiveScene(gameScene);
    }
}
