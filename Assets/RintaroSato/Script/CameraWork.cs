using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraWork : MonoBehaviour
{ 
    public PutPlate other; // インスペクタからアサイン

    public float smoothSpeed = 5f;
    public float yOffsetThreshold = 0.5f;

    [Tooltip("移動にかかる時間（秒）")]
    [SerializeField] private float duration = 1f;
    private Camera _camera;
    public void CameraReset()
    {
        _camera.gameObject.transform.DOMoveY(1,1f);
    }
    void Start()
    {
        _camera = Camera.main;
        if (_camera == null)
        {
            Debug.LogError("Main Camera が見つかりません。タグが 'MainCamera' になっているか確認してください。");
        }
    }

    void LateUpdate()
    {
        if (other == null)
        {
            Debug.LogWarning("otherが割り当てられていません");
            return;
        }

        var dynamicList = other.itemList;

        Debug.Log("追跡対象数: " + dynamicList.Count);

        if (dynamicList == null || dynamicList.Count == 0)
            return;

        float highestY = float.NegativeInfinity;

        foreach (var go in dynamicList)
        {
            if (go == null) continue;
            Debug.Log(go.name + " の Y位置: " + go.transform.position.y);
            highestY = Mathf.Max(highestY, go.transform.position.y);
        }

        if (highestY == float.NegativeInfinity)
            return;

        Vector3 camPos = _camera.transform.position;
        float targetY = camPos.y;

        Debug.Log($"カメラY: {camPos.y}, 最高Y: {highestY}, 閾値: {yOffsetThreshold}");

        if (highestY > camPos.y + yOffsetThreshold)
        {
            targetY = highestY - yOffsetThreshold;
            Debug.Log("カメラを上げます: " + targetY);
        }
        else
        {
            Debug.Log("カメラ移動なし");
        }

        Vector3 desiredPos = new Vector3(camPos.x, targetY, camPos.z);
        _camera.transform.position = Vector3.Lerp(camPos, desiredPos, Time.deltaTime * smoothSpeed);
    }
}
