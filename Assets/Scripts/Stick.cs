using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(180, -180));
    }

    UnityEngine.UI.Text t;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            t = UIText.instance.DisplayText("Pick up <color=#AB5236>stick</color>", Camera.main.WorldToScreenPoint(transform.position));
        }
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            t.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(transform.position) * 2;
            if (Input.GetKeyDown(KeyCode.E))
            {
                //pick up stick
            }
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Player")
            Destroy(t.gameObject);
    }
}
