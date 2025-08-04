using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCollider : MonoBehaviour,IDish
{
    public void Get(GameObject prefab, Sprite image = null)
    {
        FindAnyObjectByType<PutPlate>().Put(prefab, image);
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
