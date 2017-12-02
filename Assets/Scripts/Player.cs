using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [Header("Settings")]
    [SerializeField]
    float m_moveSpeed;
    [Header("References")]
    [SerializeField]
    Rigidbody2D m_rigidbody;

    void Awake()
    {
        instance = this;
        targetPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        MoveCamera();
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        m_rigidbody.AddForce(new Vector2(h * m_moveSpeed, v * m_moveSpeed));
    }

    Vector3 targetPos;

    void MoveCamera()
    {
        Vector3 p = Camera.main.WorldToViewportPoint(transform.position);
        if (p.x > 0.7)
        {
            targetPos += Vector3.right * Time.deltaTime * 5;
        }
        if (p.x < 0.3)
        {
            targetPos += Vector3.left * Time.deltaTime * 5;
        }
        if (p.y > 0.7)
        {
            targetPos += Vector3.up * Time.deltaTime * 5;
        }
        if (p.y < 0.3)
        {
            targetPos += Vector3.down * Time.deltaTime * 5;
        }
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, Time.deltaTime * 3);
    }
}
