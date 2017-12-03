using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer m_sr;
    bool fade = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" || col.tag == "Dog")
        {
            fade = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" || col.tag == "Dog")
        {
            fade = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player" || col.tag == "Dog")
        {
            fade = false;
        }
    }

    void Start()
    {
        //transform.rotation = Quaternion.Euler(0, 0, Random.Range(180, -180));
        //transform.localScale = Vector3.one * Random.Range(.8f, 2.5f);
        //transform.position += Vector3.forward * 10;
        //this.enabled = false;
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //  Instantiate(transform.parent.gameObject, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.Euler(0, 0, Random.Range(180, -180)), transform.parent.parent);
        m_sr.color = new Color(1, 1, 1, Mathf.Clamp((fade ? m_sr.color.a - Time.deltaTime : m_sr.color.a + Time.deltaTime), 0.5f, 1));
    }
}
