using UnityEngine;
using UnityEditor;
using System.Collections;

class AnimationClipInfoProperties
{
    SerializedProperty m_Property;
    private SerializedProperty Get(string property) { return m_Property.FindPropertyRelative(property); }
    public AnimationClipInfoProperties(SerializedProperty prop) { m_Property = prop; }
    public string name { get { return Get("name").stringValue; } set { Get("name").stringValue = value; } }
    public float firstFrame { get { return Get("firstFrame").floatValue; } set { Get("firstFrame").floatValue = value; } }
    public float lastFrame { get { return Get("lastFrame").floatValue; } set { Get("lastFrame").floatValue = value; } }
    public void SetCurve(AnimationCurve scurve, string namee)
    {
        SerializedProperty curve = Get("curves");
        if(curve!=null)
        {
            curve.ClearArray();
            curve.InsertArrayElementAtIndex(curve.arraySize);
            curve.GetArrayElementAtIndex(0).FindPropertyRelative("name").stringValue = namee;
            SerializedProperty icurve = curve.GetArrayElementAtIndex(0).FindPropertyRelative("curve");
            icurve.animationCurveValue = scurve;
        }
    }
    public void SetEvents(AnimationEvent[] newEvents)
    {
        SerializedProperty events = Get("events");
        if (events != null && events.isArray)
        {
           // Debug.Log(events.GetArrayElementAtIndex(0).FindPropertyRelative("functionName").stringValue);
            events.ClearArray();

            foreach (AnimationEvent evt in newEvents)
            {
            
                events.InsertArrayElementAtIndex(events.arraySize);
               // Debug.Log(events.arraySize);
                SetEvent(events.arraySize - 1, evt);
            }
        }
    }
    public void SetEvent(int index, AnimationEvent animationEvent)
    {
        SerializedProperty events = Get("events");

        if (events != null && events.isArray)
        {
            if (index < events.arraySize)
            {
                events.GetArrayElementAtIndex(index).FindPropertyRelative("floatParameter").floatValue = animationEvent.floatParameter;
                events.GetArrayElementAtIndex(index).FindPropertyRelative("functionName").stringValue = animationEvent.functionName;
                events.GetArrayElementAtIndex(index).FindPropertyRelative("intParameter").intValue = animationEvent.intParameter;
                events.GetArrayElementAtIndex(index).FindPropertyRelative("objectReferenceParameter").objectReferenceValue = animationEvent.objectReferenceParameter;
                events.GetArrayElementAtIndex(index).FindPropertyRelative("data").stringValue = animationEvent.stringParameter;
                events.GetArrayElementAtIndex(index).FindPropertyRelative("time").floatValue = animationEvent.time * 24 / (lastFrame - firstFrame);//1帧24秒 换算区间0-1
            }
        }
    }
    public void SetLoop(AnimationClipSettings src)
    {
        SerializedProperty loop = Get("loopTime");
        SerializedProperty loopblend = Get("loopBlend");
        loop.boolValue = src.loopTime;
        loopblend.boolValue = src.loopBlend;
    }
}
public class Add
{
    public static void DoAddEvent(AnimationClip sourceAnimClip, AnimationClip targetAnimClip,bool iscurve,bool isevent,bool isloop)
    {
        if ((targetAnimClip.hideFlags & HideFlags.NotEditable) != 0)
            DoAddEventImportedClip(sourceAnimClip, targetAnimClip,iscurve,isevent,isloop);
        else
            DoAddEventClip(sourceAnimClip, targetAnimClip);
    }

    static void DoAddEventClip(AnimationClip sourceAnimClip, AnimationClip targetAnimClip)
    {
        if (sourceAnimClip != targetAnimClip)
        {
            AnimationEvent[] sourceAnimEvents = AnimationUtility.GetAnimationEvents(sourceAnimClip);
            AnimationUtility.SetAnimationEvents(targetAnimClip, sourceAnimEvents);

     
        }
    }

    static void DoAddEventImportedClip(AnimationClip sourceAnimClip, AnimationClip targetAnimClip, bool iscurve, bool isevent,bool isloop)
    {
        ModelImporter modelImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(targetAnimClip)) as ModelImporter;
        if (modelImporter == null)
            return;

        SerializedObject serializedObject = new SerializedObject(modelImporter);
        SerializedProperty clipAnimations = serializedObject.FindProperty("m_ClipAnimations");

        if (!clipAnimations.isArray)
            return;

        for (int i = 0; i < clipAnimations.arraySize; i++)
        {
            AnimationClipInfoProperties clipInfoProperties = new AnimationClipInfoProperties(clipAnimations.GetArrayElementAtIndex(i));
            if (clipInfoProperties.name == targetAnimClip.name)
            {
                AnimationEvent[] sourceAnimEvents = AnimationUtility.GetAnimationEvents(sourceAnimClip);
                if(iscurve)
                {
                    EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(sourceAnimClip);
                    AnimationCurve curve = AnimationUtility.GetEditorCurve(sourceAnimClip, curveBindings[0]);
                    clipInfoProperties.SetCurve(curve, curveBindings[0].propertyName);
                }
                if(isevent)
                {
                    clipInfoProperties.SetEvents(sourceAnimEvents);
                }
                if(isloop)
                {
                    AnimationClipSettings animaSetting = AnimationUtility.GetAnimationClipSettings(sourceAnimClip);
                    clipInfoProperties.SetLoop(animaSetting);
                }
                serializedObject.ApplyModifiedProperties();
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(targetAnimClip));
                break;
            }
        }
    }
}
