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
    public bool isMoving = false;

    public Vector3 startPosition;
    private Timer timer;
    [SerializeField] Animator characterAnimator;
    [SerializeField] Animator bikeAnimator;
    // [SerializeField] Animator wheelBackAnimator;


    [SerializeField] AudioClip scooterDriveSpeed;
    [SerializeField] AudioClip scooterCrashImpact;
    [SerializeField] AudioClip scooterCrashExplosion;
    [SerializeField] AudioClip scooterJump;
    [SerializeField] AudioClip scooterDriveNormal;
    [SerializeField] AudioClip scooterStart;
    [SerializeField] AudioClip scooterIdleLoop;
    [SerializeField] AudioClip scooterAccelerate;
    [SerializeField] AudioSource source;

    void Awake () {

        source = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        source.PlayOneShot(scooterStart,0.5f);
        //playerAnim = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -10, 0)* gravityModifier;
        playerRb.constraints = RigidbodyConstraints.FreezePositionZ;
        playerRb.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        canMove = true;

        jumpForce=33;
        moveSpeed=15;
        gravityModifier=3;
        acceleration = 10f;
        deceleration = 45f;

        gameOver = false;
        isOnGround = true;
        currentSpeed = 0;
        Time.timeScale = 1;

        startPosition = new Vector3(400, 2.0f, -17);
        transform.position = startPosition;
        timer = FindObjectOfType<Timer>(); 
        //playerAudio = GetComponent<AudioSource>();
       // playerRb.AddForce(Vector3.up * 1000);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpeed > 0){
            isMoving = true;
            
        } else{
            isMoving=false;
        }
        characterAnimator.SetBool("isMoving", isMoving);
        bikeAnimator.SetBool("isMoving", isMoving);
//scale - 0.03

        if (canMove){
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isOnGround && !gameOver){
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;

                if (source.isPlaying)
                {
                    source.Stop();
                }
                source.PlayOneShot(scooterJump,1.0f);
                characterAnimator.SetBool("isJumping", true);
                bikeAnimator.SetBool("isJumping", true);

                // source.PlayOneShot(scooterJump,1.0f);

                //playerAnim.SetTrigger("Jump_trig");
                //dirtParticle.Stop();
                //playerAudio.PlayOneShot(jumpSound, 1.0f);
            }

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            //playerRb.velocity = new Vector2(horizontalInput * moveSpeed, playerRb.velocity.y);
           
            bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        
            // while(currentSpeed == 0.0f){
            //     source.PlayOneShot(scooterIdleLoop,0.2f);
            // }
            
            if (horizontalInput > 0)
            {
                source.PlayOneShot(scooterDriveNormal,0.1f);

                // speed up
                if (isShiftPressed  ){
                    currentSpeed = Mathf.MoveTowards(currentSpeed, 2*moveSpeed, 5*acceleration * Time.deltaTime);
                    speedUp = true;
                    if (source.isPlaying && source.clip != scooterDriveSpeed)
                    {
                        source.Stop();
                    }
                    source.PlayOneShot(scooterDriveSpeed,1.0f);

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

            if(currentSpeed == 0.0){
                if (source.isPlaying)
                {
                    source.Stop();
                }
                source.PlayOneShot(scooterIdleLoop,0.8f);
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
        // Debug.Log("Collision!!!"+collision.gameObject.name);
        if (collision.gameObject.CompareTag("Ground")){
            // Debug.Log("GROUND!!");
            characterAnimator.SetBool("isJumping", false);
            bikeAnimator.SetBool("isJumping", false);
            if (source.isPlaying && source.clip == scooterJump)
            {
                source.Stop();
            }
            isOnGround = true;
        }else if(collision.gameObject.CompareTag("Crossroad")) {
            Transform firstChild = collision.gameObject.transform.GetChild(0);  
            GameObject child = firstChild.gameObject;

            Debug.Log("Crossroad!! Player: " + transform.position.y + " Crossroad: " + collision.gameObject.transform.position.y + " cross child: " + child.transform.position.y);
        } else if (collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("SpecialCar")){
            Debug.Log("Collision!");
            canMove = false;
            Time.timeScale = 0;
            if (source.isPlaying )
            {
                source.Stop();
            }
            source.PlayOneShot(scooterCrashImpact,1.0f);

            StartCoroutine(WaitAndLoadScene(2.0f, "Failing"));

            // StartCoroutine(CrashSoundScene(scooterCrashImpact));
            //SceneManager.LoadScene("Failing");
        } else if (collision.gameObject.CompareTag("Finish")){
            gameOver = true;
            timer.StopTimer();
            Debug.Log("Game Over?");
            Time.timeScale = 0;
            SceneManager.LoadScene("Ending");
        }  else if (collision.gameObject.CompareTag("Chasing")){
            Debug.Log("Homeless guy caught you!");
            source.PlayOneShot(scooterCrashExplosion,1.0f);
            StartCoroutine(WaitAndLoadScene(2.0f, "Failing")); 
        }
    }

    private IEnumerator WaitAndLoadScene(float waitTime, string sceneName)
{
    // 等待指定的时间
    yield return new WaitForSecondsRealtime(waitTime);

    // 加载场景
    SceneManager.LoadScene(sceneName);
}

    public bool getCanMove(){
        return this.canMove;
    }

    public bool GetIsMoving(){
        return this.isMoving;
    }
}
