using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //おそらくシナジー条件を複数に保存している
    [SerializeField] ScripableObjectHoge[] ScripableObjects;

    //中身　→　[スクリプタブルオブジェクト数][レシピ（全パターン）][具材]
    List<string[][]> AllScriptableObjects = new List<string[][]>();
    //ランキングスコアをリストで保存する
    List<int> _RankingScore = new List<int>();
    int TotalScore = 0;


    private void Start()
    {
        //iはレシピの順番
        for (int i = 0; i < ScripableObjects.Length; i++)
        {
            //シナジー効果順不同配列
            //例：（トマト、肉、チーズ）＝＝（肉、チーズ、トマト）
            string[][] synergyConditions = new string[ScripableObjects[i].SynergyConditions.Count][];

            for (int j = 0; j < ScripableObjects[i].SynergyConditions.Count; j++)
            {
                //（シナジーに必要な具材の数 × シナジーに必要な具材の数）という配列が作れる
                synergyConditions[j] = new string[ScripableObjects[i].SynergyConditions.Count];
                int a = 1;
                int ja = j;
                for (int k = 0; k < ScripableObjects[i].SynergyConditions.Count; k++)
                {
                    ja += a;//(初期化された後は1ずつ上昇しなければならない)
                    if (ja >= ScripableObjects[i].SynergyConditions.Count)
                    {
                        ja = 0;
                    }
                    synergyConditions[j][k] = ScripableObjects[i].SynergyConditions[ja];
                }
            }
            AllScriptableObjects.Add(synergyConditions);//二次元配列を追加する必要あり
        }

        for (int i = 0; i < AllScriptableObjects.Count; i++)
        {
            for (int j = 0; j < AllScriptableObjects[i].Length; j++)
            {
                for (int k = 0; k < AllScriptableObjects[i][j].Length; k++)
                {
                    Debug.Log($"レシピ{i}の{j}番目の表の{k}番目の具材は{AllScriptableObjects[i][j][k]}");
                }
            }
        }
    }
    /// <summary>
    /// シナジースコアと具材のスコアを引数に渡すと、足した値が返ってくる。
    /// </summary>
    /// <param name="synergyScore"></param>
    /// <param name="foodScore"></param>
    /// <returns></returns>
    public int GetCalculateGivenScore(int synergyScore, int foodScore)
    {
        return synergyScore + foodScore;
    }
    /// <summary>
    /// シナジースコアと具材のスコアを足した変数（GivenScore）を渡すとリザルト画面に表示されるTotalScoreに加算される。
    /// </summary>
    /// <param name="givenScore"></param>
    public void AddTotalScore(int givenScore)
    {
        TotalScore += givenScore;
    }
    /// <summary>
    /// ハンバーガー1個分のスコアを計算する
    /// </summary>
    /// <param name="foods"></param>
    public void ScoreCount(List<Food> foods)
    {
        //初期化
        List<Queue<ScripableObjectHoge>> synergyList = new List<Queue<ScripableObjectHoge>>();
        List<Queue<Food>> givenBurgers = new List<Queue<Food>>();
        for (int i = 0; i < ScripableObjects.Length; i++)//レシピ数
        {
            synergyList[i] = new Queue<ScripableObjectHoge>();
            for (int j = 0; j < ScripableObjects.Length; j++)
            {
                synergyList[i].Enqueue(ScripableObjects[j]);//List.Count == ScripableObjects.Length
            }
        }
        for (int i = 0; i < ScripableObjects.Length; i++)//レシピ数
        {
            givenBurgers[i] = new Queue<Food>();
            for (int j = 0; j < foods.Count; j++)
            {
                givenBurgers[i].Enqueue(foods[j]);//givenBurgers.Count == ScripableObjects.Length
            }
        }
        //全パターンシナジー条件が割り出されている前提で
        //ここで具材のスコア足し算をぜんぶ試みてみる
        //やるべきこと
        ///・givenBurgersに
        ///・
        for (int i = 0; i < ScripableObjects.Length; i++)//具材の種類
        {
            ScripableObjectHoge successConditions = new ScripableObjectHoge();
            for (int j = 0; j < foods.Count; j++)//全パターンの条件を検索するのに必要
            {
                successConditions = JudgmentSuccessConditions();
            }
            int synergyScore = 0;
            
            synergyScore = successConditions.SynergyScore;
            TotalScore += GetCalculateGivenScore(synergyScore, foods[i].Score);
        }
    }
    ScripableObjectHoge JudgmentSuccessConditions()
    {

        ScripableObjectHoge successConditions = new ScripableObjectHoge();
        return successConditions;
    }
}
public interface Food
{
    public int Score { get; }
    public string Name { get; }
}