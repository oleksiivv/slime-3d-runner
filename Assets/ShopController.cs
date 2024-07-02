using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public List<Image> items;

    public List<Image> padlocks;

    public List<Image> buyObjects;

    public List<Image> useObjects;

    public List<int> prices;

    public Color32 itemAvailableColor, itemChosenColor, itemUnavailableColor;

    public Text coinsAmount;

    public Animation panel;

    public QuestsController quests;

    public SlimeSoundEffects sound;

    public GameObject openButton;

    public GameObject notEnoughCoinsAlert;

    public void ShowPanel(){
        panel.Play("ShowShop");

        PlayerPrefs.SetInt("not_first_open", 2);
    }

    public void HidePanel(){
        panel.Play("HideShop");
    }

    void Awake(){
        //HidePanel();
    }

    public void HideNotEnoughCoinsAlert(){
        notEnoughCoinsAlert.SetActive(false);
    }

    void Start(){
        //PlayerPrefs.SetInt("coins", 10000);
        FillUI();

        if(PlayerPrefs.GetInt("not_first_open", 0) == 1){
            ShowPanel();
        } else if (PlayerPrefs.GetInt("not_first_open", 0) == 0){
            PlayerPrefs.SetInt("not_first_open", 1);
        }
    }

    public void Buy(int id){
        if (PlayerPrefs.GetInt("coins", 0) >= prices[id]) {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - prices[id]);
            
            PlayerPrefs.SetInt("SkinAvailable#"+id.ToString(), 1);

            Choose(id);

            FillUI();

            int coinsSpend = PlayerPrefs.GetInt("coins_spend", 0);
            coinsSpend += prices[id];
            PlayerPrefs.SetInt("coins_spend", coinsSpend);

            if(coinsSpend>=200)quests.CompleteQuest("spend_200", true);
        
            if(coinsSpend>=500)quests.CompleteQuest("spend_500", true);
            
            if(coinsSpend>=1000)quests.CompleteQuest("spend_1000", true);

            sound.PlayPickItemEffect1();
        }else{
            notEnoughCoinsAlert.SetActive(true);
        }
    }

    public void Choose(int id){
        if(PlayerPrefs.GetInt("SkinAvailable#"+id.ToString(), 0) == 1 || id==0){
            PlayerPrefs.SetInt("CurrentItem", id);

            FillUI();
            
            sound.PlayPickItemEffect2();
        }
    }
    
    public void FillUI(){
        for(int i=0; i<items.Count; i++){
            int currentItem = PlayerPrefs.GetInt("CurrentItem", 0);

            if (i == 0 || PlayerPrefs.GetInt("SkinAvailable#"+i.ToString(), 0) == 1){
                padlocks[i].gameObject.SetActive(false);
                buyObjects[i].gameObject.SetActive(false);

                if (currentItem==i) {
                    useObjects[i].gameObject.SetActive(false);
                    items[i].GetComponent<Image>().color = itemChosenColor;
                } else {
                    useObjects[i].gameObject.SetActive(true);
                    items[i].GetComponent<Image>().color = itemAvailableColor;
                }
            } else {
                padlocks[i].gameObject.SetActive(true);
                buyObjects[i].gameObject.SetActive(true);
                
                items[i].GetComponent<Image>().color = itemUnavailableColor;
            }
        }

        UpdateCoinsAmount();
    }

    void UpdateCoinsAmount(){
        coinsAmount.text = PlayerPrefs.GetInt("coins", 0).ToString();
    }
}
