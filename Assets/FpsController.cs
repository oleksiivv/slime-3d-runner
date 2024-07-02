using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    //public Text fpsCount;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //fpsCount.GetComponent<Text>().text = "FPS: " + ((int)(1f / Time.unscaledDeltaTime)).ToString() ;
    }
}
