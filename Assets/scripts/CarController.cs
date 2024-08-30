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
    [SerializeField] float startDistance = 5.0f;
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
        positionY = 4.25f;

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
        spawnPosition = new Vector3(cyclistTransform.position.x+randomDist, 1.8f, cyclistTransform.position.z+startDistance); 


        if(IsTraffic(spawnPosition, Vector3.forward)){
            stopInstantiating = true;
        } else{
            stopInstantiating = false;
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
        if (!IsCloseToFinish()){
            Vector3 nextRoadSpawnPos = envManager.GetSpawnPosition();
            nextRoadSpawnPos.x = nextRoadSpawnPos.x - 20.0f;
            nextRoadSpawnPos.y = positionY;
            nextRoadSpawnPos.z = cyclistTransform.position.z;
            GameObject instance = Instantiate(car,  nextRoadSpawnPos, Quaternion.identity);
        }
    }

     private void SpawnSpecialCar(){

        if (!stopInstantiating)
        {
            if (!IsCloseToFinish(10.0f)){
                spawnPosition.y = positionY;
                GameObject instance = Instantiate(specialCar, spawnPosition, Quaternion.Euler(0, -90, 0));
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
