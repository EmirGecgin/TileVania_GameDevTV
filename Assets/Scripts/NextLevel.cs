using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private float delayTime=0.25f;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
            StartCoroutine("LoadNextLevel");
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(delayTime);
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        int nextBuildIndex = currentBuildIndex + 1;
        if (nextBuildIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextBuildIndex = 0;
        }

        SceneManager.LoadScene(nextBuildIndex);
    }
}
