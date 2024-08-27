using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce=5;
    public float moveSpeed=10;
    public float gravityModifier=1;
    public bool isOnGround = true;
    public bool gameOver = false;
    //private Animator playerAnim;
    //public ParticleSystem explosionParticle;
    //public ParticleSystem dirtParticle;
    //public AudioClip jumpSound;
    //public AudioClip crashSound;
    //private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerRb.constraints = RigidbodyConstraints.FreezePositionZ;
        playerRb.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        //playerAudio = GetComponent<AudioSource>();
       // playerRb.AddForce(Vector3.up * 1000);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver){
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            //playerAnim.SetTrigger("Jump_trig");
            //dirtParticle.Stop();
            //playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(horizontalInput * moveSpeed, playerRb.velocity.y);

    }

    private void OnCollisionEnter(Collision collision){
        //isOnGround = true;
        if (collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
            //dirtParticle.Play();

        }else if (collision.gameObject.CompareTag("Finish")){
            gameOver = true;
            Debug.Log("Game Over");
            //playerAnim.SetBool("Death_b", true);
            //playerAnim.SetInteger("DeathType_int", 1);
            //explosionParticle.Play();
            //dirtParticle.Stop();
            //playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
