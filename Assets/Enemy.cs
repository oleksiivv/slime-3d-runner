using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rigidbody;

    [HideInInspector]
    public QuestsController quests;

    public static int numberOfDestroyedSpikesInRun = 0;

    public bool useGravity=true;

    private void Start(){
        if(!useGravity){
            return;
        }

        rigidbody=gameObject.GetComponent<Rigidbody>();
        if(!rigidbody.useGravity){
            rigidbody.AddForce(Vector3.up*1000); 
        }

        numberOfDestroyedSpikesInRun = 0;
        
        Invoke(nameof(CleanUp), 1f);
    }

    void CleanUp(){
        if(transform.parent==null){
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other){
        if(!useGravity){
            return;
        }
        
        if(other.gameObject.tag=="Ground" && gameObject.transform.parent==null){
            if(!other.gameObject.GetComponent<GroundMoveController>().IsInvisible){
                gameObject.transform.parent=other.gameObject.transform;
            }
        }
    }

    private void OnDestroy()
    {
        numberOfDestroyedSpikesInRun++;
        
        if(numberOfDestroyedSpikesInRun>=50){
            quests.CompleteQuest("obstacle_avoid");
        }
    }
}
