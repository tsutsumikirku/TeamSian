using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "FoodSyatem")]
public class ScripableObjectHoge : ScriptableObject
{
    //（トマト、肉、チーズ）
    public List<string> SynergyConditions = new List<string>();
    public int SynergyScore;
}
