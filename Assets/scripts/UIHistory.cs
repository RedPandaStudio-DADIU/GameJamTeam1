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
    [SerializeField] Button backButton;

    public ShowHistory showHistory;  
   


    // Start is called before the first frame update
    void Start()
    {
        string displayText = "Record:\n";
        float lastTimeTaken = Timer.LastTimeTaken; 
        int minutes = Mathf.FloorToInt(lastTimeTaken / 60f);
        int seconds = Mathf.FloorToInt(lastTimeTaken % 60f);
        historyText.text = string.Format("Last Session Time: {0:00}:{1:00}", minutes, seconds);
        
        

    }
    
    

    public void OnBackButtonClick()
    {
       if (showHistory != null)
        {
            string lastScene = showHistory.LastSceneName;
            SceneManager.LoadScene(lastScene);
            
            Debug.Log("2Last scene was: " + lastScene);
            showHistory.LastSceneName= null;
            Debug.Log("3Last scene was: " + lastScene);
        }
       
        SceneManager.LoadScene("Ending"); 
    }

     
    void Update()
    {
        
    }
}
