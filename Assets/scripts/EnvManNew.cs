using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManNew : MonoBehaviour
{
    [SerializeField] GameObject[] buildingPrefabs;
    [SerializeField] GameObject streetPrefab;
    [SerializeField] GameObject crossStreetPrefab;

    // [SerializeField] int numberOfStreetsInitial = 15;
    // [SerializeField] int numberOfCrossInitial = 2;

    [SerializeField] int numberOfBuildingsInitial = 10;
    public List<GameObject> specialCarSpawningPoints = new List<GameObject>();
    private Vector3 spawnPosition= new Vector3(0,0,0);
    private Vector3 spawnPositionRoad= new Vector3(0,0,0);
    // private Vector3 spawnCrossPosition= new Vector3(0,0,0);
    private Vector3 initialPlayerPosition;
    private Vector3 currentPlayerPosition;
    private Transform doorPosition;
    private Transform cyclistTransform;
    private bool startCollapse = false;
    [SerializeField] float roadVisibiityLimit;
    [SerializeField] NavMeshManager navMeshManager; 


    // Start is called before the first frame update
    void Start()
    {
        cyclistTransform = GameObject.FindWithTag("Player").transform; 
        initialPlayerPosition = cyclistTransform.position;
        doorPosition = GameObject.FindWithTag("Finish").transform; 
        spawnPosition.x = cyclistTransform.position.x - 100f;

        InitializeObjects();        
        roadVisibiityLimit = 3;  
         
    }

    public void InitializeObjects(){
        for(int i=0; i<numberOfBuildingsInitial; i++){
            if(i%4==0){
                SpawnCrossroad();
            }
            SpawnBuilding();
        }
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
            roadVisibiityLimit -= 0.06f*Time.deltaTime;
        }

        specialCarSpawningPoints.RemoveAll(obj => obj.transform.position.x < currentPlayerPosition.x);

    }

    public void SpawnBuilding(){
        int index = Random.Range(0, buildingPrefabs.Length);
        GameObject buildingPrefab = buildingPrefabs[index];
        GameObject building = Instantiate(buildingPrefab, spawnPosition, buildingPrefab.transform.rotation);
        // Debug.Log("Why not working?");
        Transform firstChild = building.transform.GetChild(0);  
        GameObject childBuilding = firstChild.gameObject;
        
        Collider buildingCollider = childBuilding.GetComponent<Collider>();
        if(buildingCollider != null){
            // Debug.Log("Test");
            float buildingWidth = buildingCollider.bounds.size.x;
            spawnPositionRoad.x = spawnPosition.x;
            Debug.Log("Building width: "+ buildingWidth);
            spawnPositionRoad = spawnPosition;

            SpawnStreet(buildingWidth);

            spawnPosition += new Vector3(buildingWidth, 0, 0);
        }

        if(index==4){
            specialCarSpawningPoints.Add(building);

        }
    }

    public void SpawnStreet(float buildingWidth){

        GameObject street = Instantiate(streetPrefab, spawnPositionRoad,streetPrefab.transform.rotation);
        
        Transform firstChild = street.transform.GetChild(0);  
        GameObject child = firstChild.gameObject;
        Collider streetCollider = child.GetComponent<Collider>();

        if(streetCollider != null){

            float streetWidth = streetCollider.bounds.size.x;
            float scaleFactor = buildingWidth / streetWidth;
            street.transform.localScale = new Vector3(
                street.transform.localScale.x* scaleFactor,
                street.transform.localScale.y,
                street.transform.localScale.z 
            );
    

            Collider newStreetCollider = child.GetComponent<Collider>();
            if(newStreetCollider != null){
                float newStreetWidth = newStreetCollider.bounds.size.x;
                spawnPositionRoad += new Vector3(newStreetWidth, 0, 0);
            }
        } 

    }

    public void SpawnCrossroad(){
        Vector3 crossPosition = new Vector3(spawnPosition.x, 0f, -27.85f);
        GameObject cross = Instantiate(crossStreetPrefab, crossPosition,crossStreetPrefab.transform.rotation);

        Transform firstChild = cross.transform.GetChild(0);  
        GameObject child = firstChild.gameObject;
        
        Collider crossCollider = child.GetComponent<Collider>();
        if(crossCollider != null){
            float crossWidth = crossCollider.bounds.size.x;
            spawnPosition += new Vector3(crossWidth-0.1f, 0, 0);
        }
        specialCarSpawningPoints.Add(cross);
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
