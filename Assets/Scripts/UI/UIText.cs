using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIText : MonoBehaviour
{
    public static UIText instance;
    [SerializeField]
    UnityEngine.UI.Text m_prefab;

    void Awake()
    {
        instance = this;
    }

    public  UnityEngine.UI.Text DisplayText(string message, Vector3 pos)
    {
        UnityEngine.UI.Text t = Instantiate(m_prefab, pos, Quaternion.Euler(0, 0, 0), transform);
        t.rectTransform.anchoredPosition = pos / 4;
        t.text = message;
        return t;
    }
}
