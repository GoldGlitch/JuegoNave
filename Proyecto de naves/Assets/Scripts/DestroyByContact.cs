using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    private GameController gameController;
    public GameObject explosion;
    public GameObject playerExplosion;
    public int ScoreValue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boundary") || other.CompareTag("Enemy")) { return; }

        if(explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        
        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }


        
        gameController.AddScore(ScoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);

    }
    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController"); 

        gameController = FindObjectOfType<GameController>();
        GameObject.FindWithTag("GameController").GetComponent<GameController>();

    }


}
