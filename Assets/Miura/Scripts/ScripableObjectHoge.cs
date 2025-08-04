using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "FoodSyatem")]
public class ScripableObjectHoge : ScriptableObject
{
    //�i�g�}�g�A���A�`�[�Y�j
    public List<string> SynergyConditions = new List<string>();
    public int SynergyScore;
}
