using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;  // 角色对象
    private Vector3 offset;    // 摄像机与角色的偏移量

    // Start is called before the first frame update
    void Start()
    {
        // 计算摄像机与角色之间的初始偏移量
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 更新摄像机的位置，使其保持与角色的偏移量
        transform.position = player.transform.position + offset;
    }
}
