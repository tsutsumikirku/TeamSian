using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] [Header("�A�C�e�����X�g")] private List<GameObject> items;
    [SerializeField][Header("������X�s�[�h")] private float downSpeed = 10f;
    [SerializeField][Header("�A�C�e����������p�x(�b)�ŏ��l")] private float intervalMini;
    [SerializeField][Header("�A�C�e����������p�x�@�ő�l")] private float intervalMax;
    [SerializeField][Header("�A�C�e���𗎂Ƃ�����")] private float width;
    [SerializeField][Header("�A�C�e���𗎂Ƃ�����")] private float height;
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