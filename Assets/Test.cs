using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        print(SingleInstanceTest.Instance.gameObject.name);
    }
}