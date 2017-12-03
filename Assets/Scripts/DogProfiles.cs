using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogProfiles : MonoBehaviour
{
    public int index;

    public void ToggleContract()
    {
        ContractsUI.instance.ToggleDog(index);
    }
    // Use this for initialization
}
