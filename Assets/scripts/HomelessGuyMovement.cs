using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;

public class HomelessGuyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] CarController carController;    
    [SerializeField] NavMeshManager navMeshManager; 
    [SerializeField] Animator animator;
    private Transform cyclistTransform;
    private float initial_offset = 0f;
    private bool canMove = false;
    private NavMeshAgent agent;

    void Start()
    {
        carController = FindObjectOfType<CarController>();
        cyclistTransform = GameObject.FindWithTag("Player").transform; 
        initial_offset = 10f;
        transform.rotation = Quaternion.identity;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Normal cars: " + carController.GetNumberOfNormalCars() + " Special cars: " + carController.GetNumberOfSpecialCars());
        if(carController.GetNumberOfNormalCars()>= 1 && carController.GetNumberOfSpecialCars()>= 1){
            if(!canMove){
                Debug.Log("Spawning Homeless Guy");
                Spawn();
                canMove = true;
                agent = gameObject.AddComponent<NavMeshAgent>();
                agent.speed = 7f;
                agent.angularSpeed = 0.5f;
                // agent.autoBraking = false; 
                agent.acceleration = 15f;
                agent.baseOffset=0f;
                agent.height = 0.6f;
                // navMeshManager.UpdateNavMesh();

            }

            if(canMove && cyclistTransform != null)
            {
                agent.SetDestination(cyclistTransform.position);
                bool isMoving = agent.velocity.magnitude > 0.1f;
                animator.SetBool("isRunning", isMoving);
            }

            
        }

        
    }


    private void Spawn(){
        transform.position = new Vector3(cyclistTransform.position.x - initial_offset, cyclistTransform.position.y+3f, cyclistTransform.position.z);
        transform.Rotate(Vector3.up, 90);
    }
}
