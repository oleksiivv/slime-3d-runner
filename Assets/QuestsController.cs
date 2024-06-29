using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsController : MonoBehaviour
{
    public QuestSlot slot;
    public List<Quest> quests;

    private int currentQuestId;
    public int totalQuestsAmount;

    public GameObject questCompletedAlert;

    public SlimeSoundEffects sound;

    private int questsInCurrentRun=0;

    void Start(){
        currentQuestId = PlayerPrefs.GetInt("current_quest_id", 0);

        if(currentQuestId <= totalQuestsAmount){
            Invoke(nameof(ShowQuest), 2f);
        }
    }

    public void ShowQuest(){
        if(currentQuestId <= totalQuestsAmount && currentQuestId<quests.Count && slot){
            slot.SetQuest(quests[currentQuestId]);

            sound.PlayNewQuestAVailable();
        }
    }

    public void CompleteCurrentQuest(bool isBackgrounQuest=false){
        currentQuestId++;
        PlayerPrefs.SetInt("current_quest_id", currentQuestId);

        Debug.Log("Quest: "+currentQuestId.ToString());

        questsInCurrentRun++;
        
        if(isBackgrounQuest){
            return;
        }

        if (IsInvoking(nameof(HideQuestPanel))){
            return;
        }

        questCompletedAlert.gameObject.SetActive(true);

        Invoke(nameof(HideQuestPanel), 3f);
        
        Invoke(nameof(HideQuestCompletedAlert), 3f);

        Invoke(nameof(ShowQuest), 5f);

        sound.PlayQuesstCompleted();
    }

    void HideQuestPanel(){
        slot.Hide();
    }

    void HideQuestCompletedAlert(){
        questCompletedAlert.GetComponent<Animation>().Play("HideQuestSlot");
        Invoke(nameof(SetQuestCompletedAlertActiveFalse), 1.2f);
    }

    void SetQuestCompletedAlertActiveFalse(){
        questCompletedAlert.SetActive(false);
    }

    public void CompleteQuest(string code, bool isBackgrounQuest=false){
        if(PlayerPrefs.GetInt("quest_completed_"+code, 0) == 1){
            return;
        }

        bool canBeCompleted=false;
        int questId=0;

        foreach(var quest in quests){
            if(quest.code == code && questId == currentQuestId){
                canBeCompleted=true;
            }

            questId++;
        }

        if(canBeCompleted){
            Debug.Log("can be completed - "+code);
            CompleteCurrentQuest(isBackgrounQuest);
            PlayerPrefs.SetInt("quest_completed_"+code, 1);
        }
    }

    public int GetCompletedQuestsAmount(){
        return currentQuestId;
    }

    public int GetCompletedQuestsInCurrentRunAmount(){
        return questsInCurrentRun;
    }
}
