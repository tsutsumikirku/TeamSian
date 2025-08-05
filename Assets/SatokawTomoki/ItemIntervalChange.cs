using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemIntervalChange : MonoBehaviour
{
    public enum mode
    {
        itemCount//, phase
    }
    public mode changeMode;
    public float itemCountincrease;
    public float phaseincrease;
    private PhaseAddItem phaseAddItem;
    private ItemDrop itemDrop;
    private PhaseAddItem.phase phase;
    // Start is called before the first frame update
    void Start()
    {
        phaseAddItem = FindAnyObjectByType<PhaseAddItem>();
        itemDrop = FindAnyObjectByType<ItemDrop>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(changeMode == mode.phase)
        //{
        //    phaseChange();
        //}
    }
    public void ItemCountChange()
    {
        itemDrop.intervalMax -= itemCountincrease;
        itemDrop.intervalMini -= itemCountincrease;
    }
    public void phaseChange()
    {
        if (Camera.main.gameObject.transform.position.y > phaseAddItem.spaceHeight && phase == PhaseAddItem.phase.sky)
        {
            itemDrop.intervalMax /= phaseincrease;
            itemDrop.intervalMini /= phaseincrease;
            phase = PhaseAddItem.phase.space;
            Debug.Log("大気圏フェーズ");
        }
        else if (Camera.main.gameObject.transform.position.y > phaseAddItem.skyHeight && phase == PhaseAddItem.phase.shop)
        {
            itemDrop.intervalMax /= phaseincrease;
            itemDrop.intervalMini /= phaseincrease;
            phase = PhaseAddItem.phase.sky;
            Debug.Log("青空");
        }
        else
            phase = PhaseAddItem.phase.shop;
    }

}
