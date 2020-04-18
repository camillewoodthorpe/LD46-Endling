using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoCameraFollow : MonoBehaviour
{
    [SerializeField] private float m_screenPercent = 0.3f;
    [SerializeField] private float m_followSeconds = 4.0f;

    //private bool m_isFollowing = false;

    
    public void OnWalk(Vector3 targetPosition)
    {
        //if (m_isFollowing)
        //    return;
        
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetPosition);
        float screenPercentage = screenPos.x / Screen.width;
        
        if (screenPercentage < m_screenPercent || screenPercentage > (1.0f - m_screenPercent))
        {
            iTween.MoveTo(gameObject,
                iTween.Hash("x", targetPosition.x + 4, "time", m_followSeconds, "oncomplete", "OnFinishZoom", "easetype", iTween.EaseType.easeInOutSine));
        }
    }

    private void OnFinishZoom()
    {
        //m_isFollowing = false;
    }
}