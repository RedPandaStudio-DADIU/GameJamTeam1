using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UICanvas : MonoBehaviour
{

    public Canvas canvas;
    public TextMeshProUGUI textElement;
    public Button[] buttonElements; // 新增的按钮元素
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
       float screenWidthRatio = Screen.width / referenceResolution.x;
        float screenHeightRatio = Screen.height / referenceResolution.y;

   textElement.enableAutoSizing = true;

        // 设置字体大小范围
        textElement.fontSizeMin = 12;
        textElement.fontSizeMax = 90;

        // 动态调整文本的位置和大小
        if (textElement != null)
        {
            RectTransform textRect = textElement.GetComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0.5f, 0.5f); // 将锚点设置为屏幕中心
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.pivot = new Vector2(0.5f, 0.5f); // 将轴心设置在对象的中心
            textRect.anchoredPosition = new Vector2(0, Screen.height * 0.15f); // 位置调整到中上方
            textRect.sizeDelta = new Vector2(referenceResolution.x * 0.8f * screenWidthRatio, referenceResolution.y * 0.2f * screenHeightRatio); // 扩大文本区域
 }

        // 动态调整按钮的位置和大小
        if (buttonElements != null)
        {
            for (int i = 0; i < buttonElements.Length; i++)
            {
                RectTransform buttonRect = buttonElements[i].GetComponent<RectTransform>();
                buttonRect.anchorMin = new Vector2(0.5f, 0.5f); // 将锚点设置为屏幕中心
                buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
                buttonRect.pivot = new Vector2(0.5f, 0.5f); // 将轴心设置在对象的中心
                buttonRect.anchoredPosition = new Vector2(0, Screen.height * 0.05f - i * (Screen.height * 0.12f)); // 根据索引向下排列
                buttonRect.sizeDelta = new Vector2(referenceResolution.x * 0.3f * screenWidthRatio, referenceResolution.y * 0.1f * screenHeightRatio);

                // 动态调整按钮文字的字体大小
                TextMeshProUGUI buttonText = buttonElements[i].GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.enableAutoSizing = true;
                    buttonText.fontSizeMin = 12;
                    buttonText.fontSizeMax = 60;
                }
             }
        }

        // 动态调整图片的位置和大小（如果存在）
        if (imageElement != null)
        {
            imageElement.anchoredPosition = new Vector2(referenceResolution.x * -0.2f * screenWidthRatio, referenceResolution.y * 0.1f * screenHeightRatio);
              imageElement.sizeDelta = new Vector2(referenceResolution.x * 0.2f * screenWidthRatio, referenceResolution.y * 0.2f * screenHeightRatio);
       }

        //AdjustUIForAspectRatio(screenWidthRatio, screenHeightRatio);
    }


    
}
