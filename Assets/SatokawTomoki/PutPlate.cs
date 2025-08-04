using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPlate : MonoBehaviour, IDish
{
    [SerializeField][Header("お皿のオブジェクト")] private GameObject plateObject;
    [SerializeField][Header("最初の高さ")] private float putTopPosition;
    [SerializeField][Header("オブジェクトの間")] private float interval;
    private List<GameObject> itemList = new List<GameObject>();

    public void Get(GameObject prefab, Sprite image)
    {
        GameObject newObject = Instantiate(prefab);
        if (newObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(newObject.GetComponent<Rigidbody>());
        }
        if (newObject.GetComponent<ItemControl>() != null)
        {
            Destroy(newObject.GetComponent<ItemControl>());
        }
        Vector3 position = Vector3.zero;
        position.x = 0;
        float positionY = putTopPosition;
        foreach (GameObject item in itemList)
        {
            positionY += GetComponent<SpriteRenderer>().bounds.size.y /interval;
        }
        positionY += GetComponent<SpriteRenderer>().bounds.size.y /2;
        if (image != null)
        {
            newObject.GetComponent<SpriteRenderer>().sprite = image;
        }
        position.y = positionY;
        newObject.transform.parent = plateObject.transform;
        newObject.transform.localPosition = position;
        newObject.GetComponent<Collider2D>().isTrigger = true;
        itemList.Add( newObject );
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
