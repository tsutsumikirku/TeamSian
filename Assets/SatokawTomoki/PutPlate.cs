using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
        // �I�u�W�F�N�g�����ɐςݒ����i�Y���΍�j
        float positionY = putTopPosition;

        for (int i = 0; i < _itemList.Count; i++)
        {
            GameObject obj = _itemList[i];
            Collider2D col = obj.GetComponent<Collider2D>();
            if (col != null)
            {
                // �����{�␳
                positionY += col.bounds.size.y;
                positionY -= col.offset.y;
                positionY += interval;
            }

            Collider2D newCol = obj.GetComponent<Collider2D>();
            if (newCol != null)
            {
                float centerOffset = newCol.bounds.size.y / 2f - newCol.offset.y;
                float localY = positionY + centerOffset;

                Vector3 newLocalPos = obj.transform.localPosition;
                newLocalPos.x = 0f;
                newLocalPos.y = localY;
                obj.transform.localPosition = newLocalPos;
            }

            // �\�[�g�����Đݒ�
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingLayerName = "PlateItems";
                sr.sortingOrder = i;
            }

            // �S��Trigger�ɂ��Ă����āc
            if (col != null) col.isTrigger = true;
        }

        // �Ō�̃I�u�W�F�N�g���� Trigger = false�i�����蔻��ON�j
        if (_itemList.Count > 0)
        {
            GameObject last = _itemList[_itemList.Count - 1];
            Collider2D lastCol = last.GetComponent<Collider2D>();
            if (lastCol != null)
            {
                lastCol.isTrigger = false;
            }

            if (last.GetComponent<PlateCollider>() == null)
            {
                last.AddComponent<PlateCollider>();
            }
        }
    }
    public void BurgerReset()
    {
        foreach(GameObject obj in  _itemList)
        {
            Destroy(obj);
        }
        _itemList.Clear();
        plateObject.AddComponent<PlateCollider>();  
        FindAnyObjectByType<CameraWork>().CameraReset();

    }
    public void Put(GameObject prefab, Sprite image)
    {
        GameObject newObject = Instantiate(prefab);
        // �s�v�ȃR���|�[�l���g�폜
        Destroy(newObject.GetComponent<Rigidbody>());
        Destroy(newObject.GetComponent<ItemControl>());

        // �X�v���C�g�ݒ�
        SpriteRenderer sr = newObject.GetComponent<SpriteRenderer>();
        if (image != null && sr != null)
        {
            sr.sprite = image;
        }

        // �e�ݒ�Ə����ʒu�iY��Update�ōČv�Z����̂ŉ��u���j
        newObject.transform.SetParent(plateObject.transform);
        newObject.transform.localPosition = Vector3.zero;

        _itemList.Add(newObject);

        FindAnyObjectByType<PhaseAddItem>()?.AddItem(_itemList.Count);

        // Collider�����ݒ�
        Collider2D col = newObject.GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;

        // PlateCollider�Ǘ��i�S�폜 �� �Ō�ɍĐݒ�j
        if (plateObject.GetComponent<PlateCollider>() != null)
        {
            Destroy(plateObject.GetComponent<PlateCollider>());
        }

        foreach (GameObject item in _itemList)
        {
            Destroy(item.GetComponent<PlateCollider>());
        }

        // ���ʒu�ⓖ���蔻��Ȃǂ̏�����Update�Ŗ��t���[���Čv
        //plateObject.transform.DOPunchPosition(new Vector3(0, 0.5f, 0), 0.5f, 3, 1f);

        if (prefab.GetComponent<ItemCollision>().itemName == "�o���Y")
        {
            BurgerReset();
        }
    }
}
