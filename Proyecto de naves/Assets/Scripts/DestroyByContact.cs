using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameController gameController;
    public GameObject explosion;
    public GameObject playerExplosion;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boundary")) { return; }

        Instantiate(explosion, transform.position, transform.rotation);
        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
    private void OnEnable()
    {
        gameController = FindObjectOfType<GameController>();
    }

}
