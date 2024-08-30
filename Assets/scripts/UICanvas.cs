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
       float screenWidthRatio = Screen.width / referenceResolution.x;
        float screenHeightRatio = Screen.height / referenceResolution.y;

   textElement.enableAutoSizing = true;

        // 设置字体大小范围
        textElement.fontSizeMin = 12;
        textElement.fontSizeMax = 80;

        // 动态调整文本的位置和大小
        if (textElement != null)
        {
            RectTransform textRect = textElement.GetComponent<RectTransform>();
            textRect.anchoredPosition = new Vector2(referenceResolution.x * 0.1f * screenWidthRatio, referenceResolution.y * 0.2f * screenHeightRatio);
            textRect.sizeDelta = new Vector2(referenceResolution.x * 0.4f * screenWidthRatio, referenceResolution.y * 0.2f * screenHeightRatio);
       }

        // 动态调整按钮的位置和大小
        if (buttonElement != null)
        {
            RectTransform buttonRect = buttonElement.GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(referenceResolution.x * 0.2f * screenWidthRatio, -referenceResolution.y * 0.2f * screenHeightRatio);
            buttonRect.sizeDelta = new Vector2(referenceResolution.x * 0.2f * screenWidthRatio, referenceResolution.y * 0.1f * screenHeightRatio);
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
