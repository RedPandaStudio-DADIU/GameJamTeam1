using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] bool isMoving = false;
    [SerializeField] float speed = 0;    

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        speed = -5.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving){
            // # Move the Car
            transform.position=new Vector3(transform.position.x+speed*Time.deltaTime, transform.position.y, transform.position.z);
            // transform.position =  new Vector3(transform.position.x-0.02f, transform.position.y, transform.position.z) * Time.deltaTime;
            // # Check collision
            // # Check end of camera viewpoint
        }
    }
}
