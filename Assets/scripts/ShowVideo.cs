using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video; 

public class ShowVideo : MonoBehaviour
{
    [SerializeField] private float pauseTime = 60.0f;  
    [SerializeField] private float resumeDelay = 30.0f; 
    [SerializeField] private Canvas pauseCanvas;  
    [SerializeField] private VideoPlayer videoPlayer;  // Add a reference to the VideoPlayer

    public GameObject CameraObject;

    private bool gamePaused = false;
    private float timer = 0.0f;

    void Start()
    {
        
        if (pauseCanvas != null)
        {
            pauseCanvas.enabled = false;
        }

        if (videoPlayer != null)
        {
            videoPlayer.Stop();
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
            pauseCanvas.transform.position = CameraObject.transform.position + CameraObject.transform.forward * 2.0f; // 2.0f 是距离摄像机的距离，你可以根据需要调整
            pauseCanvas.transform.rotation = CameraObject.transform.rotation; 

            // Set the canvas size
            RectTransform canvasRect = pauseCanvas.GetComponent<RectTransform>();

            if (canvasRect != null)
            {
                
                canvasRect.sizeDelta = new Vector2(800, 600); // Adjust the canvas size
            }
            
            pauseCanvas.enabled = true;  
        }

         // Play the video when the canvas is shown
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }

        // wait for 15 s
        yield return new WaitForSecondsRealtime(resumeDelay);

        // continue
        Time.timeScale = 1;  // 
        Debug.Log("Time.timeScale after resume: " + Time.timeScale);
        if (pauseCanvas != null)
        {
            pauseCanvas.enabled = false;  
            Debug.Log("Pause canvas enabled after resume: " + pauseCanvas.enabled);
  
        }
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
        
        gamePaused = false;
        pauseTime = float.MaxValue;
    }
}