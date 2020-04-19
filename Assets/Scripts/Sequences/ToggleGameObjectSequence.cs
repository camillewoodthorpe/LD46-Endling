using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObjectSequence : Sequence
{
    [SerializeField] private float m_delay = 0.0f;

    [SerializeField] private GameObject m_object;
    [SerializeField] private bool m_state;
    
    
    public override void PlaySequence()
    {
        StartCoroutine(DelayedPlay());
    }

    private IEnumerator DelayedPlay()
    {
        yield return new WaitForSeconds(m_delay);
        
        m_object.SetActive(m_state);
        FinishSequence();
    }
}