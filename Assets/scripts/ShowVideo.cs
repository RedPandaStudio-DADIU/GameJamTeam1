using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShowVideo : MonoBehaviour
{
    [SerializeField] private float pauseTime = 60.0f;  
    [SerializeField] private float resumeDelay = 30.0f; 
    [SerializeField] private Canvas pauseCanvas;  

    public GameObject CameraObject;

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
            pauseCanvas.transform.position = CameraObject.transform.position + CameraObject.transform.forward * 2.0f; // 2.0f 是距离摄像机的距离，你可以根据需要调整
            pauseCanvas.transform.rotation = CameraObject.transform.rotation; // 使画布面向摄像机

            // 设置画布的大小
            RectTransform canvasRect = pauseCanvas.GetComponent<RectTransform>();

            if (canvasRect != null)
            {
                // 调整画布的宽高比，使其合适
                canvasRect.sizeDelta = new Vector2(800, 600); // 你可以根据需要调整宽度和高度
            }
            
            pauseCanvas.enabled = true;  
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
        gamePaused = false;
        pauseTime = float.MaxValue;
    }
}