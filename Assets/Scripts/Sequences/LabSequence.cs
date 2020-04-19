using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Super hard-coded code to quickly get the level working... don't learn from this :)
/// </summary>
public class LabSequence : Sequence
{
    private bool m_wearingGasMask = false;
    private bool m_guardInRange = true;
    
    
    public override void PlaySequence()
    {
        //
    }

    public void ToggleWearingGasMask()
    {
        m_wearingGasMask = !m_wearingGasMask;
        
        // Toggle player avatar...
    }

    public void ToggleGuardInRange()
    {
        m_guardInRange = !m_guardInRange;
    }
    
    public void StartCountdown()
    {
        // Play audio
        // Guard walk
    }

    public void OpenedGasValve()
    {
        if (m_guardInRange)
        {
            Caught();
        }
        else if (!m_guardInRange && !m_wearingGasMask)
        {
            Oops();
        }
        else
        {
            Success();
        }
    }

    public void Success()
    {
        ToggleClickability(false);
        
        // Gassed out others animation

        LoadFinishScene();
    }

    public void Caught()
    {
        ToggleClickability(false);
        
        // Guard caught animation
        
        Restart();
    }

    public void Oops()
    {
        ToggleClickability(false);
        
        // Gassed out animation
        
        Restart();
    }

    public void TooLate()
    {
        ToggleClickability(false);
        
        // Kill animation
        
        Restart();
    }

    public void Restart()
    {
        // Save timer
        // Restart screen
    }

    public void LoadFinishScene()
    {
        SceneManager.LoadScene("Scenes/05 - Credits");
    }

    private void ToggleClickability(bool status)
    {
        Walk.CanWalk = status;
        ClickableObject.CanClickOnObjects = status;
    }
}