using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePauseManager : MonoBehaviour
{
    [SerializeField] private float pauseTime = 5.0f;  
    [SerializeField] private float resumeDelay = 3.0f; 
    [SerializeField] private Canvas pauseCanvas;  

    private bool gamePaused = false;
    private float timer = 0.0f;

    void Start()
    {
        
        if (pauseCanvas != null)
        {
            pauseCanvas.enabled = false;
        }
        Debug.Log("Time.timeScale before resume: " + Time.timeScale);
    }

    void Update()
    {
        
        if (!gamePaused)
        {
            timer += Time.deltaTime;

            
            if (timer >= pauseTime && !gamePaused)
            {
                StartCoroutine(PauseAndResumeGame());
            }
        }
    }

    private IEnumerator PauseAndResumeGame()
    {
        // stop
        gamePaused = true;
        Time.timeScale = 0;  
        if (pauseCanvas != null)
        {
            pauseCanvas.enabled = true;  
        }

        // wait for 3 s
        yield return new WaitForSecondsRealtime(resumeDelay);

        // continue
        Time.timeScale = 1;  // 
        Debug.Log("Time.timeScale after resume: " + Time.timeScale);
        if (pauseCanvas != null)
        {
            pauseCanvas.enabled = false;  
            Debug.Log("Pause canvas enabled after resume: " + pauseCanvas.enabled);
  
        }
        gamePaused = false;
        pauseTime = float.MaxValue;
    }
}