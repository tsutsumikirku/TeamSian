using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPlate : MonoBehaviour
{
    [SerializeField][Header("���M�̃I�u�W�F�N�g")] private GameObject plateObject;
    [SerializeField][Header("�ŏ��̍����iPlate�̏�[�Ȃǁj")] private float putTopPosition = 0f;
    [SerializeField][Header("�I�u�W�F�N�g�Ԃ̊Ԋu")] private float interval = 0.05f;

    private List<GameObject> _itemList = new List<GameObject>();
    public List<GameObject> itemList => _itemList;

    public void Start()
    {
        plateObject.AddComponent<PlateCollider>();
    }

    public void Update()
    {
        // ���X���W��0�ɕۂi���Y���΍�j
        foreach (GameObject obj in _itemList)
        {
            Vector3 position = obj.transform.localPosition;
            position.x = 0f;
            obj.transform.localPosition = position;
        }
    }

    public void Put(GameObject prefab, Sprite image)
    {
        GameObject newObject = Instantiate(prefab);

        // �s�v�ȃR���|�[�l���g�폜
        Destroy(newObject.GetComponent<Rigidbody>());
        Destroy(newObject.GetComponent<ItemControl>());

        // �X�v���C�g�ݒ�
        if (image != null)
        {
            SpriteRenderer sr = newObject.GetComponent<SpriteRenderer>();
            if (sr != null) sr.sprite = image;
        }

        // �e��ݒ�
        newObject.transform.SetParent(plateObject.transform);

        // �������v�Z�i���S����ɐςށj
        float positionY = putTopPosition;

        foreach (GameObject item in _itemList)
        {
            Collider2D col = item.GetComponent<Collider2D>();
            if (col != null)
            {
                positionY += col.bounds.size.y;      // �������ςݏグ
                positionY -= col.offset.y;           // �R���C�_�[���S�␳
                positionY += interval;               // ����
            }
        }

        // �V�����I�u�W�F�N�g���g�̒��S�␳
        Collider2D newCol = newObject.GetComponent<Collider2D>();
        if (newCol != null)
        {
            positionY += newCol.bounds.size.y / 2f;
            positionY -= newCol.offset.y;
        }

        // �ʒu��ݒ�
        Vector3 position = Vector3.zero;
        position.x = 0f;
        position.y = positionY;
        newObject.transform.localPosition = position;

        // �R���C�_�[�ݒ�
        if (newCol != null) newCol.isTrigger = true;

        // �\�[�g���ݒ�i��O�ɐςށj
        SpriteRenderer newSR = newObject.GetComponent<SpriteRenderer>();
        if (newSR != null)
        {
            newSR.sortingLayerName = "PlateItems";  // Unity�Œ�`���Ă���
            newSR.sortingOrder = _itemList.Count;
        }

        // ���X�g�ɒǉ�
        _itemList.Add(newObject);

        // �Q�[���̐i�s�ʒm�Ȃ�
        FindAnyObjectByType<PhaseAddItem>()?.AddItem(_itemList.Count);

        // PlateCollider�̊Ǘ�
        if (plateObject.GetComponent<PlateCollider>() != null)
        {
            Destroy(plateObject.GetComponent<PlateCollider>());
            plateObject.GetComponent<Collider2D>().isTrigger = true;
        }

        foreach (GameObject item in _itemList)
        {
            Destroy(item.GetComponent<PlateCollider>());
            item.GetComponent<Collider2D>().isTrigger = true;
        }

        // �Ō�̃I�u�W�F�N�g���������蔻��ON
        newObject.AddComponent<PlateCollider>();
        newCol.isTrigger = false;
    }
}
