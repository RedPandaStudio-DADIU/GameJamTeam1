using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRendering : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (GetComponent<Renderer>().isVisible)
        {
            Debug.Log($"{gameObject.name} is visible.");
        } 
    }

}
