using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject soundOn;
    public GameObject soundOff;

    //public GameObject undoPanel;

    public GameObject[] audioHolders;

    public Animation panel;

    public void ShowPanel(){
        if(panel.transform.localScale.y == 0){
            panel.Play("ShowSettings");
        }
    }

    public void HidePanel(){
        panel.Play("HideSettings");
    }

    void Start(){
        setAudioPlayable();
    }
    // public void showUndoPanel(){
    //     undoPanel.SetActive(true);
    // }

    // public void closeUndoPanel(){
    //     undoPanel.SetActive(false);
    // }


    // public void undoProgress(){
        
    //     PlayerPrefs.DeleteAll();
    //     closeUndoPanel();
    //     Application.LoadLevel(Application.loadedLevel);

    // }

    public void mute(){
        PlayerPrefs.SetInt("muted",1);
        setAudioPlayable();
    }

    public void unmute(){
        PlayerPrefs.SetInt("muted",0);
        setAudioPlayable();
    }

    public void rate(){
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.EasyStreet.SlimeRun");
    }

    public void info(){
        Application.OpenURL("https://vertexstudio.herokuapp.com/");
    }

    public void setAudioPlayable(){
        int muted=PlayerPrefs.GetInt("muted",0);
        if(muted==0){
            foreach(var holder in audioHolders){
                holder.GetComponent<AudioSource>().enabled=true;
            }
            soundOn.SetActive(!false);
            soundOff.SetActive(!true);
        }
        else{
            foreach(var holder in audioHolders){
                holder.GetComponent<AudioSource>().enabled=false;
            }
            soundOn.SetActive(!true);
            soundOff.SetActive(!false);
        }
    }
}

