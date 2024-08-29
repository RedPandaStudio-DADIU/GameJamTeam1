using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLamps : MonoBehaviour
{
    public GameObject lightPrefab;     
    public int numberOfLights = 30;    
    public float spacing = 30f;        
    public float lightDuration = 5f;   

    private List<GameObject> lights = new List<GameObject>();

    void Start()
    {
        
        for (int i = 0; i < numberOfLights; i++)
        {
            Vector3 position = new Vector3(i * spacing, 0, 0); 
            GameObject light = Instantiate(lightPrefab, position, Quaternion.identity);
            lights.Add(light);
        }

       
        StartCoroutine(DisableLights());
    }

    IEnumerator DisableLights()
    {
        foreach (GameObject light in lights)
        {
            
            yield return new WaitForSeconds(lightDuration);

            
            light.GetComponent<Light>().enabled = false;
        }
    }
}
