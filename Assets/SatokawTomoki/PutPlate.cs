using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SocialPlatforms.Impl;
public class PutPlate : MonoBehaviour
{
    [SerializeField][Header("皿のオブジェクト")] private GameObject _plateObject;
    [SerializeField][Header("最上部の位置（Plateの座標基準）")] private float _putTopPosition = 0f;
    [SerializeField][Header("オブジェクト間の間隔")] private float _interval = 0.05f;
    [SerializeField][Header("ScoreManagerの参照")] private ScoreManager _scoreManager;

    private List<GameObject> _itemList = new List<GameObject>();
    public List<GameObject> itemList => _itemList;

    public void Start()
    {
        _plateObject.AddComponent<PlateCollider>();
    }

    public void Update()
    {
        // オブジェクトを順に積み上げる（Y座標計算）
        float positionY = _putTopPosition;

        for (int i = 0; i < _itemList.Count; i++)
        {
            GameObject obj = _itemList[i];
            Collider2D col = obj.GetComponent<Collider2D>();
            if (col != null)
            {
                // 高さ分加算
                positionY += col.bounds.size.y;
                positionY -= col.offset.y;
                positionY += _interval;
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

            // ソート順を設定
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingLayerName = "PlateItems";
                sr.sortingOrder = i;
            }

            // 全てTriggerにしておく
            if (col != null) col.isTrigger = true;
        }

        // 最後のオブジェクトだけ Trigger = false（衝突判定ON）
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
        foreach (GameObject obj in _itemList)
        {
            Destroy(obj);
        }
        _itemList.Clear();
        _plateObject.AddComponent<PlateCollider>();
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

        // 親設定＆初期位置（YはUpdateで再計算されるので仮でOK）
        newObject.transform.SetParent(_plateObject.transform);
        newObject.transform.localPosition = Vector3.zero;

        _itemList.Add(newObject);

        FindAnyObjectByType<PhaseAddItem>()?.AddItem();

        // Collider初期設定
        Collider2D col = newObject.GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;

        // PlateCollider管理（全削除 → 最後にだけ付与）
        if (_plateObject.GetComponent<PlateCollider>() != null)
        {
            Destroy(_plateObject.GetComponent<PlateCollider>());
        }

        foreach (GameObject item in _itemList)
        {
            Destroy(item.GetComponent<PlateCollider>());
        }

        // 位置揺らし演出はコメントアウト
        //plateObject.transform.DOPunchPosition(new Vector3(0, 0.5f, 0), 0.5f, 3, 1f);
        ManualPopupUpdate();

        if (prefab.GetComponent<ItemCollision>().itemName == "バンズ")
        {
            ScoreCount();
            BurgerReset();
        }
    }
    private void ScoreCount()
    {
        List<IFood> foods = new List<IFood>();
        for (int i = 0; i < _itemList.Count; i++)
        {
            IFood food = _itemList[i].GetComponent<IFood>();
            foods.Add(food);
        }
        _scoreManager.ScoreCount(foods);
    }
    private void ManualPopupUpdate()
    {
        List<IFood> foods = new List<IFood>();
        for (int i = 0; i < _itemList.Count; i++)
        {
            IFood food = _itemList[i].GetComponent<IFood>();
            foods.Add(food);
        }
        _scoreManager.ComboChack(foods);
    }
}
