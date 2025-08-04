using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] [Header("アイテムリスト")] private List<GameObject> items;
    [SerializeField][Header("落ちるスピード")] private float downSpeed = 10f;
    [SerializeField][Header("アイテムが落ちる頻度(秒)最小値")] private float intervalMini;
    [SerializeField][Header("アイテムが落ちる頻度　最大値")] private float intervalMax;
    [SerializeField][Header("アイテムを落とす横幅")] private float width;
    [SerializeField][Header("アイテムを落とす高さ")] private float height;
    private float timer = 0f;
    private float intervalTime;
    // Start is called before the first frame update
    void Start()
    {
        intervalTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > intervalTime)
        {
            ItemClone();
            intervalTime = Random.Range(intervalMini, intervalMax);
            timer = 0f;
        }
        timer += Time.deltaTime;
    }
    public void ItemClone()
    {
        Vector3 clonePosition = Vector3.zero;
        clonePosition.x = Random.Range(-width, width);
        clonePosition.y = height;
        GameObject newItem = Instantiate(items[Random.Range(0, items.Count-1)]);
        newItem.transform.position = clonePosition;
        newItem.AddComponent<ItemControl>().moveSpeed = downSpeed;
    }
}