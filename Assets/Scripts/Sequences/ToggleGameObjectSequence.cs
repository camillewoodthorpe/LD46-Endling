using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObjectSequence : Sequence
{
    [SerializeField] private GameObject m_object;
    [SerializeField] private bool m_state;
    
    
    public override void PlaySequence()
    {
        m_object.SetActive(m_state);
        FinishSequence();
    }
}
