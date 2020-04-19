using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    [SerializeField] private float m_duration = 5.0f;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(m_duration);
        SceneManager.LoadScene("Scenes/01 - The Lab");
    }
}