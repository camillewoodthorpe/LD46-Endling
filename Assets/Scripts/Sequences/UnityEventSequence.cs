using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventSequence : Sequence
{
    [SerializeField] private float m_delay = 0.0f;

    public UnityEvent Action;
    
    
    public override void PlaySequence()
    {
        Action.Invoke();
        FinishSequence();
    }
}