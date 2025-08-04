using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    [SerializeField][Header("�A�C�e���̖��O")]  private string itemName;
    [SerializeField][Header("�A�C�e���̃v���n�u")] private GameObject itemPrefab;
    [SerializeField] private Sprite itemImage;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.gameObject;
        if (hitObject.GetComponent<IDish>() != null)
        {
            hitObject.GetComponent<IDish>().Get(itemPrefab,itemImage);
            Destroy(this.gameObject);
        }
    }

}
public interface IDish
{
    public void Get(GameObject prefab,Sprite image = null);
}
