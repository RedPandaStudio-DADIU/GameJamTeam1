using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameOver : MonoBehaviour
{
    public void RestartGame()
    {
        
        SceneManager.LoadScene("Level1");
    }

    public void Quit(){
        Application.Quit();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
