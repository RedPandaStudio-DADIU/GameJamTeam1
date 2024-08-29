using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetRepeat : MonoBehaviour
{
    public GameObject[] streetPrefabs;
    public int numberOfStreets = 3;
    public float roadLength = 70;
    public Transform playerTransform;

    public Queue<GameObject> streetQueue = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Vector3 initialPosition = new Vector3(-120, 0, 7.9f); // 设置第一个道路片段的初始位置
        Quaternion initialRotation = Quaternion.Euler(0, 90, 180); // 设置初始旋转

        
        for (int i = 0; i < numberOfStreets; i++)
        {
             Vector3 spawnPosition = initialPosition + new Vector3(i * roadLength, 0, 0);

            GameObject road = Instantiate(GetRandomRoadPrefab(), spawnPosition, initialRotation);
            streetQueue.Enqueue(road);
        }
    }

    // Update is called once per frame
    void Update()
    {
         if (playerTransform.position.x > streetQueue.Peek().transform.position.x + roadLength)
        {
            // move and repeat the roads
            GameObject movedRoad = streetQueue.Dequeue();
            movedRoad.transform.position += new Vector3(numberOfStreets * roadLength, 0, 0);
            streetQueue.Enqueue(movedRoad);
        }
    }
     GameObject GetRandomRoadPrefab()
    {
        int randomIndex = Random.Range(0, streetPrefabs.Length);
        return streetPrefabs[randomIndex];
    }

}
