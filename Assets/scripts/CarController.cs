using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject car;
    [SerializeField] GameObject specialCar;
    [SerializeField] float startDelay = 3.0f;
    [SerializeField] float minIntervalTime = 4.0f;
    [SerializeField] float maxIntervalTime = 6.0f;
    [SerializeField] float maxIntervalTimeSpecialCar = 6.0f;
    [SerializeField] float minReactionDistance = 10.0f;
    [SerializeField] float maxReactionDistance = 25.0f;
    [SerializeField] float startDistance;
    [SerializeField] float collisionRadius = 10.0f;
    [SerializeField] EnvSpawner envManager;
    [SerializeField] float positionY;

    private Transform cyclistTransform;
    private Transform doorPosition;
    private GameObject player;
    private PlayerController playerController;
    private bool stopInstantiating = false;
    private Vector3 spawnPosition;
    private Coroutine carCoroutine;
    private Coroutine specialCarCoroutine;

    private bool isNormalCarSpawned = false;
    private bool isSpecialCarSpawned = false;
    private int startNormalCarSpawned = 0;
    private int startSpecialCarSpawned = 0;
    private int numberOfCarsStart = 4;


    void Start()
    {
        cyclistTransform = GameObject.FindWithTag("Player").transform; 
        doorPosition = GameObject.FindWithTag("Finish").transform; 

        carCoroutine = StartCoroutine(SpawnCarsRandomly());
        specialCarCoroutine = StartCoroutine(SpawnSpecialCarsRandomly());
 
        player = GameObject.Find("cycler");

        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }

        envManager = FindObjectOfType<EnvSpawner>();
        minReactionDistance = 10.0f;
        maxReactionDistance = 25.0f;
        positionY = 4.5f;

        isNormalCarSpawned = true;
        startDistance = 10f;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerController != null){
            if (!playerController.getCanMove()){
                //stop spawning
                StopCoroutine(carCoroutine);
                StopCoroutine(specialCarCoroutine);

            }
        }

        float randomDist = Random.Range(minReactionDistance, maxReactionDistance);
        spawnPosition = new Vector3(cyclistTransform.position.x+randomDist, 2.2f, cyclistTransform.position.z-startDistance); 


        if(IsTraffic(spawnPosition, Vector3.forward)){
            stopInstantiating = true;
        } else{
            stopInstantiating = false;
        }


        if(startNormalCarSpawned == numberOfCarsStart && startSpecialCarSpawned==numberOfCarsStart){
            isNormalCarSpawned = true;
            isSpecialCarSpawned = true;
        } else if(startNormalCarSpawned == numberOfCarsStart){
            isNormalCarSpawned = false;
            isSpecialCarSpawned = true;
        }

    }

     IEnumerator SpawnCarsRandomly()
    {
        yield return new WaitForSeconds(Random.Range(0, startDelay));

        while (true)
        {
            SpawnCar();
            float interval = Random.Range(minIntervalTime, maxIntervalTime);
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator SpawnSpecialCarsRandomly()
    {
        yield return new WaitForSeconds(Random.Range(0, startDelay));

        while (true)
        {
            SpawnSpecialCar();
            float interval = Random.Range(minIntervalTime, maxIntervalTimeSpecialCar);
            yield return new WaitForSeconds(interval);
        }
    }

    private void SpawnCar(){
        if (!IsCloseToFinish() && isNormalCarSpawned){
            Vector3 nextRoadSpawnPos = envManager.GetSpawnPosition();
            nextRoadSpawnPos.x = nextRoadSpawnPos.x - 20.0f;
            nextRoadSpawnPos.y = positionY;
            nextRoadSpawnPos.z = cyclistTransform.position.z;
            GameObject instance = Instantiate(car,  nextRoadSpawnPos, Quaternion.identity);

            if (startNormalCarSpawned < numberOfCarsStart){
                startNormalCarSpawned++;
            }
        }
    }

    private void SpawnSpecialCar(){
        if (!stopInstantiating && isSpecialCarSpawned)
        {
            if (!IsCloseToFinish(30.0f)){

                Debug.Log("Special Car going now!");

                if(envManager.specialCarSpawningPoints.Count > 0){
                    Debug.Log("AAAAAAAAAAAAA");
                    // for(int i=0; i<envManager.specialCarSpawningPoints.Count; i++){
                    //     float xPosition = envManager.specialCarSpawningPoints[0].transform.position.x;
                    //     if(xPosition > cyclistTransform.position.x){

                    // envManager.specialCarSpawningPoints.gameObject.

                        Transform firstChild = envManager.specialCarSpawningPoints[0].transform.GetChild(0);  
                        GameObject child = firstChild.gameObject;
                        
                        Collider crossCollider = child.GetComponent<Collider>();
                        if(crossCollider != null){
                            float crossWidth = crossCollider.bounds.size.x;
                            spawnPosition.x = envManager.specialCarSpawningPoints[0].transform.position.x + crossWidth/2;

                        }


                            // spawnPosition.x = envManager.specialCarSpawningPoints[0].transform.position.x;
                        //     break;
                        // }
                    // }
                }

                spawnPosition.y = 4f; //positionY;
                spawnPosition.z = cyclistTransform.position.z+startDistance;
                GameObject instance = Instantiate(specialCar, spawnPosition, Quaternion.Euler(0, -90, 0));
                if (startSpecialCarSpawned < numberOfCarsStart){
                    startSpecialCarSpawned++;
                }
            
                
            }
        }
    }


    private bool IsCloseToFinish(float distance=20.0f)
    {
        if ((doorPosition.position.x - cyclistTransform.position.x) < distance){
            return true;
        } else {
            return false;
        }
    }

    private bool IsTraffic(Vector3 position, Vector3 direction, float checkDistance = 10.0f)
    {

        Collider[] colliders = Physics.OverlapSphere(position, collisionRadius);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Car")) 
            {
                return true;  
            }
        }
        return false;
    }

}
