using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [Header("Settings")]
    public bool isAGoodBoy = true;
    public AudioClip m_barkSound;
    public float m_attentionSpan;
    public float m_alertRadius;
    public string m_name;
    [Header("References")]
    public LineRenderer m_lead;

    void Update()
    {
        Vector3[] newPos =
            {
                transform.position,
                Player.instance.transform.position
            };
        m_lead.SetPositions(newPos);
    }
}
