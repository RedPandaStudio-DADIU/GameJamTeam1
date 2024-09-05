using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePauseManager : MonoBehaviour
{
    [SerializeField] private float pauseTime = 5.0f;  
    [SerializeField] private float resumeDelay = 6.0f; 
    [SerializeField] private Canvas pauseCanvas;  

    public GameObject CameraObject;
    public GameObject objectToSpawn;  // 生成的物体
    public Transform playerTransform; // 获取玩家位置
    public float offsetDistance = 25f;  // 相机和物体移动的距离

    private bool gamePaused = false;
    private float timer = 0.0f;
    private Vector3 originalCameraPosition;  // 用来保存相机的初始位置
    private GameObject spawnedObject;  // 引用生成的物体

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

            
            // if (timer >= pauseTime && !gamePaused)
            // {
            //     StartCoroutine(PauseAndResumeGame());
            // }
        }
    }

    public IEnumerator PauseAndResumeGame()
    {
        // stop
        gamePaused = true;
        Time.timeScale = 0;  

        originalCameraPosition = CameraObject.transform.position;


        if (pauseCanvas != null)
        {
            

            if (CameraObject != null)
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
        }
        
        Vector3 targetPosition = originalCameraPosition + new Vector3(-offsetDistance, 0, 0);
        float cameraMoveDuration = 1.5f;  // 摄像机移动的时间
        yield return MoveCameraToPosition(targetPosition, cameraMoveDuration);

    //     if (objectToSpawn != null )
    //     {
    //         Vector3 spawnPosition = playerTransform.position + new Vector3(-offsetDistance, 0, 0);  // 玩家身后 30 个单位
    //         spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    //    }
        
        // wait for 3 s
       targetPosition = originalCameraPosition ;
         cameraMoveDuration = 1.5f;  // 摄像机移动的时间
        yield return MoveCameraToPosition(targetPosition, cameraMoveDuration);

        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }
        
        //yield return MoveCameraToPosition(originalCameraPosition, cameraMoveDuration);

        
        
        
        
        //yield return new WaitForSecondsRealtime(resumeDelay);

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

    private IEnumerator MoveCameraToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = CameraObject.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            CameraObject.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.unscaledDeltaTime;  // 使用 unscaledDeltaTime，因为我们暂停了时间
            yield return null;
        }

        CameraObject.transform.position = targetPosition;
    }

}