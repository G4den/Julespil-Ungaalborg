using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(submitPeriod))]
public class submitPeriodEditor : Editor {
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        submitPeriod Target = (submitPeriod)target;

        GUILayout.Space(20);

        GUILayout.Label("Push Info", EditorStyles.boldLabel);

        Target.EndDate = EditorGUILayout.TextField("Event end date", Target.EndDate);
        Target.IsOver = EditorGUILayout.Toggle("Event over", Target.IsOver);
        Target.Year = EditorGUILayout.IntField("Current Year",Target.Year);

        if (GUILayout.Button("Submit changes"))
            Target.SubmitChanges();

        GUILayout.Space(25);

        GUILayout.Label("Pull Info", EditorStyles.boldLabel);
        GUILayout.Label("Event end date " + Target.endDate);
        GUILayout.Label("Event over " + Target.isOver);
        GUILayout.Label("Current Year " + Target.year);


        GUILayout.Space(25);

        GUILayout.Label("Dreamlo game info sites", EditorStyles.boldLabel);
        if (GUILayout.Button("Open dreamlo database"))
            Application.OpenURL("http://dreamlo.com/lb/uHQ4c5va9UyAg95d1HZUPg3Ml7DlVOK0uP3HVXdkuk2Q");
        if (GUILayout.Button("Open dreamlo gameinfo string"))
            Application.OpenURL("http://dreamlo.com/lb/5bae2eb1613a88061429d460/pipe-get/bY0dqpzLrCvwt2iO3aOg");
    }
}
