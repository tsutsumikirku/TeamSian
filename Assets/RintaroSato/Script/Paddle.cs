using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Paddle : MonoBehaviour
{
    private Rigidbody2D paddleRb;

    [Header("�ړ��ݒ�")]
    public float baseSpeed = 10f;
    public float minSpeed = 1f;
    public float boundaryX = 7.5f;

    [Header("�T�C�Y�ݒ�")]
    [Tooltip("�p�h���̉����i���[�J���X�P�[����X�ɔ��f�����j")]
    public float paddleWidth = 2f;

    public float paddleHeight = 1f;

    private float input;
    private float currentSpeed => Mathf.Max(minSpeed, baseSpeed);

    void Awake()
    {
        paddleRb = GetComponent<Rigidbody2D>();
        if (paddleRb != null)
        {
            paddleRb.gravityScale = 0f;
            paddleRb.freezeRotation = true;
            paddleRb.bodyType = RigidbodyType2D.Kinematic;
        }
        ApplySize();
    }

    void OnValidate()
    {
        // �C���X�y�N�^�[�Œl��ς����Ƃ��ɑ����ɔ��f
        ApplySize();
    }

    void Update()
    {
        input = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        float move = input * currentSpeed * Time.fixedDeltaTime;
        Vector3 newPos = transform.position + new Vector3(move, 0f, 0f);
        newPos.x = Mathf.Clamp(newPos.x, -boundaryX, boundaryX);

        if (paddleRb != null)
        {
            paddleRb.MovePosition(newPos);
        }
        else
        {
            transform.position = newPos;
        }
    }

    private void ApplySize()
    {
        // �p�h���̃X�P�[���������ƍ����ɍ��킹��iZ�͂��̂܂܁j
        Vector3 localScale = transform.localScale;
        localScale.x = paddleWidth;
        localScale.y = paddleHeight;
        transform.localScale = localScale;

        // ���� BoxCollider2D ������΃T�C�Y�����킹��i�X�P�[���Ƃ͕ʂɐݒ肳��Ă���ꍇ�j
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box != null)
        {
            // ��{�I�ɃT�C�Y�� 1,1 �̏�Ԃ���̔䗦�ō��킹�����Ȃ�ȉ��𒲐�
            box.size = new Vector2(1f, 1f); // ���� collider ���̂̃T�C�Y��C�ӂɂ�������Εʃv���p�e�B��
        }
    }
}