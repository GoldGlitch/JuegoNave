using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    public void OnEnable()
    {
        Time.timeScale = 0f;
        

    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
    public void BotonesGameMenu(string escena)
    {
        SceneManager.LoadScene(escena);
    }
}
