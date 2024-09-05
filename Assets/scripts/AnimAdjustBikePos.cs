using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAdjustBikePos : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    private Transform originalBikeTransform;

    [SerializeField] GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        originalBikeTransform = transform;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = character.transform.position;
        transform.localScale = character.transform.localScale;  

    }
}
