using System.Collections;
using System.Collections.Generic;
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
    }
}
