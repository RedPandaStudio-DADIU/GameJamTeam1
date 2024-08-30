using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionLight : MonoBehaviour
{
    [SerializeField] StreetLamps lampManager;

    // Start is called before the first frame update
    void Start()
    {
        lampManager = FindObjectOfType<StreetLamps>();
    }

    public void CheckVisibility(){
        if(transform.position.x < Camera.main.transform.position.x - 30.0f)
        {
            transform.position = lampManager.GetPosition();
            lampManager.IncreasePosition();
    
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckVisibility();
    }
}
