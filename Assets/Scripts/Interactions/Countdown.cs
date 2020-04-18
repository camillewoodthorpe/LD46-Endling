using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private AudioClip m_10secCountdown;
    [SerializeField] private Text m_text;
    
    private float m_durationMinutes = 5;
    private DateTime m_projectedTime;
    
    
    void Start()
    {
        m_projectedTime = DateTime.UtcNow.AddMinutes(m_durationMinutes);
    }

    void Update()
    {
        TimeSpan timeRemaining = m_projectedTime - DateTime.UtcNow;
        m_text.text = "S C H E D U L E D\n" + timeRemaining.ToString("mm':'ss");
    }
}