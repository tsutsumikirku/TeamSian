using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Action OnEnd;
    //�c�莞��
    private float timeRemaining;
    //���Ԃ������Ă��邩�ǂ���
    private bool isTimeRunning = false;


    public void SetTimeLimit(float timeLimit)
    {
        timeRemaining = timeLimit;
        isTimeRunning = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isTimeRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f) {
            Debug.Log("���Ԑ؂�I");

            //�҂�����O�ɂ���
            timeRemaining = 0f;
            isTimeRunning = false;
            OnEnd?.Invoke();
        }
    }


}
