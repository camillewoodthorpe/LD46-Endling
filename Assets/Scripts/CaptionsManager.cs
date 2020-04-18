using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CaptionsManager : MonoBehaviour
{
    [SerializeField] private GameObject m_captionsCanvas;
    [SerializeField] private Text m_captions;
    [SerializeField] private AudioSource m_audioSource;


    public static CaptionsManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void ToggleCaptions(bool status)
    {
        m_captionsCanvas.SetActive(status);
        ChangeCaptions("");
    }
    
    public void ChangeCaptions(string text)
    {
        if(m_captionsCanvas.activeSelf == false && !string.IsNullOrEmpty((text)))
            m_captionsCanvas.SetActive(true);
        m_captions.text = text;
    }

    public void PlayAudio(AudioClip audio)
    {
        m_audioSource.clip = audio;
        m_audioSource.Play();
    }
}