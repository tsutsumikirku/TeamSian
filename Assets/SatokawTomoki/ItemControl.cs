using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    //アイテム個々につけるスクリプト
    [HideInInspector]public float moveSpeed = 0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = this.transform.position;
        position.y -= moveSpeed * Time.deltaTime;
        this.transform.position = position;
        if(Camera.main.gameObject.transform.position.y - 13 > this.transform.position.y)
        {
            Destroy(this.gameObject);
        }
    }
}
