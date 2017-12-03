using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogProfile : MonoBehaviour
{
    public int index;
    [SerializeField]
    Text t_name, t_stats;
    [SerializeField]
    Image i_pic;

    public void Init(ContractsUI.DogContract contract, int i)
    {
        index = i;
        t_name.text = "\"" + contract.name + "\"";
        t_stats.text = "<b>" + contract.tagline + "</b>" + "\n" +
        "<color=#FF004D>£" + contract.pay + "/hr</color>\n" +
        "<color=#29ADFF>" + contract.minDistance + "m min</color>";
        i_pic.sprite = contract.m_pic;
    }

    public void ToggleContract()
    {
        ContractsUI.instance.ToggleDog(index);
    }
    // Use this for initialization
}
