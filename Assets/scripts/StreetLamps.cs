using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLamps : MonoBehaviour
{
    public GameObject lightPrefab;     
    public int numberOfLights = 10;    
    public float spacing = 35f;  
    public float lower = 0.5f;      
    public float lightDuration = 5f;   

    private List<GameObject> lights = new List<GameObject>();
    private Vector3 position;
    private float instances = 0;
    float currentHeight = 30f;

    void Start()
    {
        lower = 0.5f;
        
        for (int i = 0; i < numberOfLights; i++)
        {
            position = new Vector3(i * spacing, currentHeight, -27); 
            GameObject light = Instantiate(lightPrefab, position, Quaternion.identity);
            lights.Add(light);
            instances++;
            currentHeight -= lower;
        }

        position = new Vector3(numberOfLights * spacing, currentHeight , -27); 

       
        // StartCoroutine(DisableLights());
    }

    public Vector3 GetPosition(){
        return position;
    }

     public void IncreasePosition(){
        instances++;
        currentHeight -= lower;
        this.position= new Vector3(instances * spacing, currentHeight , -27); 
    }


    IEnumerator DisableLights()
    {
        foreach (GameObject light in lights)
        {
            
            yield return new WaitForSeconds(lightDuration);

            
            light.GetComponent<Light>().enabled = false;
            Destroy(light);
        }
    }
}
