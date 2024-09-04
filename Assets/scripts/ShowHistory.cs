using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text;

public class ShowHistory : MonoBehaviour
{
   
        
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     public void OnHistoryButtonClick()
    {
        SceneManager.LoadScene("History1"); 
    }

    

}
