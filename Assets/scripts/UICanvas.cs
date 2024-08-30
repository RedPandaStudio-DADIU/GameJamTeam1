using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UICanvas : MonoBehaviour
{

    public Canvas canvas;
    public TextMeshProUGUI textElement;
    public Button buttonElement; // 新增的按钮元素
    public RectTransform imageElement; // 如果没有图片，可以将这个保留为空或移除

    public Vector2 referenceResolution = new Vector2(1920, 1080);
    public float widthHeightRatioThreshold = 1.5f; // 例如16:9的宽高比

    // Start is called before the first frame update
    void Start()
    {
        AdjustCanvasScaler();
        AdjustUIElements();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AdjustCanvasScaler()
    {
        CanvasScaler canvasScaler = canvas.GetComponent<CanvasScaler>();

        if (canvasScaler != null)
        {
            // 设置Canvas的缩放模式和参考分辨率
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = referenceResolution;
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;

            // 设置Match的值以匹配宽度或高度
            float aspectRatio = (float)Screen.width / Screen.height;
            canvasScaler.matchWidthOrHeight = (aspectRatio >= widthHeightRatioThreshold) ? 0 : 1;
        }
    }


    void AdjustUIElements()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 动态调整文本的位置和大小
        if (textElement != null)
        {
            RectTransform textRect = textElement.GetComponent<RectTransform>();
            textRect.anchoredPosition = new Vector2(screenWidth * 0.1f, screenHeight * 0.2f);
            textRect.sizeDelta = new Vector2(screenWidth * 0.4f, screenHeight * 0.2f);
        }

        // 动态调整按钮的位置和大小
        if (buttonElement != null)
        {
            RectTransform buttonRect = buttonElement.GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(screenWidth * 0.2f, -screenHeight * 0.2f);
            buttonRect.sizeDelta = new Vector2(screenWidth * 0.2f, screenHeight * 0.1f);
        }

        // 动态调整图片的位置和大小（如果存在）
        if (imageElement != null)
        {
            imageElement.anchoredPosition = new Vector2(screenWidth * -0.2f, screenHeight * 0.1f);
            imageElement.sizeDelta = new Vector2(screenWidth * 0.2f, screenHeight * 0.2f);
        }

        AdjustUIForAspectRatio(screenWidth, screenHeight);
    }


    void AdjustUIForAspectRatio(float screenWidth, float screenHeight)
    {
        float aspectRatio = screenWidth / screenHeight;

        // 基于屏幕比例调整某个 UI 元素的位置或大小
        if (aspectRatio > widthHeightRatioThreshold)
        {
            // 对于宽屏（例如16:9），可能需要更改UI布局
            if (textElement != null)
            {
                textElement.fontSize = 80;
            }
            if (buttonElement != null)
            {
                // 调整按钮的字体大小
                buttonElement.GetComponentInChildren<TextMeshProUGUI>().fontSize = 60;
            }
        }
        else
        {
            // 对于窄屏（例如4:3），可能需要更改其他布局
            if (textElement != null)
            {
                textElement.fontSize = 80;
            }
            if (buttonElement != null)
            {
                // 调整按钮的字体大小
                buttonElement.GetComponentInChildren<TextMeshProUGUI>().fontSize = 60;
            }
        }
    }
}
