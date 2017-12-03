using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogHUD : MonoBehaviour
{
    [SerializeField]
    Text name;
    [SerializeField]
    Image bar;
    float max;

    public void Init(string n, float f)
    {
        name.text = n;
        bar.fillAmount = 0;
        max = f;
    }

    public void UpdateBar(float f)
    {
        bar.fillAmount = f;
    }
}
