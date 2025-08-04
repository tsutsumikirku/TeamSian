using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPlate : MonoBehaviour
{
    [SerializeField][Header("お皿のオブジェクト")] private GameObject plateObject;
    [SerializeField][Header("最初の高さ")] private float putTopPosition;
    [SerializeField][Header("オブジェクトの間")] private float interval;
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

        // 不要なコンポーネントを削除
        Destroy(newObject.GetComponent<Rigidbody>());
        Destroy(newObject.GetComponent<ItemControl>());

        // スプライト設定
        if (image != null)
        {
            newObject.GetComponent<SpriteRenderer>().sprite = image;
        }

        // 親と初期位置
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

        // 最後に追加するオブジェクトの高さを加味して、中心に置く
        SpriteRenderer newSR = newObject.GetComponent<SpriteRenderer>();
        if (newSR != null)
        {
            positionY += newSR.bounds.size.y / 2f;
        }

        position.y = positionY;
        newObject.transform.localPosition = position;

        // 当たり判定など
        newObject.GetComponent<Collider2D>().isTrigger = true;
        _itemList.Add(newObject);

        FindAnyObjectByType<PhaseAddItem>()?.AddItem(_itemList.Count);

        // コライダー判定の再設定
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