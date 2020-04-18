using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public event EventHandler Clicked;
    
    public void OnMouseDown()
    {
        if (Clicked != null)
            Clicked.Invoke(this, null);
    }
}