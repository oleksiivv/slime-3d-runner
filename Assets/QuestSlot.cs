using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    public GameObject slot;

    public Animation animation;

    public Image icon;
    public Text name;
    public Text description;

    public void SetQuest(Quest quest){
        slot.gameObject.SetActive(true);
        animation.Play("ShowQuestSSlot");

        icon.sprite = quest.icon;
        name.text = quest.name;
        description.text = quest.description;
    }

    public void Hide(){
        animation.Play("HideQuestSlot");

        Invoke(nameof(SetQuestSlotActiveFalse), 1f);
    }

    void SetQuestSlotActiveFalse(){
        slot.gameObject.SetActive(false);
    }
}
