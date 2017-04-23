using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static Difficulty gameDifficulty = Difficulty.Normal;
    public Text difButtonText;

    private void Start()
    {
        if (difButtonText)
        {
            if (gameDifficulty == Difficulty.Easy)
            {
                difButtonText.text = "Difficulty: Easy";
            }
            else if (gameDifficulty == Difficulty.Hard)
            {
                difButtonText.text = "Difficulty: Hard";
            }
            else
            {
                difButtonText.text = "Difficulty: Normal";
            }
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }

    public void ChangeDifficulty()
    {
        if (difButtonText.text == "Difficulty: Easy")
        {
            difButtonText.text = "Difficulty: Normal";
            gameDifficulty = Difficulty.Normal;
        }
        else if (difButtonText.text == "Difficulty: Normal")
        {
            difButtonText.text = "Difficulty: Hard";
            gameDifficulty = Difficulty.Hard;
        }
        else if (difButtonText.text == "Difficulty: Hard")
        {
            difButtonText.text = "Difficulty: Easy";
            gameDifficulty = Difficulty.Easy;
        }
    }

    public void OverrideDifficulty(Difficulty dif)
    {
        gameDifficulty = dif;
    }
}

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}
