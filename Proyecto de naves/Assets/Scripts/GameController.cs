using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public int score;
    
    public Text scoreText;


    public GameObject restartGameObject;
    public GameObject gameOverGameObject;
    public bool restart;
    public bool gameOver;
    private bool EnterNameDialogSatate;
    public GameObject menu;
    public bool MenuAbierto;

    void Start()
    {
        UpdatespawnValues();
        restart = false;
        gameOver = false;

        MenuAbierto = false;
        EnterNameDialogSatate = true;
        gameOverGameObject.SetActive(false);
        restartGameObject.SetActive(false);
       StartCoroutine(SpawnWaves());

        score = 0;
        UpdateScore();
    }

    void UpdatespawnValues()
    {
        Vector2 half = Utils.GetHalfDimensionsInWorldUnits();
        spawnValues = new Vector3(half.x -0.9f, 0f, half.y + 8f);



    }
    private void Update()
    {
        Pausa();

        if (restart && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        
    }
    private void Pausa()
    {
        if (gameOver == true)
        {
            Time.timeScale = 0.2f;
        }
        if (EnterNameDialogSatate == true)
        {
            Time.timeScale = 0f;
        }
        else if(MenuAbierto==false)
        {
            Time.timeScale = 1f;
        }
    }
    public void MenuPause()
    {
        if(MenuAbierto == true)
        {
            Time.timeScale = 0f;
            Debug.Log(Time.timeScale);
        }
        
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (!gameOver)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
        restartGameObject.SetActive(true);
        restart = true;


    }
    public void Close()
    {
        EnterNameDialogSatate = false;
    }
    public void GameOver()
    {
        gameOverGameObject.SetActive(true);
        
        gameOver = true;

    }
    public void MenuActive()
    {
        menu.SetActive(true);

        MenuAbierto = true;
        MenuPause();
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
