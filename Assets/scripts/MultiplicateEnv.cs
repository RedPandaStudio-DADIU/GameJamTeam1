using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicateEnv : MonoBehaviour
{
    // [SerializeField] EnvSpawner envManager;
    [SerializeField] EnvManNew envManager;

    [SerializeField] float visibiityLimit;
    private bool isCollapsing = false;
    private Vector3 initialPosition;

    private float roadWidth;

    // Start is called before the first frame update
    void Start()
    {
        envManager = FindObjectOfType<EnvManNew>();
        visibiityLimit= 65f;
        initialPosition = transform.position;
        if (gameObject.tag == "Ground")
        {
            Transform firstChild = gameObject.transform.GetChild(0);  
            GameObject child = firstChild.gameObject;
            roadWidth = child.GetComponent<Collider>().bounds.size.x;
        }
    }

    public void CheckVisibility(){

        if (gameObject.tag == "Ground")
        {
            // float roadWidth = GetComponent<Collider>().bounds.size.x;
            // Debug.Log("Road width: " + roadWidth);
            float roadVisibiityLimit =  30f; //envManager.GetRoadVisibility();
            if(transform.position.x < Camera.main.transform.position.x - roadVisibiityLimit* roadWidth)
            {
                if(!envManager.GetStartCollapse()){
                    RepositionRoad();
                } if(envManager.GetStartCollapse() && !isCollapsing){
                    CollapseRoad();
                    isCollapsing = true;
                }

                if(transform.position.y < -50f){
                    isCollapsing = false;
                    RepositionRoad();
                }


                RepositionRoad();
                if(envManager.GetStartCollapse() && !isCollapsing){
                    CollapseRoad();
                    isCollapsing = true;
                }

                if(transform.position.x < -200f){
                    isCollapsing = false;
                    RepositionRoad();
                }

            }
        }
        else if (gameObject.tag == "Building"||gameObject.tag == "Crossroad")
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
        StartCoroutine(DelayReposition(100f));
    }
    
    void RepositionRoad()
    {
        Destroy(gameObject);
        // RemoveRigidbody();
        // // transform.position = initialPosition;
        // Vector3 newPosition = envManager.GetStreetSpawnPosition();
        // // Debug.Log("New position from env Manager" + newPosition);
        // transform.position = newPosition;
        // transform.rotation = envManager.GetRoadRotation();
        // envManager.SetStreetSpawnPosition(roadWidth);
        // Vector3 testPosition = envManager.GetStreetSpawnPosition();
        // // Debug.Log("Texting next position from env Manager" + testPosition);

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
        if(envManager.specialCarSpawningPoints.Contains(gameObject)){
            envManager.specialCarSpawningPoints.Remove(gameObject);
        }

        Destroy(gameObject);
        float prob = Random.Range(0f, 1f);
        if(prob <= 0.1){
            envManager.SpawnCrossroad();
        } else{
            envManager.SpawnBuilding();
        }
        envManager.SpawnBuilding();
    }
}
