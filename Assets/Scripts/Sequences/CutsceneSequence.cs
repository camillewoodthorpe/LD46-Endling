using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSequence : Sequence
{
    [Serializable]
    public class CutsceneAnimation
    {
        public Animator Animator;
        public string AnimationName;
    }
    
    [Serializable]
    public class Cutscene
    {
        public string CaptionText;
        public AudioClip Audio;
        public float DurationSeconds;
        public List<CutsceneAnimation> Animations = new List<CutsceneAnimation>();
    }
    [SerializeField] private List<Cutscene> m_cutscenes = new List<Cutscene>();

    private DateTime m_cutsceneStartTime = DateTime.UtcNow;

    private bool m_isActive = false;

    private int m_activeCutsceneIndex;
    
    
    public override void PlaySequence()
    {
        //CaptionsManager.Instance.ToggleCaptions(true);

        m_isActive = true;
        m_activeCutsceneIndex = 0;
        PlayCutscene(m_activeCutsceneIndex);
    }

    public void Update()
    {
        if (!m_isActive)
            return;

        if (DateTime.UtcNow >= m_cutsceneStartTime.AddSeconds(m_cutscenes[m_activeCutsceneIndex].DurationSeconds))
        {
            foreach (CutsceneAnimation animation in m_cutscenes[m_activeCutsceneIndex].Animations)
            {
                animation.Animator.SetBool(animation.AnimationName, false);
            }
            
            if (m_cutscenes.Count <= m_activeCutsceneIndex + 1)
            {
                OnFinished();
            }
            else
            {
                PlayCutscene(m_activeCutsceneIndex + 1);
            }
        }
    }

    private void PlayCutscene(int index)
    {
        m_activeCutsceneIndex = index;
        m_cutsceneStartTime = DateTime.UtcNow;

        CaptionsManager.Instance.ChangeCaptions(m_cutscenes[m_activeCutsceneIndex].CaptionText);
        if (m_cutscenes[m_activeCutsceneIndex].Audio != null)
            CaptionsManager.Instance.PlayAudio(m_cutscenes[m_activeCutsceneIndex].Audio);

        foreach (CutsceneAnimation animation in m_cutscenes[m_activeCutsceneIndex].Animations)
        {
            animation.Animator.SetBool(animation.AnimationName, true);
        }
    }

    private void OnFinished()
    {
        m_isActive = false;
        m_activeCutsceneIndex = 0;
        
        CaptionsManager.Instance.ToggleCaptions(false);
        FinishSequence();
    }
}