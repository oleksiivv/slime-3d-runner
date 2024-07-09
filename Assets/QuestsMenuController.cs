using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestsMenuController : MonoBehaviour
{
    public List<Quest> quests;
    public List<Image> questSlots;
    public List<Text> questSlotNames, questSlotNamesBg;
    public List<Text> questSlotDesc, questSlotDescBg;
    public List<Image> questSlotImages;
    public List<Text> questSlotStatuses, questSlotStatusesBg;

    public Animator openQuestsButtonAnimator;

    void Start(){
        for(int i=0; i<questSlotNamesBg.Count; i++){
            questSlotNames.Add(questSlotNamesBg[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Text>());
        }

        for(int i=0; i<questSlotDescBg.Count; i++){
            questSlotDesc.Add(questSlotDescBg[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Text>());
        }

        for(int i=0; i<questSlotStatusesBg.Count; i++){
            questSlotStatuses.Add(questSlotStatusesBg[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Text>());
        }

        if(PlayerPrefs.GetInt("not_first_quests_open", 0) == 1){
            openQuestsButtonAnimator.enabled=true;
        } else if (PlayerPrefs.GetInt("not_first_quests_open", 0) == 0){
            PlayerPrefs.SetInt("not_first_quests_open", 1);

            openQuestsButtonAnimator.enabled=false;
        }else{
            openQuestsButtonAnimator.enabled=false;
        }
    }

     public Animation panel;

    public void ShowPanel(){
        Init();
        panel.Play("ShowShop");

        openQuestsButtonAnimator.enabled=false;
        PlayerPrefs.SetInt("not_first_quests_open", 2);
    }

    public void HidePanel(){
        panel.Play("HideShop");
    }

    public void Init(){
        var currentQuest = PlayerPrefs.GetInt("current_quest_id", 0);
        int amount = questSlots.Count;
        int startFrom=1;

        if (currentQuest>=quests.Count) {
            startFrom=0;
        } else {
            questSlots[0].gameObject.SetActive(true);

            questSlotNames[0].text = quests[currentQuest].name;
            questSlotNamesBg[0].text = quests[currentQuest].name;

            questSlotDesc[0].text = quests[currentQuest].description;
            questSlotDescBg[0].text = quests[currentQuest].description;

            questSlotStatuses[0].text = "Current";
            questSlotStatusesBg[0].text = "Current";

            questSlots[0].color = new Color32(150, 255, 140, 120);
            questSlotImages[0].sprite = quests[currentQuest].icon;
         
            // if (currentQuest!=0 && currentQuest<quests.Count-1){
            //     questSlots[currentQuest+1].gameObject.SetActive(true);

            //     questSlotNames[currentQuest+1].text = "Unavailable yet";
            //     questSlotNamesBg[currentQuest+1].text = "Unavailable yet";

            //     questSlotDesc[currentQuest+1].text = "Complete previous quest to open this one!";
            //     questSlotDescBg[currentQuest+1].text = "Complete previous quest to open this one!";

            //     questSlotStatuses[currentQuest+1].text = "";
            //     questSlotStatusesBg[currentQuest+1].text = "";

            //     questSlots[currentQuest+1].color = new Color32(255, 255, 255, 120);
            //     questSlotImages[currentQuest+1].gameObject.SetActive(false);
            // }
        }

        for(int i=startFrom; i<amount; i++){
            if(i<=currentQuest){
                questSlots[i].gameObject.SetActive(true);
                
                questSlotNames[i].text = quests[i-startFrom].name;
                questSlotNamesBg[i].text = quests[i-startFrom].name;
                
                questSlotDesc[i].text = quests[i-startFrom].description;
                questSlotDescBg[i].text = quests[i-startFrom].description;
                
                questSlotImages[i].sprite = quests[i-startFrom].icon;
                
                questSlotStatuses[i].text = "Completed";
                questSlotStatusesBg[i].text = "Completed";

                questSlots[i].color = new Color32(150, 255, 140, 0);
            }else if(i>currentQuest){
                questSlots[i].gameObject.SetActive(true);

                //questSlotNames[i].text = "Unavailable yet";
                //questSlotNamesBg[i].text = "Unavailable yet";

                questSlotNames[i].text = quests[i-startFrom+1].name;
                questSlotNamesBg[i].text = quests[i-startFrom+1].name;

                //questSlotDesc[i].text = "Complete previous quest to open this one!";
                //questSlotDescBg[i].text = "Complete previous quest to open this one!";

                questSlotDesc[i].text = quests[i-startFrom+1].description;
                questSlotDescBg[i].text = quests[i-startFrom+1].description;
                
                questSlotImages[i].sprite = quests[i-startFrom+1].icon;

                questSlotStatuses[i].text = "Unavailable yet";
                questSlotStatusesBg[i].text = "Unavailable yet";

                questSlots[i].color = new Color32(255, 255, 255, 120);
                //questSlotImages[i].gameObject.SetActive(false);
            }
        }
    }
}
