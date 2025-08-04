using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    List<float> _RankingScore = new List<float>();


}

public interface Food
{
    public int _Score { get; }
    public List<string> Names { get; }
}