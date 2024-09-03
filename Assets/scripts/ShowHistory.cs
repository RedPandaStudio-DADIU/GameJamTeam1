using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text;

public class ShowHistory : MonoBehaviour
{
    public string LastSceneName= "Starting";
        
    
    // Start is called before the first frame update
    void Start()
    {
        
        LastSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("1Last scene was: " + LastSceneName);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     

    


     public void OnHistoryButtonClick()
    {
        SceneManager.LoadScene("HistoryScore"); 
    }

     public void OnBackButtonClick()
    {
        SceneManager.LoadScene("Starting"); 
    }

}
