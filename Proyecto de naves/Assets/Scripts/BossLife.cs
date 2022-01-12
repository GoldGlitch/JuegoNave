using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : MonoBehaviour
{
    private GameController gameController;
    public GameObject explosion;
  
    public int ScoreValue;
    public int numeroDisparos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) { return; }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }


        if (other.CompareTag("Bala"))
        {
            numeroDisparos = numeroDisparos + 1;
            gameController.AddScore(ScoreValue);
            if (numeroDisparos == 10)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                gameController.Win();
                Destroy(gameObject);
            }
            
        }



        //gameController.AddScore(ScoreValue);
        Destroy(other.gameObject);
        

    }
    private void Start()
    {
        numeroDisparos = 0;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        gameController = FindObjectOfType<GameController>();
        GameObject.FindWithTag("GameController").GetComponent<GameController>();

    }
}
