using System;
using System.Collections;
using System.Collections.Generic;
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
    
    private float m_durationMinutes = 5;
    private DateTime m_projectedTime;

    private bool m_isCountingDown = false;
    private bool m_isCountingDownTenSecs = false;
    

    void Update()
    {
        if (!m_isCountingDown)
            return;
        
        TimeSpan timeRemaining = m_projectedTime - DateTime.UtcNow;
        m_countdownText.text = "S C H E D U L E D\n" + timeRemaining.ToString("mm':'ss");

        if (!m_isCountingDownTenSecs && timeRemaining <= TimeSpan.FromSeconds(m_tenSecCountdownAudio.length))
        {
            StartTenSecCountdown();
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
        
        // Toggle player avatar...
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
        
        StartCoroutine(Restart("Scenes/04 - Fail - Caught"));
    }

    public void Oops()
    {
        Debug.Log("Oops!");

        ToggleClickability(false);
        
        // Gassed out animation
        
        StartCoroutine(Restart("Scenes/04 - Fail - Gassed Out"));
    }

    public void TooLate()
    {
        Debug.Log("TooLate!");

        ToggleClickability(false);
        
        // Kill animation

        StartCoroutine(Restart("Scenes/04 - Fail - Too Late"));
    }

    public IEnumerator Restart(string sceneToLoad)
    {
        yield return new WaitForSeconds(3.0f);

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