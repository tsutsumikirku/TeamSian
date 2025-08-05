using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField, Header("TimeManager")] private TimeManager _timeManager;
    [SerializeField, Header("ScoreManager")] private ScoreManager _scoreManager;
    [SerializeField, Header("ItemDrop")] private ItemDrop _itemDrop;
    [SerializeField, Header("時間制限を設定してください")] private int _timeLimit = 200;
    [SerializeField, Header("制限時間を表示するテキストをバインドしてください")] TextMeshProUGUI _timeManagerText;
    [SerializeField, Header("現在のスコアを表示するテキストをバインドしてください")] TextMeshProUGUI _scoreManagerText;

    void Start()
    {
        _itemDrop.ManualStart();
        _timeManager.SetTimeLimit(_timeLimit);
        _timeManager.OnUpdate = (x) =>
        {
            _timeManagerText.text = (x / 60).ToString("00") + ":" + (x % 60).ToString("00");
        };
        _scoreManager.OnScoreUp = (x) =>
        {
            _scoreManagerText.text = x.ToString();
        };
        _timeManager.OnEnd = () => _itemDrop.ManualStop();
    }
}
