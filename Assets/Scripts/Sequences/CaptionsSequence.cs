using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptionsSequence : Sequence
{
    [Serializable]
    public class Caption
    {
        public string CaptionText;
        public AudioClip Audio;
        public float DurationSeconds;
    }
    [SerializeField] private List<Caption> m_captions = new List<Caption>();

    private DateTime m_captionStartTime = DateTime.UtcNow;

    private bool m_isActive = false;

    private int m_activeCaptionIndex;
    
    
    public override void PlaySequence()
    {
        CaptionsManager.Instance.ToggleCaptions(true);

        m_isActive = true;
        m_activeCaptionIndex = 0;
        PlayCaption(m_activeCaptionIndex);
    }

    public void Update()
    {
        if (!m_isActive)
            return;

        if (DateTime.UtcNow >= m_captionStartTime.AddSeconds(m_captions[m_activeCaptionIndex].DurationSeconds))
        {
            if (m_captions.Count <= m_activeCaptionIndex + 1)
            {
                OnFinished();
            }
            else
            {
                PlayCaption(m_activeCaptionIndex + 1);
            }
        }
    }

    private void PlayCaption(int index)
    {
        m_activeCaptionIndex = index;
        m_captionStartTime = DateTime.UtcNow;

        CaptionsManager.Instance.ChangeCaptions(m_captions[m_activeCaptionIndex].CaptionText);
        if (m_captions[m_activeCaptionIndex].Audio != null)
            CaptionsManager.Instance.PlayAudio(m_captions[m_activeCaptionIndex].Audio);
    }

    private void OnFinished()
    {
        m_isActive = false;
        m_activeCaptionIndex = 0;
        
        CaptionsManager.Instance.ToggleCaptions(false);
        FinishSequence();
    }
}