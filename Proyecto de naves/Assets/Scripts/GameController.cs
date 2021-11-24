using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private int score;
    public Text scoreText;


    public Text restartText;
    public Text gameOverText;
    private bool restart;
    private bool gameOver;

    void Start()
    {
        restart = false;
        gameOver = false;
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
       StartCoroutine(SpawnWaves());

        score = 0;
        UpdateScore();
    }
    private void Update()
    {
        if(restart && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (!gameOver)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
        restartText.gameObject.SetActive(true);
        restart = true;


    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOver = true;

    }
    public void AddScore(int value)
    {
        score += value;
        UpdateScore();
    }
    void UpdateScore()
    {
        scoreText.text = "Score : "  + score;
    }
}
