using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce=10;
    public float moveSpeed=10;
    public float gravityModifier=2;
    public bool isOnGround = true;
    public bool gameOver = false;

    public float currentSpeed = 0;  // 
    public float acceleration = 2f;   // 
    public float deceleration = 2f; 
    private bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerRb.constraints = RigidbodyConstraints.FreezePositionZ;
        playerRb.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        canMove = true;
        //playerAudio = GetComponent<AudioSource>();
       // playerRb.AddForce(Vector3.up * 1000);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove){
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver){
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                //playerAnim.SetTrigger("Jump_trig");
                //dirtParticle.Stop();
                //playerAudio.PlayOneShot(jumpSound, 1.0f);
            }

            float horizontalInput = Input.GetAxis("Horizontal");
            //playerRb.velocity = new Vector2(horizontalInput * moveSpeed, playerRb.velocity.y);
            if (horizontalInput > 0)
            {
                // speed up
                currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, acceleration * Time.deltaTime);
            }
            else if (horizontalInput < 0)
            {
                // slow down
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
            }
            else
            {
                // gradully stop
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
            }
            playerRb.velocity = new Vector2(currentSpeed, playerRb.velocity.y);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Finish")){
            gameOver = true;
            Debug.Log("Game Over");
            SceneManager.LoadScene("Ending");
        }
    }


    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Ground")){
            isOnGround = true;

        } else if (collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("SpecialCar")){
            Debug.Log("Collision!");
            canMove = false;
            Time.timeScale = 0;
        } else if (collision.gameObject.CompareTag("Finish")){
            gameOver = true;
            Debug.Log("Game Over");
            Time.timeScale = 0;

        }
    }

    public bool getCanMove(){
        return this.canMove;
    }
}
