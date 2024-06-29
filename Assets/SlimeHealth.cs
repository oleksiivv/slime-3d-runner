using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeHealth : MonoBehaviour
{
    public static bool IsAlive=true;

    public List<Image> hearts;

    private int heartsCnt=3;

    public PlayerUIController ui;

    public SlimeAnimator animator;

    public BoxCollider boxCollider;

    public PlayerScore score;

    public ParticleSystem heartPickUpEffect, gemPickUpEffect, x2PickUpEffect;

    public Text coinsText;

    public QuestsController quests;

    public SlimeSoundEffects sound;

    //QUEST ONLY
    private bool HadOneHeart=false;

    void Awake(){
        IsAlive=true;
        heartsCnt=3;

        ShowHearts(3);

        coinsText.text = PlayerPrefs.GetInt("coins", 0).ToString();
    }

    public bool NeedMoreHearts(){
        return heartsCnt<3;
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag.ToLower().Equals("enemy") && SlimeHealth.IsAlive){
            if(other.gameObject.GetComponent<NonGravityEnemy>()){
                //other.gameObject.transform.parent=null;
                //other.gameObject.GetComponent<Rigidbody>().useGravity=true;
            }

            Handheld.Vibrate();

            heartsCnt--;
            boxCollider.isTrigger=true;

            if (heartsCnt == 0) {
                Death();

                animator.Death();
                sound.PlayDeath();

                if(transform.position.y >= -1.3f){
                    GetComponent<Rigidbody>().useGravity=true;
                    GetComponent<Rigidbody>().isKinematic=false;
                } 
                
            } else {
                animator.Damage();
                sound.PlayDamage();
            }

            ShowHearts(heartsCnt);

            avoidedSpike=false;

            if(!IsInvoking(nameof(ResetCollider))){
                Invoke(nameof(ResetCollider), 1f);
            }
        }
    }

    public void HandleExternalDamage(){
        if(SlimeHealth.IsAlive){
            heartsCnt=0;

            boxCollider.isTrigger=true;

            Death();

            animator.Death();
            sound.PlayDeath();

            ShowHearts(heartsCnt);

            avoidedSpike=false;

            GetComponent<Rigidbody>().useGravity=true;
            GetComponent<Rigidbody>().isKinematic=false;            
        }
    }

    //QUESTS ONLY
    private int gemsInSingleRun=0;
    private int heartsInSingleRun=0;
    private int xBoostsInSingleRun=0;
    [HideInInspector]
    public bool avoidedSpike=true;

    void OnTriggerExit(Collider other){
        if(other.gameObject.tag.ToLower().Equals("enemy") && SlimeHealth.IsAlive){ 
            boxCollider.isTrigger=false;
            animator.Run();
        }
    }

    void ResetCollider(){
        boxCollider.isTrigger=false;
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.tag.ToLower().Equals("enemy") && SlimeHealth.IsAlive){
            if(other.gameObject.GetComponent<NonGravityEnemy>()){
                boxCollider.isTrigger=false;
                
                other.gameObject.GetComponent<NonGravityEnemy>().enabled=false;
                other.gameObject.GetComponent<BoxCollider>().enabled=false;
            }

            animator.Run();
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag.ToLower().Equals("heart") && !other.gameObject.name.ToLower().Contains("container")){
            if(heartsCnt<3){
                heartsCnt++;
            }

            ShowHearts(heartsCnt);
            //Destroy(other.gameObject.transform.parent.gameObject);

            heartPickUpEffect.Play();
            sound.PlayPickItemEffect1();

            other.gameObject.transform.parent.gameObject.GetComponent<Heart>().HandleDestroy();

            heartsInSingleRun++;
            if(heartsInSingleRun==10){
                quests.CompleteQuest("health_10");
            }
        }
        else if(other.gameObject.tag.ToLower().Equals("gem") && !other.gameObject.name.ToLower().Contains("container")){
            //Destroy(other.gameObject.transform.parent.gameObject);

            other.gameObject.transform.parent.gameObject.GetComponent<Gem>().HandleDestroy();

            gemPickUpEffect.Play();
            sound.PlayPickItemEffect2();

            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + 1);

            coinsText.text = PlayerPrefs.GetInt("coins", 0).ToString();

            gemsInSingleRun += 1;
            if(gemsInSingleRun>=100){
                quests.CompleteQuest("gem_collector");
            }

            if(PlayerPrefs.GetInt("coins", 0) >= 500){
                quests.CompleteQuest("gems_holder");
            }
        }
        else if(other.gameObject.tag.ToLower().Equals("x2") && !other.gameObject.name.ToLower().Contains("container")){
            //Destroy(other.gameObject.transform.parent.gameObject);
            other.gameObject.transform.parent.gameObject.GetComponent<Gem>().HandleDestroy();

            x2PickUpEffect.Play();
            sound.PlayPickItemEffect3();

            score.X2();

            xBoostsInSingleRun++;
            if(xBoostsInSingleRun == 3){
                quests.CompleteQuest("speed_deamon");
            }
        }
    }

    void Death(){
        IsAlive=false;
        
        if(score.HasNewHiScore){
            ui.SetWinPanelVisibility(true);
        }else{
            ui.SetLosePanelVisibility(true);
        }

        score.ShowResults(ui);
    }

    void ShowHearts(int number){
        // for(int i=2; i>0; i--){
        //     hearts[i].gameObject.SetActive(true);
        // }

        for(int i=2; i>=0; i--){
            //hearts[i].gameObject.SetActive(false);
                
            if(i <= number-1){
                if(hearts[i].color.a==0){
                    hearts[i].gameObject.GetComponent<Animation>().Play("HeartShow");
                }

                continue;
                //hearts[i].gameObject.GetComponent<Animation>().Play("HeartHide");
            }

            if(hearts[i].color.a != 0){
                hearts[i].gameObject.GetComponent<Animation>().Play("HeartHide");
            }
        }

        if(number==1){
            HadOneHeart=true;
        }
        if(number==3 && HadOneHeart){
            quests.CompleteQuest("heart_warrior");
        }
    }

    public bool CanRun(){
        return !boxCollider.isTrigger;
    }
}
