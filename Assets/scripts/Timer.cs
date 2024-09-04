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

    [SerializeField] private AudioSource audioSource;  // Reference to AudioSource component
    [SerializeField] private AudioClip audioClip30To90Seconds;  // Audio clip for 30-90 seconds
    [SerializeField] private AudioClip audioClipLast15Seconds;  // Audio clip for last 15 seconds
    [SerializeField] private AudioClip audioClipLast10Seconds;  // Audio clip for last 10 seconds

    private bool playedAudio30To90 = false;  // To prevent replaying the clip for 30-90 sec range
    private bool playedAudioLast15 = false;  // To prevent replaying the clip for last 15 sec
    private bool playedAudioLast10 = false;  // To prevent replaying the clip for last 10 sec

    // Method to play audio clips
    private void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    
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

         // Play the audio for 30-90 seconds range
            if (timeRemaining <= 90.0f && timeRemaining >= 30.0f && !playedAudio30To90)
            {
                PlayAudio(audioClip30To90Seconds);
                playedAudio30To90 = true;  // Ensure the clip is only played once
            }

            // Play the audio for the last 15 seconds
            if (timeRemaining <= 15.0f && !playedAudioLast15)
            {
                PlayAudio(audioClipLast15Seconds);
                playedAudioLast15 = true;  // Ensure the clip is only played once
            }

            // Play the audio for the last 10 seconds
            if (timeRemaining <= 10.0f && !playedAudioLast10)
            {
                PlayAudio(audioClipLast10Seconds);
                playedAudioLast10 = true;  // Ensure the clip is only played once
            }

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
