using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Timer : MonoBehaviour
{
    [SerializeField] float timeLimit;
    [SerializeField] TextMeshProUGUI text;
    private float timeRemaining;
    //private int gameSession;
    private float elapsedTime = 0;
    private bool isRunning = true;
    
    public static float LastTimeTaken { get; private set; }


    void Start()
    {
        timeLimit = 120.0f;
        timeRemaining = timeLimit;
        elapsedTime = 0f;
        isRunning = true;
        
    }

    

    void Update()
    {
        if (isRunning)
        {
        
        timeRemaining -= Time.deltaTime;
        elapsedTime += Time.deltaTime;
        timeRemaining = Mathf.Max(timeRemaining, 0);

        if (timeRemaining <= 15.0f){
            UpdateTime(timeRemaining, Color.red);

        } else if(timeRemaining <= Mathf.FloorToInt(timeLimit/2)){
            UpdateTime(timeRemaining, Color.yellow);

        } else{
            UpdateTime(timeRemaining, Color.green);

        }
        if (timeRemaining == 0.0){
            GameOver();
        }
        
        }
    


    }

    public void StopTimer()
    {
        isRunning = false;
        
        LastTimeTaken = elapsedTime; 

        Debug.Log("Timer stopped: Time = " + LastTimeTaken);
     }

    public void GameOver(){

        //StopTimer();
        Debug.Log("Time is over!!!!!!!!!!!!!");
        SceneManager.LoadScene("Failing");
    }

    public void UpdateTime(float timeLimit, Color color){
        int minutes = Mathf.FloorToInt(timeLimit / 60f);
        int seconds = Mathf.FloorToInt(timeLimit % 60f);
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        text.color = color;
    }


}
