using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateScript : MonoBehaviour
{
    public int playerScore = 0;
    public Text scoreText;
    public AudioSource gameOverSound;
    public AudioSource bellSound;

    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
        bellSound.PlayOneShot(bellSound.clip);
    }

    public void GameOver()
    {
        StartCoroutine(LoadGameOverScene());
    }
    
    IEnumerator LoadGameOverScene()
    {
        gameOverSound.PlayOneShot(gameOverSound.clip);

        PlayerPrefs.SetInt("PlayerScore", playerScore);
        PlayerPrefs.SetString("MenuMode", "gameover");
        PlayerPrefs.Save();
        
        yield return new WaitForSeconds(1.0f);
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MenuScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        Scene menuScene = SceneManager.GetSceneByName("MenuScene");
        SceneManager.SetActiveScene(menuScene);

    }
}
