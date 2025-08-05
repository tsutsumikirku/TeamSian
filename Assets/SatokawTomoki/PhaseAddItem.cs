using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseAddItem : MonoBehaviour
{
    public GameObject cameraObject;
    [System.Serializable]
    public class PhaseItem
    {
        public GameObject itemPrefab;
        public int probability;
        public phase phaseHeight;
    }
    public List<PhaseItem> addItems;
    public enum phase
    {
        shop,sky,space
    }
    public int shopHeight;
    public int skyHeight;
    public int spaceHeight;
    private ItemDrop itemDrop;

    // Start is called before the first frame update
    void Start()
    {
        itemDrop = FindAnyObjectByType<ItemDrop>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddItem()
    {
        foreach (PhaseItem item in addItems)
        {
            int goalHight = 0; 
            switch (item.phaseHeight)
            {
                case phase.shop:
                    goalHight = shopHeight;
                    break;
                    case phase.sky:
                    goalHight = skyHeight;
                    break;
                    case phase.space:
                    goalHight = spaceHeight;
                    break;
            }
            if (goalHight > cameraObject.transform.position.y)
            {
                continue;
            }
            bool isAdded = false;
            int index = 0;
            int i = 0;
            foreach (ItemDrop.ItemData itemdata in itemDrop.items)
            {
                if (itemdata.itemPrefab == item.itemPrefab)
                {
                    isAdded = true;
                    index = i;
                    break;
                }
                i++;
            }
            ItemDrop.ItemData data = new ItemDrop.ItemData();
            data.itemPrefab = item.itemPrefab;
            data.probability = item.probability;
            if (isAdded)
            {
                itemDrop.items[index] = data;
            }
            else
            {
                
                itemDrop.items.Add(data);
            }
        }
    }
}