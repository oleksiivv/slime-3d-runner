using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    public List<GameObject> skins;

    public int startFrom=1;

    void Start()
    {
        int currentSkin = PlayerPrefs.GetInt("CurrentItem", 0);

        foreach(var skin in skins){
            skin.SetActive(false);
        }

        if(currentSkin>=startFrom){
            skins[currentSkin-1].SetActive(true);
        }
    }
}
