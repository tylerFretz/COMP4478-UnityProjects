using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public TextMeshPro contentText;
    public TextMeshPro scoreText;

    void Awake()
    {
        string mode = PlayerPrefs.GetString("MenuMode");
        PlayerPrefs.DeleteKey("MenuMode");

        if (mode == "gameover")
        {
            ShowGameOverContent();
        }
        else
        {
            ShowMainMenuContent();
        }
    }

    public void ShowMainMenuContent()
    {
        contentText.text = "Collect as many fish as you can but watch out for bombs! \n - \n Move the net with WASD keys. Rotate the net with QE keys.";
        contentText.alignment = TextAlignmentOptions.TopGeoAligned;
        contentText.fontSize = 5;
        scoreText.text = "";
    }

    public void ShowGameOverContent()
    {
        var score = PlayerPrefs.GetInt("PlayerScore");
        contentText.text = "Game Over!";
        contentText.alignment = TextAlignmentOptions.MidlineGeoAligned;
        contentText.fontSize = 8;
        scoreText.text = "Score: " + score;
    }
}
