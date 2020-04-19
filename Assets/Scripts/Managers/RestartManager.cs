using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartManager : MonoBehaviour
{
    public static bool HasRestarted = false;
    public static TimeSpan RemainingTime = new TimeSpan(0, 0, 5, 0);
}