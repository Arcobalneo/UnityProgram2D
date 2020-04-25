using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SingleInstanceTest : MonoBehaviour
{
    public static SingleInstanceTest Instance;

    private void Awake()
    {
        Instance = this;
    }
}