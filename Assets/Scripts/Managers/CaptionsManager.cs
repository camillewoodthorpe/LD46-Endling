using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CaptionsManager : MonoBehaviour
{
    [SerializeField] private Text m_captions;
    [SerializeField] private AudioSource m_audioSource;


    public static CaptionsManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void ToggleCaptions(bool status)
    {
        ChangeCaptions("");
    }
    
    public void ChangeCaptions(string text)
    {
        m_captions.text = text;
    }

    public void PlayAudio(AudioClip audio)
    {
        m_audioSource.clip = audio;
        m_audioSource.Play();
    }

    public void StopAudio()
    {
        m_audioSource.Stop();
    }
}