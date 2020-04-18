using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderClickSequence : Sequence
{
    [SerializeField] private ClickableObject m_clickableObject;
    
    
    public override void PlaySequence()
    {
        m_clickableObject.Clicked += OnFinished;
    }

    private void OnFinished(object sender, EventArgs args)
    {
        m_clickableObject.Clicked -= OnFinished;
        FinishSequence();
    }
}
