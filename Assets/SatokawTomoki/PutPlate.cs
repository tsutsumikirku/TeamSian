using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPlate : MonoBehaviour
{
    [SerializeField][Header("���M�̃I�u�W�F�N�g")] private GameObject plateObject;
    [SerializeField][Header("�ŏ��̍���")] private float putTopPosition;
    [SerializeField][Header("�I�u�W�F�N�g�̊�")] private float interval;
    private List<GameObject> _itemList = new List<GameObject>();

    public List<GameObject> itemList
    {
        get { return _itemList; }
    }
    public void Start()
    {
        plateObject.AddComponent<PlateCollider>();
    }
    public void Update()
    {
        foreach (GameObject obj in itemList)
        {
            Vector3 position = obj.transform.localPosition;
            position.x = 0;
            obj.transform.localPosition = position;
        }
    }
    public void Put(GameObject prefab, Sprite image)
    {
        GameObject newObject = Instantiate(prefab);

        // �s�v�ȃR���|�[�l���g���폜
        Destroy(newObject.GetComponent<Rigidbody>());
        Destroy(newObject.GetComponent<ItemControl>());

        // �X�v���C�g�ݒ�
        if (image != null)
        {
            newObject.GetComponent<SpriteRenderer>().sprite = image;
        }

        // �e�Ə����ʒu
        newObject.transform.parent = plateObject.transform;
        Vector3 position = Vector3.zero;
        position.x = 0f;

        float positionY = putTopPosition;

        foreach (GameObject item in _itemList)
        {
            SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                positionY += sr.bounds.size.y + interval;
            }
        }

        // �Ō�ɒǉ�����I�u�W�F�N�g�̍������������āA���S�ɒu��
        SpriteRenderer newSR = newObject.GetComponent<SpriteRenderer>();
        if (newSR != null)
        {
            positionY += newSR.bounds.size.y / 2f;
        }

        position.y = positionY;
        newObject.transform.localPosition = position;

        // �����蔻��Ȃ�
        newObject.GetComponent<Collider2D>().isTrigger = true;
        _itemList.Add(newObject);

        FindAnyObjectByType<PhaseAddItem>()?.AddItem(_itemList.Count);

        // �R���C�_�[����̍Đݒ�
        if (plateObject.GetComponent<PlateCollider>() != null)
        {
            Destroy(plateObject.GetComponent<PlateCollider>());
        }
        foreach (GameObject item in _itemList)
        {
            Destroy(item.GetComponent<PlateCollider>());
            item.GetComponent<Collider2D>().isTrigger = true;
        }
        newObject.AddComponent<PlateCollider>();
        newObject.GetComponent<Collider2D>().isTrigger = false;
    }
}