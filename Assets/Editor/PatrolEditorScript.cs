using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(PatrolScript))]
public class PatrolEditorScript : Editor {
    public void OnSceneGUI()
    {
        PatrolScript ps = (PatrolScript)target;

        EditorGUI.BeginChangeCheck();
        for (int i = 0; i < ps.patrolPoints.Count; i++)
        {
            ps.patrolPoints[i] = Handles.DoPositionHandle(ps.patrolPoints[i], Quaternion.identity);
            Debug.DrawLine(ps.patrolPoints[i], ps.patrolPoints[(i + 1) % ps.patrolPoints.Count]);
        }

        if (EditorGUI.EndChangeCheck())
        {
            // then add an undo point
            Undo.RecordObject(ps, "Move Patrol Point");
            EditorSceneManager.MarkAllScenesDirty();
        }
    }
}
