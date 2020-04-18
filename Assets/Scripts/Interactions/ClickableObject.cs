using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ClickableObject : MonoBehaviour
{
    public static bool CanClickOnObjects = true;
    
    public event EventHandler Clicked;

    [Serializable]
    public class Audio
    {
        public AudioClip Clip;
        public string Caption;
    }
    
    public List<Audio> AudioClips = new List<Audio>();
    [SerializeField] private AudioSource m_audioSource;
    
    public void OnMouseDown()
    {
        if (!CanClickOnObjects)
            return;
        
        if (Clicked != null)
            Clicked.Invoke(this, null);
        
        Reset();
        StartCoroutine(PlayRandomClip());
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

    private IEnumerator PlayRandomClip()
    {
        Random rand = new Random();

        int randomIndex = rand.Next(0, AudioClips.Count);

        Audio currentAudio = AudioClips[randomIndex];

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

        yield return new WaitForSeconds(AudioClips[randomIndex].Clip.length);
        CaptionsManager.Instance.ToggleCaptions(false);
    }
}