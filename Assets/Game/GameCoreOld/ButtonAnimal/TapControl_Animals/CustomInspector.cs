using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

#if UNITY_EDITOR

[CustomEditor(typeof(PlayAnimation))]
public class InspectorPlayAnimation : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayAnimation playAnimation = (PlayAnimation)target;

        if (GUILayout.Button("Play Animation Open"))
        {
            playAnimation.PlayOpenAnimation();
        }

        if (GUILayout.Button("Play Animation Close"))
        {
            playAnimation.PlayCloseAnimation();
        }
    }
}

#endif