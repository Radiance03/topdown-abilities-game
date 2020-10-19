using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManageScenes : MonoBehaviour
{
    public GameObject mapManager;

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void CheckAndLoadLevel( int LevelNumber)
    {


        if (mapManager.GetComponent<MapManager>().LevelUnlocked >= LevelNumber)
        {
            LoadScene("Level" + LevelNumber.ToString());

        }
        else
        {

        }


    }

}
