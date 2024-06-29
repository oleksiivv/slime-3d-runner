using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public Text scoreText;
    public Text hiScoreText;

    public GameObject hiScorePanel;

    private int score = 0;

    public int scoreIncrementStep = 1;

    private bool hasNewHiScore=false;

    private bool isFirstTry=false;

    private int x=1;

    public GameObject x2;
    public Text x2Label;

    public QuestsController quests;

    public SlimeHealth slime;

    public Text totalDistanceLabel;

    public bool HasNewHiScore{
        get{
            return hasNewHiScore;
        }
    }

    void Start(){
        score=0;

        if(scoreIncrementStep<=0){
            scoreIncrementStep=1;
        }

        hasNewHiScore=false;

        isFirstTry = PlayerPrefs.GetInt("hi_score", 0) == 0;

        if(isFirstTry){
            hiScorePanel.gameObject.SetActive(false);
        } else{
            hiScoreText.text = PlayerPrefs.GetInt("hi_score", 0).ToString();
            hiScorePanel.gameObject.SetActive(true);
        }

        StartCoroutine(ScoreIncrement());

        CancelInvoke(nameof(On5MinutesPassed));
        Invoke(nameof(On5MinutesPassed), 300);
    }

    void On5MinutesPassed(){
        if(SlimeHealth.IsAlive && score>100){
            quests.CompleteQuest("survive_5m");
        }
    }

    public void X2(){
        x *= 2;

        Invoke(nameof(ResetX), 6f);

        if(x>1) {
            x2.SetActive(true);
            x2Label.text = "x"+x.ToString();
        }

        //QUESTS ONLY
        quests.CompleteQuest("combo_2x");

        if(x==4){
            quests.CompleteQuest("combo_4x");
        }else if(x==8){
            quests.CompleteQuest("combo_8x");
        }
    }

    void ResetX(){
        if(x > 1) x /= 2;

        if(x <= 1) x2.SetActive(false);
        else x2Label.text = "x"+x.ToString();
    }

    IEnumerator ScoreIncrement(){
        while(SlimeHealth.IsAlive){
            score += scoreIncrementStep * x;

            scoreText.text = score.ToString();

            if(score > PlayerPrefs.GetInt("hi_score", 0)){
                hiScorePanel.gameObject.SetActive(false);
                
                PlayerPrefs.SetInt("hi_score", score);

                if(!isFirstTry){
                    hasNewHiScore = true;
                }
            }

            if(score>=1000 && slime.avoidedSpike){
                quests.CompleteQuest("run_without_hit_1000");
            }

            int totalScore = PlayerPrefs.GetInt("total_distance", 0);
            totalScore += scoreIncrementStep * x;
            PlayerPrefs.SetInt("total_distance", totalScore);
            totalDistanceLabel.text = totalScore.ToString();

            if(totalScore >= 10000){
                quests.CompleteQuest("distance_runner");
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ShowResults(PlayerUIController ui){
        if(HasNewHiScore){
            ui.hasNewHiScore.gameObject.SetActive(true);
        }else{
            ui.hasNewHiScore.gameObject.SetActive(false);
        }

        if (quests.GetCompletedQuestsInCurrentRunAmount() > 0){
            ui.currentQuests.gameObject.SetActive(true);
            ui.currentQuests.text = "+"+quests.GetCompletedQuestsInCurrentRunAmount().ToString() + " completed quests";
        } else {
            ui.currentQuests.gameObject.SetActive(false);
        }

        ui.totalQuests.text = quests.GetCompletedQuestsAmount().ToString() + "/" + quests.totalQuestsAmount + " completed quests";// in total";
    }

    public bool IsHardMode(){
        return score > 100;
    }
}
