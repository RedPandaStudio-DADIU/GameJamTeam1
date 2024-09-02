using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
      public void OnExitButtonClick()
    {
#if UNITY_EDITOR
        // 退出编辑器中的播放模式
        EditorApplication.isPlaying = false;
#else
        // 退出应用程序
        Application.Quit();
#endif
    }
    
}
