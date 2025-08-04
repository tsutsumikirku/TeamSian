using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "FoodSyatem")]
public class ScripableObjectHoge : ScriptableObject
{
    public List<string> _hogeString = new List<string>();
    public int _PerhapsFoodScore;
}
