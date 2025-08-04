using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] ScripableObjectHoge[] ScripableObjectHoge;

    List<int> _RankingScore = new List<int>();
    int TotalScore = 0;

    /// <summary>
    /// �V�i�W�[�X�R�A�Ƌ�ނ̃X�R�A�������ɓn���ƁA�������l���Ԃ��Ă���B
    /// </summary>
    /// <param name="synergyScore"></param>
    /// <param name="foodScore"></param>
    /// <returns></returns>
    public int GetCalculateGivenScore(int synergyScore, int foodScore)
    {
        return synergyScore + foodScore;
    }
    /// <summary>
    /// �V�i�W�[�X�R�A�Ƌ�ނ̃X�R�A�𑫂����ϐ��iGivenScore�j��n���ƃ��U���g��ʂɕ\�������TotalScore�ɉ��Z�����B
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