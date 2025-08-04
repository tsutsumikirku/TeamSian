using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //�����炭�V�i�W�[�����𕡐��ɕۑ����Ă���
    [SerializeField] ScripableObjectHoge[] ScripableObjects;

    //���g�@���@[�X�N���v�^�u���I�u�W�F�N�g��][���V�s�i�S�p�^�[���j][���]
    List<string[][]> AllScriptableObjects = new List<string[][]>();
    //�����L���O�X�R�A�����X�g�ŕۑ�����
    List<int> _RankingScore = new List<int>();
    int TotalScore = 0;


    private void Start()
    {
        //i�̓��V�s�̏���
        for (int i = 0; i < ScripableObjects.Length; i++)
        {
            //�V�i�W�[���ʏ��s���z��
            //��F�i�g�}�g�A���A�`�[�Y�j�����i���A�`�[�Y�A�g�}�g�j
            string[][] synergyConditions = new string[ScripableObjects[i].SynergyConditions.Count][];

            for (int j = 0; j < ScripableObjects[i].SynergyConditions.Count; j++)
            {
                //�i�V�i�W�[�ɕK�v�ȋ�ނ̐� �~ �V�i�W�[�ɕK�v�ȋ�ނ̐��j�Ƃ����z�񂪍���
                synergyConditions[j] = new string[ScripableObjects[i].SynergyConditions.Count];
                int a = 1;
                int ja = j;
                for (int k = 0; k < ScripableObjects[i].SynergyConditions.Count; k++)
                {
                    ja += a;//(���������ꂽ���1���㏸���Ȃ���΂Ȃ�Ȃ�)
                    if (ja >= ScripableObjects[i].SynergyConditions.Count)
                    {
                        ja = 0;
                    }
                    synergyConditions[j][k] = ScripableObjects[i].SynergyConditions[ja];
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
        //������
        List<ScripableObjectHoge> synergyList = new List<ScripableObjectHoge>();//���V�s�������쐬
        List<Food[]> givenBurgers = new List<Food[]>();//�n���ꂽ�o�[�K�[�����V�s�Ԃ񕡐����Ă���[foods.Count][foods]���o�[�K�[�ɐς܂�Ă���S��ށ@���@
        for (int i = 0; i < ScripableObjects.Length; i++)//���V�s��
        {
            synergyList.Add(ScripableObjects[i]);
        }
        for (int i = 0; i < ScripableObjects.Length; i++)//���V�s��
        {
            givenBurgers.Add(new Food[foods.Count]);
            for (int j = 0; j < foods.Count; j++)
            {
                givenBurgers[i][j] = foods[j];//givenBurgers.Count == ScripableObjects.Length
            }
        }
        //�S�p�^�[���V�i�W�[����������o����Ă���O���
        //�����ŋ�ނ̃X�R�A�����Z������Ԏ��݂Ă݂�
        //���ׂ�����
        ///�EgivenBurgers��
        ///�E
        for (int i = 0; i < synergyList.Count; i++)//���V�s�̐�
        {
            int successConditionsScore = 0;
            successConditionsScore = JudgmentSuccessConditionsScore(synergyList[i], givenBurgers[i]);

            //for (int j = 0; j < foods.Count; j++)//�S�p�^�[���̏�������������̂ɕK�v
            //{
            //    successConditions = JudgmentSuccessConditions();
            //}
            int synergyScore = 0;
            
            synergyScore = successConditionsScore;
            TotalScore += GetCalculateGivenScore(synergyScore, foods[i].Score);
        }
    }
    int JudgmentSuccessConditionsScore(ScripableObjectHoge synergyList, Food[] givenBurgers)//�����͈�x����������Ȃ��EFood�͕������ꂽQueue�̒�����ЂƂ����������
    {
        int successConditionsScore = 0;
        List<string> SynergyConditions = synergyList.SynergyConditions; //���V�s����istring�̃R���N�V�����j
        Queue<string> seachStringQueue = new Queue<string>();
        for (int i = 0; i < givenBurgers.Length; i++)//��ނ̎�ނԂ�J��Ԃ�
        {
            seachStringQueue.Enqueue(givenBurgers[i].Name);
            if (seachStringQueue.Count > SynergyConditions.Count)
            {
                seachStringQueue.Dequeue();
            }
            string[] searchString = seachStringQueue.ToArray();
            bool isSuccess = true;
            for (int j = 0; j < synergyList.SynergyConditions.Count; j ++)//���V�s�̐������J��Ԃ�
            {
                string judgIngredientsName = givenBurgers[j].Name;//���V�s��j�Ԗڂ̖��O �� 0,1,2,3,4,
                string judgCondition = searchString[i];//i�Ԗڂɂ����ނ̖��O �� j�̌������I�������ɋ�ނ��X�V�����
                if (judgIngredientsName != judgCondition)
                {
                    isSuccess = false;
                }
            }
        }
        return successConditionsScore;
    }
}
public interface Food
{
    public int Score { get; }
    public string Name { get; }
}