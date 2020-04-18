using UnityEngine;
 
[RequireComponent(typeof(MeshRenderer))]
public class AnimationCycle : MonoBehaviour {
    public float fps = 8f;
    public Mesh[] meshes;
 
    private MeshFilter m_meshFilter;

    public bool IsPlaying
    {
        set
        {
            if (value == true)
                m_isPlaying = true;
            else
            {
                m_isPlaying = false;
                Stop();
            }
        }
    }

    public bool m_isPlaying = false;
 
    void Awake()
    {
        m_meshFilter = GetComponent<MeshFilter> ();
    }

    public void Stop()
    {
        m_meshFilter.sharedMesh = meshes [0];
    }
 
    void Update()
    {
        if (!m_isPlaying)
            return;
        
        int index = ((int)(Time.time * fps)) % meshes.Length;
        m_meshFilter.sharedMesh = meshes [index];
    }
}