using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //�����炭�V�i�W�[�����𕡐��ɕۑ����Ă���
    [SerializeField] ScripableObjectHoge[] ScripableObjectHoge;
    //���g�@���@[�X�N���v�^�u���I�u�W�F�N�g��][���V�s�i�S�p�^�[���j][���]
    List<string[][]> AllScriptableObjects = new List<string[][]>();
    //�����L���O�X�R�A�����X�g�ŕۑ�����
    List<int> _RankingScore = new List<int>();
    int TotalScore = 0;

    private void Start()
    {
        //i�̓��V�s�̏���
        for (int i = 0; i < ScripableObjectHoge.Length; i++)
        {
            //�V�i�W�[���ʏ��s���z��
            //��F�i�g�}�g�A���A�`�[�Y�j�����i���A�`�[�Y�A�g�}�g�j
            string[][] synergyConditions = new string[ScripableObjectHoge[i].SynergyConditions.Count][];

            for (int j = 0; j < ScripableObjectHoge[i].SynergyConditions.Count; j++)
            {
                //�i�V�i�W�[�ɕK�v�ȋ�ނ̐� �~ �V�i�W�[�ɕK�v�ȋ�ނ̐��j�Ƃ����z�񂪍���
                synergyConditions[j] = new string[ScripableObjectHoge[i].SynergyConditions.Count];
                int a = 1;
                int ja = j;
                for (int k = 0; k < ScripableObjectHoge[i].SynergyConditions.Count; k++)
                {
                    ja += a;//(���������ꂽ���1���㏸���Ȃ���΂Ȃ�Ȃ�)
                    if (ja >= ScripableObjectHoge[i].SynergyConditions.Count)
                    {
                        ja = 0;
                    }
                    synergyConditions[j][k] = ScripableObjectHoge[i].SynergyConditions[ja];
                }
            }
            AllScriptableObjects.Add(synergyConditions);//�񎟌��z���ǉ�����K�v����
        }

        for (int i = 0; i < AllScriptableObjects.Count; i++)
        {
            for (int j = 0; j < AllScriptableObjects[i].Length; j++)
            {
                for (int k = 0; k < AllScriptableObjects[i][j].Length; k++)
                {
                    Debug.Log($"���V�s{i}��{j}�Ԗڂ̕\��{k}�Ԗڂ̋�ނ�{AllScriptableObjects[i][j][k]}");
                }
            }
        }
    }
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
    /// <summary>
    /// �n���o�[�K�[1���̃X�R�A���v�Z����
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