using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text;


public class UIHistory : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI historyText; // 显示所有记录的文本
    [SerializeField] Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        string displayText = "Record:\n";
        float lastTimeTaken = Timer.LastTimeTaken; // 读取最后一局的时间
        int minutes = Mathf.FloorToInt(lastTimeTaken / 60f);
        int seconds = Mathf.FloorToInt(lastTimeTaken % 60f);
        historyText.text = string.Format("Last Session Time: {0:00}:{1:00}", minutes, seconds);
    

    }

    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("Ending"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
