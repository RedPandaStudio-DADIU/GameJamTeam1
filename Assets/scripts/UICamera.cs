using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        // 确保主相机已经被设置
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // 设置相机的位置
        mainCamera.transform.position = new Vector3(0, 1, -10);

        // 设置相机的旋转
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);

        // 设置相机的视角 (FOV)
        mainCamera.fieldOfView = 60f;

    }
}
