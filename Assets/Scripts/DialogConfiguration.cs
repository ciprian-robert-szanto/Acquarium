using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public struct Dialog {
    public Sprite image;
    public string title;
    public string message;
}

[CreateAssetMenu(fileName = "DialogConfiguration", menuName = "Configurations/Dialog Configuration", order = 1)]
public class DialogConfiguration: ScriptableObject
{
    public List<Dialog> dialogs = new List<Dialog>();
}
