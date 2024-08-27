using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCarMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool isMoving = false;
    [SerializeField] float speed = 0;    
    void Start()
    {
        isMoving = true;
        speed = -5.5f;
    }

    // Update is called once per frame
    void Update()
    {
         if (isMoving){
            // # Move the Car
            transform.position=new Vector3(transform.position.x, transform.position.y, transform.position.z+speed*Time.deltaTime);
        }
    }
}
