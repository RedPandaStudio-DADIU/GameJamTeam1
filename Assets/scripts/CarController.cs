using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject car;
    [SerializeField] GameObject specialCar;
    [SerializeField] float startDelay = 3.0f;
    [SerializeField] float repeatTime = 0.0f;
    [SerializeField] float minIntervalTime = 4.0f;
    [SerializeField] float maxIntervalTime = 12.0f;
    [SerializeField] float minReactionDistance = 5.0f;
    [SerializeField] float maxReactionDistance = 7.0f;
    [SerializeField] float startDistance = 5.0f;
    [SerializeField] float collisionRadius = 30.0f;

    private Transform cyclistTransform;

    void Start()
    {
        cyclistTransform = GameObject.FindWithTag("Player").transform; 
        repeatTime = 5.0f;
        InvokeRepeating("SpawnCar", startDelay, repeatTime);
        InvokeRepeating("SpawnSpecialCar", startDelay, repeatTime);

    }

    // Update is called once per frame
    void Update()
    {
        repeatTime = Random.Range(minIntervalTime,maxIntervalTime);
    }

    private void SpawnCar(){
        GameObject instance = Instantiate(car);
    }

     private void SpawnSpecialCar(){
        float randomDist = Random.Range(minReactionDistance, maxReactionDistance);
        Vector3 spawnPosition = new Vector3(cyclistTransform.position.x+randomDist, 1.8f, cyclistTransform.position.z+startDistance); 

        if (!IsTraffic(spawnPosition, Vector3.forward))
        {
            GameObject instance = Instantiate(specialCar, spawnPosition, Quaternion.Euler(0, -90, 0));
        }
    }


    private bool IsTraffic(Vector3 position, Vector3 direction, float checkDistance = 30.0f)
    {
        // Collider[] colliders = Physics.OverlapSphere(position, collisionRadius);
        // foreach (Collider col in colliders)
        // {
        //     if (col.CompareTag("Car")) 
        //     {
        //         return true;  
        //     }
        // }
        // return false;

        // Vector3 futurePosition = position + direction.normalized * checkDistance;
        // colliders = Physics.OverlapSphere(futurePosition, collisionRadius);
        // foreach (Collider col in colliders)
        // {
        //     if (col.CompareTag("Car"))
        //     {
        //         return true;
        //     }
        // }

        return false;
    }

}
