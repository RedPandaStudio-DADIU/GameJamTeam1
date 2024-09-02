using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowTimeInEnding : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lastTimeText;
    [SerializeField] TextMeshProUGUI feedbackText;
    [SerializeField] Button historyButton; 

    // Start is called before the first frame update
    void Start()
    {
        float lastTimeTaken = Timer.LastTimeTaken; // 读取最后一局的时间
        int minutes = Mathf.FloorToInt(lastTimeTaken / 60f);
        int seconds = Mathf.FloorToInt(lastTimeTaken % 60f);
        lastTimeText.text = string.Format("You used : {0:00}:{1:00} in Last Session", minutes, seconds);
        SetFeedbackMessage(lastTimeTaken);
    }

    void SetFeedbackMessage(float timeTaken)
    {
        // 假设 120 秒是满分，越快完成越好
        if (timeTaken <= 60f)
        {
            feedbackText.text = "Excellent! You were really fast!";
            feedbackText.color = Color.green;
        }
        else if (timeTaken <= 100f)
        {
            feedbackText.text = "Good job! You did well!";
            feedbackText.color = Color.yellow;
        }
        else
        {
            feedbackText.text = "Keep practicing! You can do better!";
            feedbackText.color = Color.red;
        }
    }

    void OnHistoryButtonClick()
    {
        SceneManager.LoadScene("HistoryScore"); 
    }
    
    public void historyButtonClick()
    {
            SceneManager.LoadScene("HistoryScore"); 
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
