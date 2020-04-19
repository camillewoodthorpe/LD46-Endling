using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] private string m_objectName = "";
    
    public static bool CanClickOnObjects = false;
    
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
        Tooltip.Instance.ShowTooltip(m_objectName, TooltipText);
    }

    public void OnMouseExit()
    {
        Tooltip.Instance.HideTooltip();
    }
    
    public void OnMouseDown()
    {
        if (!CanClickOnObjects)
            return;
        
        if (Clicked != null)
            Clicked.Invoke(this, null);
        
        Reset();
        StartCoroutine(PlayClip());
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