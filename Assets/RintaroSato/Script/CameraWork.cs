using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraWork : MonoBehaviour
{ 
    public PutPlate other; // �C���X�y�N�^����A�T�C��

    public float smoothSpeed = 5f;
    public float yOffsetThreshold = 0.5f;

    [Tooltip("�ړ��ɂ����鎞�ԁi�b�j")]
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
            Debug.LogError("Main Camera ��������܂���B�^�O�� 'MainCamera' �ɂȂ��Ă��邩�m�F���Ă��������B");
        }
    }

    void LateUpdate()
    {
        if (other == null)
        {
            Debug.LogWarning("other�����蓖�Ă��Ă��܂���");
            return;
        }

        var dynamicList = other.itemList;

        Debug.Log("�ǐՑΏې�: " + dynamicList.Count);

        if (dynamicList == null || dynamicList.Count == 0)
            return;

        float highestY = float.NegativeInfinity;

        foreach (var go in dynamicList)
        {
            if (go == null) continue;
            Debug.Log(go.name + " �� Y�ʒu: " + go.transform.position.y);
            highestY = Mathf.Max(highestY, go.transform.position.y);
        }

        if (highestY == float.NegativeInfinity)
            return;

        Vector3 camPos = _camera.transform.position;
        float targetY = camPos.y;

        Debug.Log($"�J����Y: {camPos.y}, �ō�Y: {highestY}, 臒l: {yOffsetThreshold}");

        if (highestY > camPos.y + yOffsetThreshold)
        {
            targetY = highestY - yOffsetThreshold;
            Debug.Log("�J�������グ�܂�: " + targetY);
        }
        else
        {
            Debug.Log("�J�����ړ��Ȃ�");
        }

        Vector3 desiredPos = new Vector3(camPos.x, targetY, camPos.z);
        _camera.transform.position = Vector3.Lerp(camPos, desiredPos, Time.deltaTime * smoothSpeed);
    }
}
