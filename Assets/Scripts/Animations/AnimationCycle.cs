using System;
using UnityEngine;
 
[RequireComponent(typeof(MeshRenderer))]
public class AnimationCycle : MonoBehaviour {
    public float fps = 8f;
    public Mesh[] meshes;
 
    [SerializeField] private MeshFilter m_meshFilter;

    [SerializeField] private float m_multiplier = 1.0f;
    
    private bool m_isPlaying = false;

    [SerializeField] private bool m_playOnStart = false;


    void Start()
    {
        if (m_playOnStart)
        {
            StartCycle();
        }
    }

    public void StartCycle()
    {
        m_isPlaying = true;
    }
    
    public void StopCycle()
    {
        m_isPlaying = false;
        m_meshFilter.sharedMesh = meshes [0];
    }
 
    void Update()
    {
        if (!m_isPlaying)
            return;
        
        int index = ((int)(Time.time * fps * m_multiplier)) % meshes.Length;
        m_meshFilter.sharedMesh = meshes [index];
    }
}