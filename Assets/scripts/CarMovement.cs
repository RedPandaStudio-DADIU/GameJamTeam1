using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] bool isMoving = false;
    [SerializeField] float speed = 0;    
    private bool canMove = false;
    [SerializeField] Animator carAnimator;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        carAnimator.SetBool("isMoving", isMoving);
        speed = -5.5f;
        canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving && canMove){
            // # Move the Car
            transform.position=new Vector3(transform.position.x+speed*Time.deltaTime, transform.position.y, transform.position.z);
            // transform.localScale = new Vector3(
            //     400f,400f, 400f
            // );

        }
    }

    public void setCanMove(bool canMove){
        this.canMove = canMove;
    }
}
