using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/* Assignment: Portal
/  Programmer: Owen Jones
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 04/07/2024
*/
[CustomEditor(typeof(TurretSphereVision))]
public class SphereView : Editor
{
    //Place this script in a folder called "Editor"
    void OnSceneGUI()
    {
        TurretSphereVision fow = (TurretSphereVision)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.visionDistance);
        Vector3 visionAngleA = fow.DirFromAngle(-fow.visionAngle / 2, false);
        Vector3 visionAngleB = fow.DirFromAngle(fow.visionAngle / 2, false);
        Handles.DrawLine(fow.transform.position, fow.transform.position + visionAngleA * fow.visionDistance);
        Handles.DrawLine(fow.transform.position, fow.transform.position + visionAngleB * fow.visionDistance);

        Handles.color = Color.red;
        foreach (Transform targetInSight in fow.SpottedTargets)
        {
            Handles.DrawLine(fow.transform.position, targetInSight.position);
        }
    }
}

