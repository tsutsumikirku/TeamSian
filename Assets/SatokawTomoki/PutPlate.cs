using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPlate : MonoBehaviour
{
    [SerializeField][Header("お皿のオブジェクト")] private GameObject plateObject;
    [SerializeField][Header("最初の高さ（Plateの上端など）")] private float putTopPosition = 0f;
    [SerializeField][Header("オブジェクト間の間隔")] private float interval = 0.05f;

    private List<GameObject> _itemList = new List<GameObject>();
    public List<GameObject> itemList => _itemList;

    public void Start()
    {
        plateObject.AddComponent<PlateCollider>();
    }

    public void Update()
    {
        // 常にX座標を0に保つ（横ズレ対策）
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

        // 不要なコンポーネント削除
        Destroy(newObject.GetComponent<Rigidbody>());
        Destroy(newObject.GetComponent<ItemControl>());

        // スプライト設定
        if (image != null)
        {
            SpriteRenderer sr = newObject.GetComponent<SpriteRenderer>();
            if (sr != null) sr.sprite = image;
        }

        // 親を設定
        newObject.transform.SetParent(plateObject.transform);

        // 高さを計算（中心を基準に積む）
        float positionY = putTopPosition;

        foreach (GameObject item in _itemList)
        {
            Collider2D col = item.GetComponent<Collider2D>();
            if (col != null)
            {
                positionY += col.bounds.size.y;      // 高さ分積み上げ
                positionY -= col.offset.y;           // コライダー中心補正
                positionY += interval;               // 隙間
            }
        }

        // 新しいオブジェクト自身の中心補正
        Collider2D newCol = newObject.GetComponent<Collider2D>();
        if (newCol != null)
        {
            positionY += newCol.bounds.size.y / 2f;
            positionY -= newCol.offset.y;
        }

        // 位置を設定
        Vector3 position = Vector3.zero;
        position.x = 0f;
        position.y = positionY;
        newObject.transform.localPosition = position;

        // コライダー設定
        if (newCol != null) newCol.isTrigger = true;

        // ソート順設定（手前に積む）
        SpriteRenderer newSR = newObject.GetComponent<SpriteRenderer>();
        if (newSR != null)
        {
            newSR.sortingLayerName = "PlateItems";  // Unityで定義しておく
            newSR.sortingOrder = _itemList.Count;
        }

        // リストに追加
        _itemList.Add(newObject);

        // ゲームの進行通知など
        FindAnyObjectByType<PhaseAddItem>()?.AddItem(_itemList.Count);

        // PlateColliderの管理
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

        // 最後のオブジェクトだけ当たり判定ON
        newObject.AddComponent<PlateCollider>();
        newCol.isTrigger = false;
    }
}
