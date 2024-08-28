using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] bool isMoving = false;
    [SerializeField] float speed = 0;    
    private bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        speed = -5.5f;
        canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving && canMove){
            // # Move the Car
            transform.position=new Vector3(transform.position.x+speed*Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    public void setCanMove(bool canMove){
        this.canMove = canMove;
    }
}
