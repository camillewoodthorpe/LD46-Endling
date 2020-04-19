using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] private string m_objectName = "";
    
    public static bool CanClickOnObjects = false;
    public UnityEvent Action;
    public event EventHandler Clicked;

    private int m_playCount = 0;

    [Serializable]
    public class Audio
    {
        public AudioClip Clip;
        public string Caption;
    }
    
    public List<Audio> AudioClips = new List<Audio>();
    [SerializeField] private AudioSource m_audioSource;

    public string TooltipText = "";
    

    public void OnMouseEnter()
    {
        if (!CanClickOnObjects)
            return;
        
        Tooltip.Instance.ShowTooltip(m_objectName, TooltipText);
    }

    public void OnMouseExit()
    {
        if (!CanClickOnObjects)
            return;
        
        Tooltip.Instance.HideTooltip();
    }
    
    public void OnMouseDown()
    {
        if (!CanClickOnObjects)
            return;
        
        if (Clicked != null)
            Clicked.Invoke(this, null);
        
        Reset();

        if (AudioClips.Count > 0)
            StartCoroutine(PlayClip());

        Action.Invoke();
    }

    private void Reset()
    {
        StopAllCoroutines();
        
        CaptionsManager.Instance.ToggleCaptions(false);
        if (m_audioSource == null)
            CaptionsManager.Instance.StopAudio();
        else
            m_audioSource.Stop();
    }

    private IEnumerator PlayClip()
    {
        Audio currentAudio = AudioClips[m_playCount];
        if (m_audioSource != null)
        {
            m_audioSource.clip = currentAudio.Clip;
            m_audioSource.Play();
        }
        else
        {
            CaptionsManager.Instance.PlayAudio(currentAudio.Clip);
        }

        if (!string.IsNullOrEmpty(currentAudio.Caption))
        {
            CaptionsManager.Instance.ChangeCaptions(currentAudio.Caption);
        }
        
        m_playCount++;
        if (m_playCount >= AudioClips.Count)
            m_playCount = 0;

        yield return new WaitForSeconds(AudioClips[m_playCount].Clip.length);
        CaptionsManager.Instance.ToggleCaptions(false);
    }
}