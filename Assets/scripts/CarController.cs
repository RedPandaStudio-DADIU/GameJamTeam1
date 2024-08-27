using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject car;
    [SerializeField] float startDelay = 3.0f;
    [SerializeField] float repeatTime = 0.0f;
    [SerializeField] float minIntervalTime = 4.0f;
    [SerializeField] float maxIntervalTime = 12.0f;


    void Start()
    {
        repeatTime = 5.0f;
        InvokeRepeating("SpawnCar", startDelay, repeatTime);
    }

    // Update is called once per frame
    void Update()
    {
        repeatTime = Random.Range(minIntervalTime,maxIntervalTime);
    }

    private void SpawnCar(){
        GameObject instance = Instantiate(car);

    }

}
