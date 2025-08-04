using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //おそらくシナジー条件を複数に保存している
    [SerializeField] ScripableObjectHoge[] ScripableObjectHoge;
    //中身　→　[スクリプタブルオブジェクト数][レシピ（全パターン）][具材]
    List<string[][]> AllScriptableObjects = new List<string[][]>();
    //ランキングスコアをリストで保存する
    List<int> _RankingScore = new List<int>();
    int TotalScore = 0;

    private void Start()
    {
        //iはレシピの順番
        for (int i = 0; i < ScripableObjectHoge.Length; i++)
        {
            //シナジー効果順不同配列
            //例：（トマト、肉、チーズ）＝＝（肉、チーズ、トマト）
            string[][] synergyConditions = new string[ScripableObjectHoge[i].SynergyConditions.Count][];

            for (int j = 0; j < ScripableObjectHoge[i].SynergyConditions.Count; j++)
            {
                //（シナジーに必要な具材の数 × シナジーに必要な具材の数）という配列が作れる
                synergyConditions[j] = new string[ScripableObjectHoge[i].SynergyConditions.Count];
                int a = 1;
                int ja = j;
                for (int k = 0; k < ScripableObjectHoge[i].SynergyConditions.Count; k++)
                {
                    ja += a;//(初期化された後は1ずつ上昇しなければならない)
                    if (ja >= ScripableObjectHoge[i].SynergyConditions.Count)
                    {
                        ja = 0;
                    }
                    synergyConditions[j][k] = ScripableObjectHoge[i].SynergyConditions[ja];
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
        for (int i = 0; i < foods.Count; i++)
        {
            int foodScore = foods[i].Score;
            string foodName = foods[i].Name;
        }
    }
}
public interface Food
{
    public int Score { get; }
    public string Name { get; }
}