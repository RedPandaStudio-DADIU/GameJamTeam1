using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    void FixedUpdate()
    {
        if (gameObject.transform.position.y < 0){
            Destroy(gameObject);
        }
    }
}
