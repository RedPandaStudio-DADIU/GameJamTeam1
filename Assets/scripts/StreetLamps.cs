using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLamps : MonoBehaviour
{
    public GameObject lightPrefab;     
    public int numberOfLights = 10;    
    public float spacing = 30f;        
    public float lightDuration = 5f;   

    private List<GameObject> lights = new List<GameObject>();
    private Vector3 position;
    private float instances = 0;

    void Start()
    {
        
        for (int i = 0; i < numberOfLights; i++)
        {
            position = new Vector3(i * spacing, 0, -20); 
            GameObject light = Instantiate(lightPrefab, position, Quaternion.identity);
            lights.Add(light);
            instances++;
        }

        position = new Vector3(numberOfLights * spacing, 0, -20); 

       
        // StartCoroutine(DisableLights());
    }

    public Vector3 GetPosition(){
        return position;
    }

     public void IncreasePosition(){
        instances++;
        this.position= new Vector3(instances * spacing, 0, -20); 
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
