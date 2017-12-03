using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endgame : MonoBehaviour
{
    bool allowEnd;
    int totalInZone;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Dog")
        {
            if (c.GetComponent<Dog>().m_hud.done)
            {
                totalInZone++;
                if (totalInZone == Dog.total)
                {
                    Debug.Log("done");
                    allowEnd = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Dog")
        {
            if (c.GetComponent<Dog>().m_hud.done)
            {
                totalInZone--;
                if (totalInZone < 0)
                    totalInZone = 0;
            }
        }
    }
}
