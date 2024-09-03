using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce=35;
    public float moveSpeed=10;
    public float gravityModifier=3;
    public bool isOnGround = true;
    public bool gameOver = false;

    public float currentSpeed = 0;  
    public float acceleration = 3f;   
    public float deceleration = 20f; 
    private bool canMove = false;
    private bool speedUp = false;


    public Vector3 startPosition;

    private Timer timer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //playerAnim = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -10, 0)* gravityModifier;
        playerRb.constraints = RigidbodyConstraints.FreezePositionZ;
        playerRb.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        canMove = true;

        jumpForce=30;
        moveSpeed=15;
        gravityModifier=3;
        acceleration = 10f;
        deceleration = 45f;

        gameOver = false;
        isOnGround = true;
        currentSpeed = 0;
        Time.timeScale = 1;

       startPosition = new Vector3(400, 2, -6);
        transform.position = startPosition;
        timer = FindObjectOfType<Timer>(); 
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
            float verticalInput = Input.GetAxis("Vertical");

            //playerRb.velocity = new Vector2(horizontalInput * moveSpeed, playerRb.velocity.y);
            if (horizontalInput > 0)
            {
                // speed up
                if (verticalInput > 0){
                    currentSpeed = Mathf.MoveTowards(currentSpeed, 2*moveSpeed, 5*acceleration * Time.deltaTime);
                    speedUp = true;
                }else{
                    if (speedUp){
                        currentSpeed = Mathf.MoveTowards(currentSpeed, 0, 20*deceleration * Time.deltaTime);
                        speedUp = false;
                    } else{
                        currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, acceleration * Time.deltaTime);
                    }
                }
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


        if (transform.position.y < -10 && !gameOver) // 这里 -10 是你可以设置的阈值
        {
            gameOver = true;  // 标记游戏结束
            Debug.Log("Game Over: Player fell down");
            
            

            // 加载结束场景
            SceneManager.LoadScene("Failing");  // 加载结束场景或显示游戏结束信息
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Finish")){
            gameOver = true;
            
            timer.StopTimer(); // Stop and save the timer
            
            Debug.Log("Game Over(success)");
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
            SceneManager.LoadScene("Failing");
        } else if (collision.gameObject.CompareTag("Finish")){
            gameOver = true;
            timer.StopTimer();
            Debug.Log("Game Over?");
            Time.timeScale = 0;
            SceneManager.LoadScene("Ending");
        }
    }

    public bool getCanMove(){
        return this.canMove;
    }
}
