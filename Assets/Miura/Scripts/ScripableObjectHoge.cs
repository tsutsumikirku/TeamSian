using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "FoodSystem")]
public class ScripableObjectHoge : ScriptableObject
{
    //�i�g�}�g�A���A�`�[�Y�j
    [SerializeField, Header("コンボの名前を記載してください")]public string Name;
    [SerializeField, Header("コンディションを記載してください")]public List<string> SynergyConditions = new List<string>();
    [SerializeField, Header("スコアを記載してください")]public int SynergyScore;
}
