using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicateEnv : MonoBehaviour
{
    [SerializeField] EnvSpawner envManager;
    [SerializeField] float visibiityLimit;

    // Start is called before the first frame update
    void Start()
    {
        envManager = FindObjectOfType<EnvSpawner>();
        visibiityLimit= 65f;
    }

    public void CheckVisibility(){

        if (gameObject.tag == "Ground")
        {
            float roadWidth = GetComponent<Collider>().bounds.size.x;
            float roadVisibiityLimit =  envManager.GetRoadVisibility();
            if(transform.position.x < Camera.main.transform.position.x - roadVisibiityLimit* roadWidth)
            {
                RepositionRoad(roadWidth);
            }
        }
        else if (gameObject.tag == "Building")
        {
            if(transform.position.x < Camera.main.transform.position.x - visibiityLimit)
            {
                ReplaceBuilding();
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        CheckVisibility();
    }

    void CollapseRoad(){
        AddRigidbody();
        StartCoroutine(DelayReposition(10f));
        RemoveRigidbody();
    }
    
    void RepositionRoad(float roadWidth)
    {
        
        Vector3 newPosition = envManager.GetStreetSpawnPosition();
        transform.position = newPosition;
        envManager.SetStreetSpawnPosition(roadWidth);
    }

    IEnumerator DelayReposition(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    void AddRigidbody(){
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.mass = 2.0f;
        rb.drag = 1.0f;
        rb.angularDrag = 0.5f;
        rb.useGravity = true;
    }

    void RemoveRigidbody(){
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Destroy(rb);
        }
    }

    void ReplaceBuilding()
    {
        // Debug.LogWarning("BuildingCreation");
        Destroy(gameObject);
        envManager.SpawnBuilding();
    }
}
