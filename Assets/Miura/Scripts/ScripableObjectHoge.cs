using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "FoodSyatem")]
public class ScripableObjectHoge : ScriptableObject
{
    public List<string> SynergyConditions = new List<string>();
    public int FoodScore;
}
