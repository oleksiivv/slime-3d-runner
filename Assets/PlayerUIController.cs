using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : BaseUIController
{
    public GameObject winPanel, losePanel, pausePanel;

    public SlimeSoundEffects sound;

    //RUN RESULTS
    public GameObject hasNewHiScore;
    public Text currentQuests;
    public Text totalQuests;

    void Start(){
        Time.timeScale=1;
    }

    public void Pause(){
        Time.timeScale=0;
        pausePanel.SetActive(true);

        sound.PlayJump();
    }

    public void Resume(){
        Time.timeScale=1;
        pausePanel.SetActive(false);

        sound.PlayPickItemEffect3();
    }

    public void Restart(){
        OpenScene(Application.loadedLevel);

        sound.PlayPickItemEffect3();
    }

    public void SetWinPanelVisibility(bool visible){
        winPanel.SetActive(visible);

        sound.PlayNewHiScore();
    }

    public void SetLosePanelVisibility(bool visible){
        SetWinPanelVisibility(false);
        losePanel.SetActive(visible);
    }
}
