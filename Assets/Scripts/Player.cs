using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [Header("Settings")]
    [SerializeField]
    float m_moveSpeed;
    public static float m_leadLength = 2;
    [Header("References")]
    [SerializeField]
    Rigidbody2D m_rigidbody;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        targetPos = transform.position + Vector3.back * 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.instance.m_currentState == GameStateManager.GameSate.Gameplay)
        {
            Movement();
            MoveCamera();
        }
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_moveSpeed *= 3;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_moveSpeed /= 3;
        }

        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0)
        {
            m_leadLength = Mathf.Clamp(m_leadLength + (Input.mouseScrollDelta.y * Time.deltaTime * 10), 1f, 3.5f);
        }

        m_rigidbody.AddForce(new Vector2(h * m_moveSpeed, v * m_moveSpeed));

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z - 90);
    }

    Vector3 targetPos;

    void MoveCamera()
    {
        Vector3 p = Camera.main.WorldToViewportPoint(transform.position);
        if (p.x > 0.6)
        {
            targetPos += Vector3.right * Time.deltaTime * 5;
        }
        if (p.x < 0.4)
        {
            targetPos += Vector3.left * Time.deltaTime * 5;
        }
        if (p.y > 0.6)
        {
            targetPos += Vector3.up * Time.deltaTime * 5;
        }
        if (p.y < 0.4)
        {
            targetPos += Vector3.down * Time.deltaTime * 5;
        }
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, Time.deltaTime * 3);
    }
}
