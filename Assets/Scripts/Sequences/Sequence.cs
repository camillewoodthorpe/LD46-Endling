using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sequence : MonoBehaviour
{
    public event EventHandler FinishedSequence;
    
    public abstract void PlaySequence();

    public virtual void FinishSequence()
    {
        if (FinishedSequence != null)
            FinishedSequence.Invoke(this, null);
    }
}