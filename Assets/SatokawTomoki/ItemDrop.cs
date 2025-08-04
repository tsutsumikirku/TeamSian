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
    [SerializeField][Header("�A�C�e�����X�g")] public List<ItemData> items;
    [SerializeField][Header("������X�s�[�h")] private float downSpeed = 10f;
    [SerializeField][Header("�A�C�e����������p�x(�b)�ŏ��l")] private float intervalMini;
    [SerializeField][Header("�A�C�e����������p�x�@�ő�l")] private float intervalMax;
    [SerializeField][Header("�A�C�e���𗎂Ƃ�����")] private float width;
    [SerializeField][Header("�A�C�e���𗎂Ƃ�����")] private float height;
    private float timer = 0f;
    private float intervalTime;
    private int probabilityTotal;
    // Start is called before the first frame update
    void Start()
    {
        intervalTime = 1f;
        probabilityTotal = 0;
        foreach(ItemData item in items)
        {
            probabilityTotal += item.probability;
        }
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
        GameObject itemObject = ItemSelect().itemPrefab;
        GameObject newItem = Instantiate(itemObject);
        newItem.transform.position = clonePosition;
        newItem.AddComponent<ItemControl>().moveSpeed = downSpeed;

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
        int iNum = Random.Range(1, probabilityTotal + 1);
        int total = 0;
        ItemData data = null;
        foreach(ItemData item in items)
        {
            
            if(total < iNum && total + item.probability >= iNum)
            {
                data = item;
                break;
            }
            total += item.probability;
        }
        if(data == null)
        {
            Debug.LogError("ItemData Null");
        }
        return data;
    }
}