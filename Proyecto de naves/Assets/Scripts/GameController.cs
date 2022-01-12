using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject[] EnemyShips;
    public GameObject[] Boss;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public int score;
    private int contadorAsteroides;
    private int enemyShipCount;
    private int bossCount;


    private int contadorOleadas;
    private int contadorOleadas2;


    public Text scoreText;


    public GameObject restartGameObject;
    public GameObject gameOverGameObject;
    public GameObject winGameObject;
    public bool restart;
    public bool gameOver;
    public bool Winner;
    private bool boss;
    
    public GameObject menu;
    public bool MenuAbierto;

    void Start()
    {
        contadorAsteroides = 3;
        enemyShipCount = 0;
        bossCount = 0;



        UpdatespawnValues();
        restart = false;
        gameOver = false;
        Winner = false;
        boss = true;

        MenuAbierto = false;
        
        gameOverGameObject.SetActive(false);
        winGameObject.SetActive(false);
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


        Debug.Log(contadorOleadas2);
        
    }
    private void Pausa()
    {
        if (gameOver == true)
        {
            Time.timeScale = 0.2f;
        }
       
        else if(MenuAbierto==false)
        {
            Time.timeScale = 1f;
        }

        if (Winner == true)
        {
            Time.timeScale = 0.2f;
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
        SceneManager.LoadScene(1);
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (!gameOver || !Winner)
        {
            if (contadorOleadas2 <= 10)
            {
                for (int i = 0; i < contadorAsteroides/*hazardCount*/; i++)
                {
                    //GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Instantiate(hazard, spawnPosition, Quaternion.identity);
                    yield return new WaitForSeconds(spawnWait);
                }
                if (contadorOleadas == 3)
                {
                    contadorOleadas = 0;
                    enemyShipCount = enemyShipCount + 1;
                    for (int i = 0; i < enemyShipCount; i++)
                    {
                        //GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                        GameObject enemyShip = EnemyShips[Random.Range(0, EnemyShips.Length)];
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                        Instantiate(enemyShip, spawnPosition, Quaternion.identity);
                        yield return new WaitForSeconds(spawnWait);
                    }
                }

            }
            else
            {
                if (contadorOleadas2 >= 10)
                {
                   


                    if (boss)
                    {
                        GameObject enemyBoss = Boss[Random.Range(0, Boss.Length)];
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                        Instantiate(enemyBoss, spawnPosition, Quaternion.identity);
                        yield return new WaitForSeconds(spawnWait);
                        boss = false;
                    }

                    /*
                    //contadorOleadas2 = 0;
                    //bossCount = 1;
                    for (int i = 0; i < 1; i++)
                    {
                        //GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                        GameObject enemyBoss = Boss[Random.Range(0, Boss.Length)];
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                        Instantiate(enemyBoss, spawnPosition, Quaternion.identity);
                        yield return new WaitForSeconds(spawnWait);
                    }*/
                }

            }
            
           
            yield return new WaitForSeconds(waveWait);
            contadorAsteroides = contadorAsteroides + 1;
            contadorOleadas = contadorOleadas + 1;
            contadorOleadas2 = contadorOleadas2 + 1;



        }
        restartGameObject.SetActive(true);
        restart = true;


    }
   
    public void GameOver()
    {
        gameOverGameObject.SetActive(true);
        
        gameOver = true;

    }   
    public void Win()
    {
        winGameObject.SetActive(true);
        Winner = true;
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
