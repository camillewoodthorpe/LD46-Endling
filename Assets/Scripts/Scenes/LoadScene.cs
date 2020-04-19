using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private bool m_autoLoad = true;
    [SerializeField] private float m_duration = 5.0f;
    [SerializeField] private string m_sceneName;
    
    private void Start()
    {
        if (m_autoLoad)
        {
            StartCoroutine(Load());
        }
    }

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(m_duration);
        
        SceneManager.LoadScene(m_sceneName);
    }

    public void ForceLoad()
    {
        SceneManager.LoadScene(m_sceneName);
    }
}