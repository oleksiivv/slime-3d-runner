using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesGenerator : MonoBehaviour
{
    
    public GameObject[] enemies;
    public Vector3 instPos;

    public float delay;

    public QuestsController quests;

    // public Text difficulty;
    // public Image[] stars;
    // public Color32 godStarColor;
    // private int level;

    void Start(){
        GroundMoveController.speed=1f;
        StartCoroutine(generator());

        //difficulty.text="Easy";
        //showStars(1);
        //level=1;
    }

    string prevEnemyName="";
    IEnumerator generator(){
        while(SlimeHealth.IsAlive){
            int n=Random.Range(0,enemies.Length);
            var newEnemy = Instantiate(enemies[n],new Vector3(instPos.x,enemies[n].transform.position.y,instPos.z),enemies[n].transform.rotation) as GameObject;
            
            newEnemy.GetComponent<Enemy>().quests = quests;
            
            yield return new WaitForSeconds(Random.Range(delay/2.5f,delay*1.5f));

            if(delay>1)delay-= 0.01f;

            Debug.Log(delay);

            prevEnemyName=enemies[n].gameObject.name;
            
            
            // if(delay>0.5f)delay*=0.965f;
            // Debug.Log("Delay: "+delay.ToString()+" Speed: "+(GroundMoveController.speed).ToString());

            // if(delay>1.8f && delay < 2.5f && level<2){
            //     difficulty.text="Medium";
            //     level=2;
            //     showStars(2);
            // }
            // else if(delay>0.8f && delay<=1.8f && level<3){
            //     difficulty.text="Pro";
            //     level=3;
            //     showStars(3);
            // }
            // else if(delay<=0.8f && level<4){
            //     difficulty.text="GOD!";
            //     level=4;
            //     godLevel();
            // }
        }
    }

    // public void hideAllStars(){
    //     foreach(var star in stars)star.gameObject.SetActive(false);
    // }
    // public void showAllStars(){
    //     foreach(var star in stars)star.gameObject.SetActive(false);
    // }

    // public void showStars(int n){
    //     hideAllStars();

    //     for(int i=0;i<n;i++){
    //         stars[i].gameObject.SetActive(true);
    //     }
    // }

    // public void godLevel(){
    //     foreach(var star in stars){
    //         star.color=godStarColor;
    //     }
    // }
}
