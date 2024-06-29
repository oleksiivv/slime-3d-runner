using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : BaseUIController
{
    public Text coinsText;
    public Text hiText;

    public GameObject coinsPanel, hiPanel;

    void Start(){
        var coins = PlayerPrefs.GetInt("coins", 0);
        var hi = PlayerPrefs.GetInt("hi_score", 0);

        coinsText.text = coins.ToString();
        hiText.text = hi.ToString();

        coinsPanel.SetActive(coins>0);
        hiPanel.SetActive(hi>0);
    }
}
