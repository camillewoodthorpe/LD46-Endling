﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Super hard-coded code to quickly get the level working... don't learn from this :)
/// </summary>
public class LabManager : MonoBehaviour
{
    private bool m_wearingGasMask = false;
    private bool m_guardInRange = true;
    
    [SerializeField] private AudioSource m_guardAudioSource;
    [SerializeField] private AudioClip m_caughtClip;

    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_fiveMinCountdownAudio;
    [SerializeField] private AudioClip m_tenSecCountdownAudio;
    [SerializeField] private Text m_countdownText;

    [SerializeField] private GameObject m_gasMask;
    [SerializeField] private GameObject m_gasMaskReturn;
    [SerializeField] private Walk m_playerWalk;
    [SerializeField] private GameObject m_playerNormal;
    [SerializeField] private GameObject m_playerMask;
    
    private float m_durationMinutes = 1;
    private DateTime m_projectedTime;

    private bool m_isCountingDown = false;
    private bool m_isCountingDownTenSecs = false;
    

    void Update()
    {
        if (!m_isCountingDown)
            return;
        
        TimeSpan timeRemaining = m_projectedTime - DateTime.UtcNow;
        m_countdownText.text = "S C H E D U L E D\n" + timeRemaining.ToString("mm':'ss");

        if (!m_isCountingDownTenSecs && timeRemaining <= TimeSpan.FromSeconds(m_tenSecCountdownAudio.length + 0.8f))
        {
            StartTenSecCountdown();
        }
        else if (DateTime.UtcNow >= m_projectedTime)
        {
            TooLate();
        }
    }
    
    public void StartFiveMinCountdown()
    {
        m_projectedTime = DateTime.UtcNow.AddMinutes(m_durationMinutes);
        m_isCountingDown = true;

        m_audioSource.clip = m_fiveMinCountdownAudio;
        m_audioSource.Play();
    }
    
    public void StartTenSecCountdown()
    {
        m_isCountingDownTenSecs = true;
        
        m_audioSource.clip = m_tenSecCountdownAudio;
        m_audioSource.Play();
        
        // Guard walk
    }

    public void ToggleWearingGasMask()
    {
        m_wearingGasMask = !m_wearingGasMask;
        
        m_gasMask.SetActive(!m_wearingGasMask);
        m_gasMaskReturn.SetActive(m_wearingGasMask);
        
        // Toggle player avatar...
        m_playerWalk.m_walkCycle = m_wearingGasMask ? m_playerMask.GetComponent<AnimationCycle>() : m_playerNormal.GetComponent<AnimationCycle>();
        m_playerMask.SetActive(m_wearingGasMask);
        m_playerNormal.SetActive(!m_wearingGasMask);
    }

    public void ToggleGuardInRange()
    {
        m_guardInRange = !m_guardInRange;
    }

    public void OpenedGasValve()
    {
        if (m_guardInRange)
        {
            Caught();
        }
        else if (!m_guardInRange && !m_wearingGasMask)
        {
            Oops();
        }
        else
        {
            Success();
        }
    }

    public void Success()
    {
        Debug.Log("Success!");
        
        ToggleClickability(false);
        
        // Gassed out others animation

        LoadFinishScene();
    }

    public void Caught()
    {
        Debug.Log("Caught!");
        
        ToggleClickability(false);
        
        m_guardAudioSource.clip = m_caughtClip;
        m_guardAudioSource.Play();

        CaptionsManager.Instance.ChangeCaptions("Stop! What are you doing back there?");
        
        // Guard caught animation
        
        StartCoroutine(Restart("Scenes/04 - Fail - Caught", 3.0f));
    }

    public void Oops()
    {
        Debug.Log("Oops!");
        m_isCountingDown = false;
        ToggleClickability(false);
        
        // Gassed out animation
        
        StartCoroutine(Restart("Scenes/04 - Fail - Gassed Out", 0));
    }

    public void TooLate()
    {
        m_isCountingDown = false;
        Debug.Log("TooLate!");

        ToggleClickability(false);
        
        // Kill animation

        StartCoroutine(Restart("Scenes/04 - Fail - Too Late", 0));
    }

    public IEnumerator Restart(string sceneToLoad, float delay)
    {
        m_isCountingDown = false;
        Tooltip.Instance.HideTooltip();
        
        yield return new WaitForSeconds(delay);

        // Save timer
        
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadFinishScene()
    {
        SceneManager.LoadScene("Scenes/05 - Credits");
    }

    private void ToggleClickability(bool status)
    {
        Walk.CanWalk = status;
        ClickableObject.CanClickOnObjects = status;
    }
}