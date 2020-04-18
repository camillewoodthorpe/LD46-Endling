using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    [SerializeField] private OrthoCameraFollow m_cameraFollow;
    [SerializeField] private AudioSource m_audioSource;
    
    [SerializeField] private GameObject m_walkCirclePrefab;
    [SerializeField] private float m_walkSpeed = 2.0f;
    [SerializeField] private AnimationCycle m_walkCycle;

    private Vector3 originalScale = new Vector3();

    
    private void Start()
    {
        originalScale = transform.localScale;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.gameObject.CompareTag("Ground"))
                {
                    WalkToTarget(hit.point);
                }
            }
        }
    }

    private void WalkToTarget(Vector3 target)
    {
        Vector3 levelTarget = new Vector3(target.x, transform.position.y, target.z);
        float duration = Vector3.Distance(levelTarget, transform.position) / m_walkSpeed;

        // Flip sprite to face direction
        if (levelTarget.x > transform.position.x)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
        
        // Animate
        m_audioSource.Play();
        m_walkCycle.StartCycle();
        iTween.MoveTo(gameObject,
            iTween.Hash("x", levelTarget.x, "y", levelTarget.y, "z", levelTarget.z, "time", duration, "oncomplete", "OnFinish", "easetype", iTween.EaseType.linear));
        
        // Instantiate walk circle
        Vector3 walkCircleTarget = new Vector3(levelTarget.x, 0.05f, levelTarget.z);
        GameObject.Instantiate(m_walkCirclePrefab, walkCircleTarget, Quaternion.identity);
        
        // Move camera
        m_cameraFollow.OnWalk(walkCircleTarget);
    }

    private void OnFinish()
    {
        m_audioSource.Stop();
        m_walkCycle.StopCycle();
    }
}