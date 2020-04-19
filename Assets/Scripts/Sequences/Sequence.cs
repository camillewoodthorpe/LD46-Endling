using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sequence : MonoBehaviour
{
    public bool SkipOnRestart = false;
    public bool OnlyOnRestart = false;
    
    public event EventHandler FinishedSequence;

    public virtual void PrePlaySequence()
    {
        if (SkipOnRestart && RestartManager.HasRestarted)
        {
            FinishSequence();
        }
        else if (SkipOnRestart && !RestartManager.HasRestarted)
        {
            PlaySequence();
        }
        else if (OnlyOnRestart && RestartManager.HasRestarted)
        {
            PlaySequence();
        }
        else if (OnlyOnRestart && !RestartManager.HasRestarted)
        {
            FinishSequence();
        }
        else
        {
            PlaySequence();
        }
    }
    
    public abstract void PlaySequence();

    public virtual void FinishSequence()
    {
        if (FinishedSequence != null)
            FinishedSequence.Invoke(this, null);
    }
}