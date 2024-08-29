using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro;

public class StoryManager : MonoBehaviour
{
    public Image[] storyImages;         // 用于存储所有剧情图片的数组
    public TextMeshProUGUI storyText;             // 用于显示剧情文本的 Text 组件
    public string[] storyTexts;         // 用于存储所有剧情文本的数组
    public float displayDuration = 3f;  // 每张图片和文本显示的时间
    public string nextSceneName = "Level1"; // 剧情结束后要加载的场景名称

    private int currentIndex = 0;       // 当前展示的图片和文本的索引
    private float timer = 0f;
    private bool isStoryPlaying = false; // 用于控制是否开始播放剧情

    public GameObject canvasToHide;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Image img in storyImages)
        {
            img.gameObject.SetActive(false);
        }
        storyText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        


        if (isStoryPlaying)
        {
            timer += Time.deltaTime;

            // 如果超过显示时间，则切换到下一个剧情元素
            if (timer >= displayDuration)
            {
                currentIndex++;
                if (currentIndex < storyImages.Length)
                {
                    ShowStoryElement();
                }
                else
                {
                    // 如果所有剧情播放完毕，切换到下一个场景
                    SceneManager.LoadScene("Level1");
                }

                timer = 0f;
            }
        }
    }

     public void StartStory()
    {
        // 在按钮点击后开始播放剧情
        isStoryPlaying = true;
        currentIndex = 0;
        timer = 0f;
        ShowStoryElement();
          if (canvasToHide != null)
        {
            canvasToHide.SetActive(false);
        }
    }

    void ShowStoryElement()
    {
        // 显示当前索引的图片和文本
        for (int i = 0; i < storyImages.Length; i++)
        {
            storyImages[i].gameObject.SetActive(i == currentIndex);
        }
        storyText.text = storyTexts[currentIndex];
    }
}
