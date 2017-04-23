using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Canvas canvas;
    public Text gameOverText;
    public GameObject asteroid;
    public GameObject ufo;

    LevelManager manager;
    public float asteroidSpawnTime = 1f;
    float asteroidTimeLeft = 1f;
    public static int asteroidsDestroyed;
    public static bool gameOver;

    public float ufoSpawnTime = 3.5f;
    float ufoTimeLeft = 3.5f;

    private void Start()
    {
        manager = gameObject.AddComponent<LevelManager>();
        asteroidsDestroyed = 0;
        gameOver = false;
        canvas.enabled = false;

        if (LevelManager.gameDifficulty == Difficulty.Hard)
        {
            ufoSpawnTime = 2.5f;
            ufoTimeLeft = ufoSpawnTime;

            asteroidSpawnTime = .7f;
            asteroidTimeLeft = asteroidSpawnTime;
        }
    }

    void Update()
    {
        if (!gameOver)
        {
            asteroidTimeLeft -= Time.deltaTime;
            if (asteroidTimeLeft <= 0)
            {
                SpawnAsteroid();
            }

            if (LevelManager.gameDifficulty != Difficulty.Easy)
            {
                ufoTimeLeft -= Time.deltaTime;
                if (ufoTimeLeft <= 0)
                {
                    SpawnUfo();
                }
            }

        }
        else
        {
            int asteroidCount = transform.childCount;
            for (int i = 0; i < asteroidCount; i++)
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }

            GameObject[] beamList = GameObject.FindGameObjectsWithTag("Beam");

            foreach (var beam in beamList)
            {
                Destroy(beam);
            }

            gameOverText.text = "Asteroids destroyed: " + asteroidsDestroyed.ToString() + "\nDifficulty: " + LevelManager.gameDifficulty;
            canvas.enabled = true;

            if (Input.GetButtonDown("Restart"))
            {
                manager.LoadLevel("Main");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            manager.LoadLevel("Menu");
        }
    }


    void SpawnUfo()
    {
        float xRand;
        float yRand;
        int location = Random.Range(1, 9);

        switch (location)
        {
            case 1:
                xRand = 0.1f;
                yRand = 1.1f;
                break;
            case 2:
                xRand = 0.9f;
                yRand = 1.1f;
                break;
            case 3:
                xRand = -0.1f;
                yRand = 0.9f;
                break;
            case 4:
                xRand = 1.1f;
                yRand = 0.9f;
                break;
            case 5:
                xRand = -0.1f;
                yRand = 0.1f;
                break;
            case 6:
                xRand = 1.1f;
                yRand = 0.1f;
                break;
            case 7:
                xRand = 0.1f;
                yRand = -0.1f;
                break;
            default:
                xRand = 0.9f;
                yRand = -0.1f;
                break;
        }

        Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(xRand, yRand, 10.0f));
        Instantiate(ufo, spawnPosition, transform.rotation, this.transform);

        ufoTimeLeft = ufoSpawnTime;
    }

    void SpawnAsteroid()
    {
        float xRand;
        float yRand;

        if (CoinToss())
        {
            if (CoinToss())
            {
                xRand = -0.1f;
            }
            else
            {
                xRand = 1.1f;
            }
            yRand = Random.Range(0f, 1f);
        }
        else
        {
            if (CoinToss())
            {
                yRand = -0.1f;
            }
            else
            {
                yRand = 1.1f;
            }
            xRand = Random.Range(0f, 1f);
        }

        Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(xRand, yRand, 10.0f));
        Instantiate(asteroid, spawnPosition, transform.rotation, this.transform);
        asteroidTimeLeft = asteroidSpawnTime;
    }

    public static bool CoinToss()
    {
        return (Random.value > 0.5f);
    }
}
