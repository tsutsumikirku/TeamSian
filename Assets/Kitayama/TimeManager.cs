using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Action OnEnd;
    //Žc‚èŽžŠÔ
    private float timeRemaining;
    //ŽžŠÔ‚ª“®‚¢‚Ä‚¢‚é‚©‚Ç‚¤‚©
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
            Debug.Log("ŽžŠÔØ‚êI");

            //‚Ò‚Á‚½‚è‚O‚É‚·‚é
            timeRemaining = 0f;
            isTimeRunning = false;
            OnEnd?.Invoke();
        }
    }


}
