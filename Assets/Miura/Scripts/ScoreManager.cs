using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    //private void Start()
    //{
    //    //iはレシピの順番
    //    for (int i = 0; i < ScripableObjects.Length; i++)
    //    {
    //        //シナジー効果順不同配列
    //        //例：（トマト、肉、チーズ）＝＝（肉、チーズ、トマト）
    //        string[][] synergyConditions = new string[ScripableObjects[i].SynergyConditions.Count][];

    //        for (int j = 0; j < ScripableObjects[i].SynergyConditions.Count; j++)
    //        {
    //            //（シナジーに必要な具材の数 × シナジーに必要な具材の数）という配列が作れる
    //            synergyConditions[j] = new string[ScripableObjects[i].SynergyConditions.Count];
    //            int a = 1;
    //            int ja = j;
    //            for (int k = 0; k < ScripableObjects[i].SynergyConditions.Count; k++)
    //            {
    //                ja += a;//(初期化された後は1ずつ上昇しなければならない)
    //                if (ja >= ScripableObjects[i].SynergyConditions.Count)
    //                {
    //                    ja = 0;
    //                }
    //                synergyConditions[j][k] = ScripableObjects[i].SynergyConditions[ja];
    //            }
    //        }
    //        AllScriptableObjects.Add(synergyConditions);//二次元配列を追加する必要あり
    //    }

    //    for (int i = 0; i < AllScriptableObjects.Count; i++)
    //    {
    //        for (int j = 0; j < AllScriptableObjects[i].Length; j++)
    //        {
    //            for (int k = 0; k < AllScriptableObjects[i][j].Length; k++)
    //            {
    //                Debug.Log($"レシピ{i}の{j}番目の表の{k}番目の具材は{AllScriptableObjects[i][j][k]}");
    //            }
    //        }
    //    }
    //}
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
    /// ハンバーガー1個分のスコアを計算する
    /// </summary>
    /// <param name="foods"></param>
    public void ScoreCount(List<Food> foods)
    {
        //初期化・計算に影響はないはず

        //ScripableObjectsを新規リスト化
        List<ScripableObjectHoge> synergyList = ScripableObjects.ToList();

        //計算開始・全レシピを参照
        for (int i = 0; i < synergyList.Count; i++)//レシピの数
        {
            int synergyScore = 0;
            int successConditionsScore = 0;

            successConditionsScore = JudgmentSuccessConditionsScore(synergyList[i], foods);
            synergyScore = successConditionsScore;
            TotalScore += GetCalculateGivenScore(synergyScore, foods[i].Score);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="synergyList"></param>
    /// <param name="givenBurgers"></param>
    /// <returns></returns>
    int JudgmentSuccessConditionsScore(ScripableObjectHoge synergyList, List<Food> givenBurgers)//条件は一度しか試されない・Foodは複製されたQueueの中からひとつだけ試される
    {
        int successConditionsScore = 0;
        List<string> SynergyConditions = synergyList.SynergyConditions; //レシピ一つ分（stringのコレクション）
        Queue<string> seachStringQueue = new Queue<string>();
        for (int i = 0; i < givenBurgers.Count; i++)//具材の種類ぶん繰り返す
        {
            seachStringQueue.Enqueue(givenBurgers[i].Name);
            if (seachStringQueue.Count > SynergyConditions.Count)
            {
                seachStringQueue.Dequeue();
            }
            string[] searchString = seachStringQueue.ToArray();
            bool isSuccess = true;
            for (int j = 0; j < synergyList.SynergyConditions.Count; j ++)//レシピの中にある要素数だけ繰り返す
            {
                if (SynergyConditions[j] != searchString[j])
                {
                    isSuccess = false;
                }
            }
            if (isSuccess)
            {
                successConditionsScore = synergyList.SynergyScore;
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