using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraRenderTest : MonoBehaviour
{
    public bool AutoControl = true;
    public float Offset1,Offset2;
    public Material Mat;
    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!Mat) return;
        if (Application.isPlaying)
        {
            if (AutoControl)
            {
                float time = Time.time;
                Offset1 = Mathf.Sin(time * 20) / 20;
                Offset2 = Mathf.Sin(time * 10) / 100;
            }
            
            Mat.SetFloat("_OffsetX1", Offset1);
            Mat.SetFloat("_OffsetX2", Offset2);
        }

        Graphics.Blit(source, destination, Mat);
    }
}
