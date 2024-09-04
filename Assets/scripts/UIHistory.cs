using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text;


public class UIHistory : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI historyText; 
    //[SerializeField] Button backButton;

    


    // Start is called before the first frame update
    void Start()
    {
        string displayText = "Record:\n";
        float lastTimeTaken = Timer.LastTimeTaken; 
        int minutes = Mathf.FloorToInt(lastTimeTaken / 60f);
        int seconds = Mathf.FloorToInt(lastTimeTaken % 60f);
        displayText += string.Format("Last Session Time: {0:00}:{1:00}", minutes, seconds);

        historyText.text = displayText;

    }
    
    

    public void OnBackButtonClick()
    {
       
       
        SceneManager.LoadScene("Ending"); 
    }

     
    void Update()
    {
        
    }
}
