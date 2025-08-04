using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] ScripableObjectHoge[] ScripableObjectHoge;

    List<int> _RankingScore = new List<int>();
    int TotalScore = 0;

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