using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Paddle : MonoBehaviour
{
    private Rigidbody2D paddleRb;

    [Header("移動設定")]
    public float baseSpeed = 10f;
    public float minSpeed = 1f;
    public float boundaryX = 7.5f;

    [Header("サイズ設定")]
    [Tooltip("パドルの横幅（ローカルスケールのXに反映される）")]
    public float paddleWidth = 2f;

    [Tooltip("Height は保持される。必要なら別途公開して調整可能にしてください。")]
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
        // インスペクターで値を変えたときに即座に反映
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
        // パドルのスケールを横幅と高さに合わせる（Zはそのまま）
        Vector3 localScale = transform.localScale;
        localScale.x = paddleWidth;
        localScale.y = paddleHeight;
        transform.localScale = localScale;

        // もし BoxCollider2D があればサイズを合わせる（スケールとは別に設定されている場合）
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box != null)
        {
            // 基本的にサイズを 1,1 の状態からの比率で合わせたいなら以下を調整
            box.size = new Vector2(1f, 1f); // もし collider 自体のサイズを任意にしたければ別プロパティ化
        }
    }
}