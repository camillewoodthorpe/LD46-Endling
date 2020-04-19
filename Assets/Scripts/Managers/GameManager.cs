using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Sequence> m_sequences = new List<Sequence>();
    private int m_activeIndex = 0;

    public void Start()
    {
        Component[] components = transform.GetComponentsInChildren(typeof(Sequence));
        
        m_sequences = components.Cast<Sequence>().ToList();
        
        StartSequences();
    }

    private void StartSequences()
    {
        StartSequence(0);
    }

    private void StartSequence(int index)
    {
        m_activeIndex = index;
        m_sequences[m_activeIndex].FinishedSequence += OnFinishedSequence;
        m_sequences[m_activeIndex].PlaySequence();
    }
    
    private void OnFinishedSequence(object sender, EventArgs args)
    {
        m_sequences[m_activeIndex].FinishedSequence -= OnFinishedSequence;
        
        if (m_sequences.Count <= m_activeIndex + 1)
        {
            Debug.Log("FINISHED!");
        }
        else
        {
            StartSequence(m_activeIndex + 1);
        }
    }
}