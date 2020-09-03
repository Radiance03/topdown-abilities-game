using System;
using UnityEngine;

[Serializable]
public class Block 
{
    [SerializeField]
    private GameObject First,Second;

    public void Deactivate()
    {
        First.SetActive(false);
        Second.SetActive(false);
    }
    public void Activate()
    {
        First.SetActive(true);
        Second.SetActive(true);
    }
}
