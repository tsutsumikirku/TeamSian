using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseAddItem : MonoBehaviour
{
    [System.Serializable]
    public class PhaseItem
    {
        public GameObject itemPrefab;
        public int probability;
        public int accumulationCount;
    }
    public List<PhaseItem> addItems;
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
    public void AddItem(int itemCount)
    {
        foreach (PhaseItem item in addItems)
        {
            if (item.accumulationCount > itemCount)
            {
                continue;
            }
            bool isAdded = false;
            foreach (ItemDrop.ItemData data in itemDrop.items)
            {
                if (data.itemPrefab == item.itemPrefab)
                {
                    isAdded = true;
                    break;
                }
            }
            if (!isAdded)
            {
                ItemDrop.ItemData data = new ItemDrop.ItemData();
                data.itemPrefab = item.itemPrefab;
                data.probability = item.probability;
                itemDrop.items.Add(data);
            }
        }
    }
}