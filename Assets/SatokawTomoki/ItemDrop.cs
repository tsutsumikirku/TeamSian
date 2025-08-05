using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class ItemDrop : MonoBehaviour
{
    [System.Serializable]
    public class ItemData
    {
        public GameObject itemPrefab;
        public int probability;
    }
    [SerializeField][Header("アイテムリスト")] public List<ItemData> items;
    [SerializeField][Header("落下スピード")] private float downSpeed = 10f;
    [SerializeField][Header("アイテム出現間隔(秒)最小値")] private float intervalMini;
    [SerializeField][Header("アイテム出現間隔 最大値")] private float intervalMax;
    [SerializeField][Header("アイテム出現範囲 横幅")] private float width;
    [SerializeField][Header("アイテム出現範囲 高さ")] private float height;
    [SerializeField] private GameObject cameraObject;
    List<GameObject> dropItems = new List<GameObject>();    
    private float timer = 0f;
    private float intervalTime;
    private int probabilityTotal;
    private int clonedCount = 1;
    bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        intervalTime = 1f;

    }
    public void ManualStart()
    {
        isStart = true;
    }
    public void ManualStop()
    {
        isStart = false;
        foreach (GameObject item in dropItems)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;
        if (timer > intervalTime)
        {
            ItemClone();
            intervalTime = Random.Range(intervalMini, intervalMax);
            timer = 0f;
        }
        timer += Time.deltaTime;
        height = cameraObject.transform.position.y + 13;
    }
    public void ItemClone()
    {
        Vector3 clonePosition = Vector3.zero;
        clonePosition.x = Random.Range(-width, width);
        clonePosition.y = height;
        GameObject itemObject = ItemSelect().itemPrefab;
        GameObject newItem = Instantiate(itemObject);
        dropItems.Add(newItem);
        newItem.transform.position = clonePosition;
        newItem.AddComponent<ItemControl>().moveSpeed = downSpeed;
        newItem.GetComponent<SpriteRenderer>().sortingOrder = clonedCount;
        clonedCount++;
        ////rigbody�̊m�F
        //Rigidbody2D newItemRb =newItem.GetComponent<Rigidbody2D>();
        //if(newItemRb != null )
        //{
        //    newItemRb =  newItem.AddComponent<Rigidbody2D>();
        //}
        //newItemRb.bodyType = RigidbodyType2D.Dynamic;
        //newItemRb.gravityScale = 0f;
    }
    public ItemData ItemSelect()
    {
        probabilityTotal = 0;
        foreach (ItemData item in items)
        {
            probabilityTotal += item.probability;
        }
        int iNum = Random.Range(1, probabilityTotal + 1);
        int total = 0;
        ItemData data = null;
        foreach (ItemData item in items)
        {

            if (total < iNum && total + item.probability >= iNum)
            {
                data = item;
                break;
            }
            total += item.probability;
        }
        if (data == null)
        {
            Debug.LogError("ItemData Null");
        }
        return data;
    }
}