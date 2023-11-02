using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessionController : MonoBehaviour
{
    [SerializeField] private  int playerLives=3;
    private void Awake()
    {
        int findGameSessionOnjects = FindObjectsOfType<GameSessionController>().Length;
        if (findGameSessionOnjects > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessOfPlayerDeath()
    {
        if (playerLives > 1)
        {

            StartCoroutine("TakeTimeForDeath");
        }
        else
        {
            StartCoroutine("TakeTimeForResetGameSession");
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
    }

    private void TakeDeath()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator TakeTimeForDeath()
    {
        yield return new WaitForSeconds(1.5f);
        TakeDeath();
    }
    IEnumerator TakeTimeForResetGameSession()
    {
        yield return new WaitForSeconds(1.5f);
        ResetGameSession();
    }
    
}
