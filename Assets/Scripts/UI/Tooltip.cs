using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance;

    [SerializeField] private Text m_text;

    private bool m_isShowing = false;


    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!m_isShowing)
            return;
        
        //CanvasScaler scaler = GetComponentInParent<CanvasScaler>();
        //m_text.rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x * scaler.referenceResolution.x / Screen.width, Input.mousePosition.y * scaler.referenceResolution.y / Screen.height);
        
        //Debug.Log(Input.mousePosition.x + " " + Input.mousePosition.y);
        //m_text.rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        
        Canvas canvas = GetComponentInParent<Canvas>();
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, new Vector2(Input.mousePosition.x, Input.mousePosition.y + 50), canvas.worldCamera, out pos);
        transform.position = canvas.transform.TransformPoint(pos);
    }

    public void ShowTooltip(string title, string text)
    {
        gameObject.SetActive(true);
        m_text.text = $"[{title.ToUpper()}]\n{text}";
        m_isShowing = true;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        m_text.text = "";
        m_isShowing = false;
    }
}