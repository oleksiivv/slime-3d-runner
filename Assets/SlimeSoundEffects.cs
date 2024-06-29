using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSoundEffects : MonoBehaviour
{
    public AudioSource source;

    public AudioClip jumpEffect, pickItemEffect1, pickItemEffect2, pickItemEffect3;
    public AudioClip damageEffect, deathEffect;
    public AudioClip questCompletedEffect, newQuestAvailableEffect;

    public AudioClip newHiScoreEffect;

    void Start(){
        source.enabled=PlayerPrefs.GetInt("muted",0) == 0;
    }

    public void PlayJump(){
        source.clip = jumpEffect;
        source.Play();
    }

    public void PlayPickItemEffect1(){
        source.clip = pickItemEffect1;
        source.Play();
    }

    public void PlayPickItemEffect2(){
        source.clip = pickItemEffect2;
        source.Play();
    }

    public void PlayPickItemEffect3(){
        source.clip = pickItemEffect3;
        source.Play();
    }

    public void PlayDamage(){
        source.clip = damageEffect;
        source.Play();
    }

    public void PlayDeath(){
        source.clip = deathEffect;
        source.Play();
    }

    public void PlayQuesstCompleted(){
        source.clip = questCompletedEffect;
        source.Play();
    }

    public void PlayNewQuestAVailable(){
        source.clip = newQuestAvailableEffect;
        source.Play();
    }

    public void PlayNewHiScore(){
        source.clip = newHiScoreEffect;
        source.Play();
    }
}
