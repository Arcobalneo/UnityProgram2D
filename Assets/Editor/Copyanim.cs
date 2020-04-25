using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Copyanim : EditorWindow {
    private bool iscure, isevent, isloop;
    public GUISkin guiskin;
    public AnimationClip[] sourceAnimation, targetAnimation;
    public int animationNum=0;
    [MenuItem("Zero/Copyanim")]
    public static void OpenWindow()
    {
        Copyanim window = GetWindow(typeof(Copyanim)) as Copyanim;
        window.minSize = new Vector2(300, 300);
        window.maxSize = new Vector2(600, 600);
        window.Show();
    }
    public void OnEnable()
    {
        sourceAnimation = new AnimationClip[25];
        targetAnimation = new AnimationClip[25];
    }
    public void OnGUI()
    {
        GUILayout.Label("请选择要拷贝的动画属性(需要保持动画长度相同):");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("动画事件:");
        isevent=EditorGUILayout.Toggle(isevent);
        GUILayout.Label("动画曲线:");
        iscure = EditorGUILayout.Toggle(iscure);
        GUILayout.Label("动画循环:");
        isloop = EditorGUILayout.Toggle(isloop);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("需要拷贝的动画个数:");
        animationNum = EditorGUILayout.IntField(animationNum);
        EditorGUILayout.EndHorizontal();
   
        for(int i=0;i<animationNum;i++)
        {
            EditorGUILayout.BeginHorizontal();
            sourceAnimation[i] = EditorGUILayout.ObjectField(sourceAnimation[i], typeof(AnimationClip), true) as AnimationClip;
            GUILayout.Label("  --->");
            targetAnimation[i] = EditorGUILayout.ObjectField(targetAnimation[i], typeof(AnimationClip), true) as AnimationClip;
            EditorGUILayout.EndHorizontal();
        }
        if(GUILayout.Button("执行"))
        {
            for(int i=0;i<animationNum;i++)
            {
                Add.DoAddEvent(sourceAnimation[i], targetAnimation[i],iscure,isevent,isloop);
            }
        }
    }
}
