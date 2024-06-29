using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIController : MonoBehaviour
{
    public GameObject loadingPanel;

    public void OpenScene(int sceneId){
        Time.timeScale=1;

        loadingPanel.SetActive(true);
        
        Application.LoadLevelAsync(sceneId);
    }
}
