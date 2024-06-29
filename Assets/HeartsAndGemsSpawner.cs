using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsAndGemsSpawner : MonoBehaviour
{
    public List<GameObject> hearts;
    public List<GameObject> gems;

    public SlimeHealth slimeHealth;

    public Vector3 instPos;
    public Vector2 instPosYRange;

    void Start(){
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){
        while(SlimeHealth.IsAlive){
            bool spawnHeart = Random.Range(0, 3) == 1 && slimeHealth.NeedMoreHearts();

            bool spawnGem = Random.Range(0, 2) == 0;

            if (spawnGem){
                var gemPrephab = gems[Random.Range(0, gems.Count)];

                int amountOfGems=Random.Range(2, 6);
                
                if (gemPrephab.gameObject.name.ToLower().Contains("x2")) {
                    InstantiateGems(gemPrephab, new Vector3(instPos.x, Random.Range(instPosYRange.x, instPosYRange.y), instPos.z));
                } else {
                    for(int i=-amountOfGems/2; i<=amountOfGems/2; i++){
                        InstantiateGems(gemPrephab, new Vector3(instPos.x + i, Random.Range(instPosYRange.x, instPosYRange.y), instPos.z));
                    }
                }
            }

            if (spawnHeart) {
                var heartPrephab = hearts[Random.Range(0, hearts.Count)];
                var heart = Instantiate(
                    heartPrephab,
                    new Vector3(instPos.x-3, Random.Range(instPosYRange.x, instPosYRange.y), instPos.z),
                    heartPrephab.transform.rotation
                ) as GameObject;

                heart.GetComponent<Heart>().health = slimeHealth;
            }

            yield return new WaitForSeconds(2f * Random.Range(0.3f, 1.2f));
        }
    }

    void InstantiateGems(GameObject gemPrephab, Vector3 position){
        var gem = Instantiate(
            gemPrephab,
            position,
            gemPrephab.transform.rotation
        ) as GameObject;
    }
}
