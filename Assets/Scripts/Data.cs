using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "ScriptableObjects/Data/Tsutsumi")]
public class Data : ScriptableObject
{
    public string[] Name;
    public int Score;
}
