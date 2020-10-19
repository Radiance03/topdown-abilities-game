using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{


    public int LevelUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        LevelUnlocked = PlayerPrefs.GetInt("UnlockedLevel");

    }

  

    // Update is called once per frame
    void Update()
    {
        
    }
}
