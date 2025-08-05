using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] Data data;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Data Name: " + data.Name);
    }
}
