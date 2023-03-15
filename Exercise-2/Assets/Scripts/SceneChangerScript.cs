using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerScript : MonoBehaviour
{
    public void Play()
    {
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        // delay for button animation
        yield return new WaitForSeconds(0.5f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        Scene gameScene = SceneManager.GetSceneByName("GameScene");
        SceneManager.SetActiveScene(gameScene);

    }

    public void Quit()
    {
        Application.Quit();
    }
}
