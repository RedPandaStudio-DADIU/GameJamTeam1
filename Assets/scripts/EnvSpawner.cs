using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] buildingPrefabs;
    [SerializeField] GameObject streetPrefab;
    [SerializeField] GameObject crossStreetPrefab;

    [SerializeField] int numberOfStreetsInitial = 15;
    // [SerializeField] int numberOfCrossInitial = 2;

    [SerializeField] int numberOfBuildingsInitial = 12;

    private Vector3 spawnPosition= new Vector3(0,0,0);
    private Vector3 spawnPositionRoad= new Vector3(0,0,0);
    // private Vector3 spawnCrossPosition= new Vector3(0,0,0);
    private Vector3 initialPlayerPosition;
    private Vector3 currentPlayerPosition;
    private Transform doorPosition;
    private Transform cyclistTransform;
    private bool startCollapse = false;
    [SerializeField] float roadVisibiityLimit;


    void Start()
    {
        cyclistTransform = GameObject.FindWithTag("Player").transform; 
        initialPlayerPosition = cyclistTransform.position;
        doorPosition = GameObject.FindWithTag("Finish").transform; 

        InitializeObjects();

        // for(int i =0; i<numberOfStreetsInitial; i++){
        //     SpawnStreet();
        // }

        // for(int i=0; i<numberOfBuildingsInitial; i++){
        //     SpawnBuilding();
        // }
        
        roadVisibiityLimit = 3;

    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerPosition = cyclistTransform.position;

        if(currentPlayerPosition.x >= ((doorPosition.position.x - initialPlayerPosition.x)/2)){
            startCollapse = true;
        }

        if (startCollapse){
            // CollapseRoad();
            roadVisibiityLimit -= 0.04f*Time.deltaTime;
        }
    }

    public void InitializeObjects(){
        for(int i =0; i<numberOfStreetsInitial; i++){
            if(i%4==0){
                SpawnCrossroad();
            }
            SpawnStreet();
        }

        for(int i=0; i<numberOfBuildingsInitial; i++){
            SpawnBuilding();
        }
    }

    public void SpawnStreet(){
        GameObject street = Instantiate(streetPrefab, spawnPositionRoad,streetPrefab.transform.rotation);
        Collider streetCollider = street.GetComponent<Collider>();
        if(streetCollider != null){
            float streetWidth = streetCollider.bounds.size.x;
            spawnPositionRoad += new Vector3(streetWidth, 0, 0);
        }
    }
   public void SpawnCrossroad(){
        Vector3 crossPosition = new Vector3(spawnPosition.x, -0.75f, spawnPosition.z);
        GameObject cross = Instantiate(crossStreetPrefab, crossPosition, Quaternion.identity);


        Transform firstChild = cross.transform.GetChild(0);  
        GameObject child = firstChild.gameObject;
        
        Collider crossCollider = child.GetComponent<Collider>();
        if(crossCollider != null){
            float crossWidth = crossCollider.bounds.size.x;
            spawnPosition += new Vector3(crossWidth, 0, 0);
        }


        // Collider crossCollider = cross.GetComponent<Collider>();
        // if(crossCollider != null){
        //     float crossWidth = crossCollider.bounds.size.x;
        //     spawnPosition += new Vector3(crossWidth, 0, 0);
        // }
    }


    public void SpawnBuilding(){
        GameObject buildingPrefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
        GameObject building = Instantiate(buildingPrefab, spawnPosition, buildingPrefab.transform.rotation);
        // Debug.Log($"{spawnPosition}: position of the building.");
        Transform firstChild = building.transform.GetChild(0);  
        GameObject childBuilding = firstChild.gameObject;
        
        Collider buildingCollider = childBuilding.GetComponent<Collider>();
        if(buildingCollider != null){
            float buildingWidth = buildingCollider.bounds.size.x;
            spawnPosition += new Vector3(buildingWidth, 0, 0);
        }
        
    }

    public Vector3 GetSpawnPosition(){
        return this.spawnPosition;
    }

    public Vector3 GetStreetSpawnPosition(){
        return this.spawnPositionRoad;
    }

    // public Vector3 GetCrossSpawnPosition(){
    //     return this.spawnCrossPosition;
    // }


    public void SetSpawnPosition(float position){
        this.spawnPosition += new Vector3(position, 0, 0);
    }

    public void SetStreetSpawnPosition(float position){
        spawnPositionRoad += new Vector3(position, 0, 0);
    }

    public float GetRoadVisibility(){
        return this.roadVisibiityLimit;
    }

    public void SetRoadVisibility(float visibiityLimit){
        this.roadVisibiityLimit = visibiityLimit;
    }

    public bool GetStartCollapse(){
        return this.startCollapse;
    }

    public Quaternion GetRoadRotation(){
        return this.streetPrefab.transform.rotation;
    }
}
