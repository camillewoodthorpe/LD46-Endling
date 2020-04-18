using UnityEngine;
 
[RequireComponent(typeof(MeshRenderer))]
public class WalkCycle : MonoBehaviour {
    public float fps = 8f;
    public Mesh[] meshes;
 
    MeshFilter meshFilter;

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

    private bool m_isPlaying = false;
 
    void Awake()
    {
        meshFilter = GetComponent<MeshFilter> ();
    }

    public void Stop()
    {
        meshFilter.sharedMesh = meshes [0];
    }
 
    void Update()
    {
        if (!m_isPlaying)
            return;
        
        int index = ((int)(Time.time * fps)) % meshes.Length;
        meshFilter.sharedMesh = meshes [index];
    }
}