using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Link))]
public class LinksEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Leaderboard"))
            Application.OpenURL("http://dreamlo.com/lb/qJ9WiOZafE6bxhb6Xxzb0QXgW8WHU8K0CB-NBSnOuvTw");
        GUILayout.Space(25);
        if (GUILayout.Button("Gameinfo"))
            Application.OpenURL("http://dreamlo.com/lb/5bae2eb1613a88061429d460/pipe-get/bY0dqpzLrCvwt2iO3aOg");
        GUILayout.Space(25);
        GUILayout.Label("Login til både onesignal og google \nUsername: Station10JuleSpil@gmail.com \nPassword: codeerbae");
        if (GUILayout.Button("One Signal"))
            Application.OpenURL("https://onesignal.com/apps/a19d26e6-8528-40ec-a020-76c6b8e249ea/templates/821f74ca-33d5-413b-a8eb-92138b12a11b/notifications/new");
    }
}
