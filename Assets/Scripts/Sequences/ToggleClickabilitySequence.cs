using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToggleClickabilitySequence : Sequence
{
    [SerializeField] private bool m_state;
    
    
    public override void PlaySequence()
    {
        Walk.CanWalk = m_state;
        ClickableObject.CanClickOnObjects = m_state;
        
        FinishSequence();
    }
}