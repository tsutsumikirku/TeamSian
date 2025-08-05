using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // シナジー情報をScriptableObjectで管理している
    [SerializeField] ScripableObjectHoge[] ScripableObjects;
    [SerializeField] GameObject scoreText; // スコア表示用のUIオブジェクト
    [SerializeField] GameObject comboPanel; // スコアパネルの親オブジェクト
    private GameObject conboText;
    public Action<int> OnScoreUp;
    int TotalScore = 0;
    public void ScoreCount(List<IFood> foods)
    {
        List<Queue<string>> synergyGroups = new List<Queue<string>>();
        for (int i = 0; i < ScripableObjects.Length; i++)
        {
            // シナジーの名前をキューに追加
            Queue<string> synergyQueue = new Queue<string>();
            synergyGroups.Add(synergyQueue);
        }
        foreach (var food in foods)
        {
            TotalScore += food.Score;
            for (int i = 0; i < ScripableObjects.Length; i++)
            {
                //もしシナジー条件をチェックするキューの要素数がコンディションの数と一致したらDequeueする
                if (synergyGroups[i].Count == ScripableObjects[i].SynergyConditions.Count)
                {
                    synergyGroups[i].Dequeue();
                }
                synergyGroups[i].Enqueue(food.Name);
                // シナジー条件に一致するかチェック
                if (ScripableObjects[i].SynergyConditions.All(condition => synergyGroups[i].Contains(condition)))
                {
                    TotalScore += ScripableObjects[i].SynergyScore;
                    synergyGroups[i].Dequeue(); // 一致した条件をキューから削除
                }
            }
        }
        OnScoreUp?.Invoke(TotalScore);
    }
    public bool ComboChack(List<IFood> foods)
    {
        List<Queue<string>> synergyGroups = new List<Queue<string>>();
        for (int i = 0; i < ScripableObjects.Length; i++)
        {
            // シナジーの名前をキューに追加
            Queue<string> synergyQueue = new Queue<string>();
            synergyGroups.Add(synergyQueue);
        }
        for (int j = foods.Count - 1; j >= 0; j--)
        {
            var food = foods[j];
            for (int i = 0; i < ScripableObjects.Length; i++)
            {
                //もしシナジー条件をチェックするキューの要素数がコンディションの数と一致したらDequeueする
                if (synergyGroups[i].Count == ScripableObjects[i].SynergyConditions.Count)
                {
                    synergyGroups[i].Dequeue();
                }
                synergyGroups[i].Enqueue(food.Name);
                // シナジー条件に一致するかチェック
                if (ScripableObjects[i].SynergyConditions.All(condition => synergyGroups[i].Contains(condition)))
                {
                    synergyGroups[i].Dequeue(); // 一致した条件をキューから削除
                    Debug.LogError($"Combo Achieved: {ScripableObjects[i].Name} with score {ScripableObjects[i].SynergyScore}");
                    return true; // 一致したらここで終了
                }
            }
        }
        return false; // 一致しなかった場合
    }
}
public interface IFood
{
    public int Score { get; }
    public string Name { get; }
    public Vector2 Transform { get; }
}