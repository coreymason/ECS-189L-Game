using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIVision))]
public class AIVisionPreview : Editor
{
    private void OnSceneGUI()
    {
        AIVision vision = (AIVision) target;
        Handles.color = Color.green;
        Handles.DrawWireArc(vision.transform.position, Vector3.forward, Vector3.up, 360, vision.viewRadius);
        
        Vector3 viewAngleA = vision.DirectionFromAngle(-vision.viewAngle / 2, false);
        Vector3 viewAngleB = vision.DirectionFromAngle(vision.viewAngle / 2, false);
        Handles.DrawLine(vision.transform.position, vision.transform.position + viewAngleA * vision.viewRadius);
        Handles.DrawLine(vision.transform.position, vision.transform.position + viewAngleB * vision.viewRadius);
        
        Handles.color = Color.magenta;
        foreach (var foundTarget in vision.foundTargets)
        {
            Handles.DrawLine(vision.transform.position, foundTarget.position);
        }
    }
}
