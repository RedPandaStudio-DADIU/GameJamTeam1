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
        visibiityLimit= 30f;
    }

    public void CheckVisibility(){

        if (gameObject.tag == "Ground")
        {
            float roadWidth = GetComponent<Collider>().bounds.size.x;
            if(transform.position.x < Camera.main.transform.position.x - 2* roadWidth)
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

    void RepositionRoad(float roadWidth)
    {
        Vector3 newPosition = envManager.GetStreetSpawnPosition();
        transform.position = newPosition;
        envManager.SetStreetSpawnPosition(roadWidth);
    }

    void ReplaceBuilding()
    {
        Debug.LogWarning("BuildingCreation");
        Destroy(gameObject);
        envManager.SpawnBuilding();
    }
}
