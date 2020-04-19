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
        // Update location
    }

    public void ShowTooltip(string title, string text)
    {
        gameObject.SetActive(true);
        m_text.text = $"[{title}]\n{text}";
        m_isShowing = true;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        m_text.text = "";
        m_isShowing = false;
    }
}