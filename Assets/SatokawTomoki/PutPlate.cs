using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
        // オブジェクトを順に積み直す（ズレ対策）
        float positionY = putTopPosition;

        for (int i = 0; i < _itemList.Count; i++)
        {
            GameObject obj = _itemList[i];
            Collider2D col = obj.GetComponent<Collider2D>();
            if (col != null)
            {
                // 高さ＋補正
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

            // ソート順も再設定
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingLayerName = "PlateItems";
                sr.sortingOrder = i;
            }

            // 全てTriggerにしておいて…
            if (col != null) col.isTrigger = true;
        }

        // 最後のオブジェクトだけ Trigger = false（当たり判定ON）
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
        // 不要なコンポーネント削除
        Destroy(newObject.GetComponent<Rigidbody>());
        Destroy(newObject.GetComponent<ItemControl>());

        // スプライト設定
        SpriteRenderer sr = newObject.GetComponent<SpriteRenderer>();
        if (image != null && sr != null)
        {
            sr.sprite = image;
        }

        // 親設定と初期位置（YはUpdateで再計算するので仮置き）
        newObject.transform.SetParent(plateObject.transform);
        newObject.transform.localPosition = Vector3.zero;

        _itemList.Add(newObject);

        FindAnyObjectByType<PhaseAddItem>()?.AddItem(_itemList.Count);

        // Collider初期設定
        Collider2D col = newObject.GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;

        // PlateCollider管理（全削除 → 最後に再設定）
        if (plateObject.GetComponent<PlateCollider>() != null)
        {
            Destroy(plateObject.GetComponent<PlateCollider>());
        }

        foreach (GameObject item in _itemList)
        {
            Destroy(item.GetComponent<PlateCollider>());
        }

        // ※位置や当たり判定などの処理はUpdateで毎フレーム再計
        //plateObject.transform.DOPunchPosition(new Vector3(0, 0.5f, 0), 0.5f, 3, 1f);

        if (prefab.GetComponent<ItemCollision>().itemName == "バンズ")
        {
            BurgerReset();
        }
    }
}
