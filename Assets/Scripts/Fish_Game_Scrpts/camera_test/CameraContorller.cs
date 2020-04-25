using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContorller : MonoBehaviour
{
    CameraFilterPack_Blur_Bloom myCamera1;
    CameraFilterPack_TV_80 myCamera2;
    // Start is called before the first frame update
    void Start()
    {
        myCamera1 = GetComponent<CameraFilterPack_Blur_Bloom>();
        myCamera2 = GetComponent<CameraFilterPack_TV_80>();
        myCamera1.Glow = 0.12f;
        myCamera2.Fade = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
