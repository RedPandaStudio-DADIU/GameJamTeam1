using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro;

public class StoryManager : MonoBehaviour
{
    public Image storyImage;         // 用于显示唯一的剧情图片
    public TextMeshProUGUI storyText;             // 用于显示剧情文本的 Text 组件
    public string[] storyTexts;         // 用于存储所有剧情文本的数组
    public float displayDuration = 4f;  // 每张图片和文本显示的时间
    public string nextSceneName = "Level1"; // 剧情结束后要加载的场景名称

    private int currentIndex = 0;       // 当前展示的文本的索引
    private float timer = 0f;
    private bool isStoryPlaying = false; // 用于控制是否开始播放剧情

    public GameObject canvasToHide;
    public GameObject storycanvas;

    // Start is called before the first frame update
    void Start()
    {
        // Hide the canvas initially
        if (storycanvas != null)
        {
            storycanvas.SetActive(false);  
        }
        
        if (storyImage != null)
        {
            storyImage.gameObject.SetActive(false);  // Hide the image until the story starts
        }

        // Clear the text initially
        storyText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (isStoryPlaying)
        {
            timer += Time.deltaTime;

            // Switch to the next text when the display duration is reached
            if (timer >= displayDuration)
            {
                currentIndex++;
                if (currentIndex < storyTexts.Length)
                {
                    ShowStoryElement();
                }
                else
                {
                    // All texts have been displayed, move to the next scene
                    SceneManager.LoadScene(nextSceneName);
                }

                timer = 0f; // Reset the timer for the next text
            }
        }
    }

    public void StartStory()
    {
        // Start playing the story when the button is clicked
        isStoryPlaying = true;
        storycanvas.SetActive(true);
        currentIndex = 0;
        timer = 0f;

        // Show the story image only once
        if (storyImage != null)
        {
            storyImage.gameObject.SetActive(true);

            RectTransform imageRect = storyImage.GetComponent<RectTransform>();

            // Set the image size as a fixed ratio of the screen size
            imageRect.sizeDelta = new Vector2(Screen.width * 1f, Screen.height * 1.5f); // 80% of screen width, 50% of screen height
            imageRect.anchoredPosition = new Vector2(0, 100); // Center the image with some padding from the top
            imageRect.anchorMin = new Vector2(0.5f, 0.5f); // Anchor to the middle of the screen
            imageRect.anchorMax = new Vector2(0.5f, 0.5f);
            imageRect.pivot = new Vector2(0.5f, 0.5f); // Pivot at the center of the image
      
        }

        ShowStoryElement();

        if (canvasToHide != null)
        {
            canvasToHide.SetActive(false);
        }
    }

    void ShowStoryElement()
    {
        
        
        
        // Only update the text, the image remains the same
        storyText.text = storyTexts[currentIndex];

        // Adjust text properties for auto-sizing and alignment
        RectTransform textRect = storyText.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.5f, 0.5f);
        textRect.anchorMax = new Vector2(0.5f, 0.5f);
        textRect.pivot = new Vector2(0.5f, 0.5f);
        textRect.anchoredPosition = new Vector2(0, 100); // Position the text a bit lower on the screen
        textRect.sizeDelta = new Vector2(Screen.width * 0.8f, Screen.height * 0.5f); // Adjust the size of the text box

        storyText.enableAutoSizing = true;
        storyText.fontSizeMin = 12;
        storyText.fontSizeMax = 80;
        storyText.alignment = TextAlignmentOptions.Center; // Center the text

        // If needed, the picture remains on screen, no need to hide or show it again
    }
}
