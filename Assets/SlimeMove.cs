using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : MonoBehaviour
{
    public float topY, bottomY;

    private int direction=1;

    public float speed=1f;

    public ParticleSystem landEffect;

    public SlimeAnimator animator;

    public SlimeHealth slimeHealth;

    bool playedEffect=false;
    bool rotating=true;

    public QuestsController quests;

    public SlimeSoundEffects sound;

    void Update(){
        if(!SlimeHealth.IsAlive)return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(transform.position.x, direction > 0 ? bottomY : topY, transform.position.z),
            0.1f * Time.timeScale * speed
        );

        if((transform.position.y == bottomY || transform.position.y == topY)){
            
            if(!playedEffect) {
                landEffect.Play();
                playedEffect=true;
            }

            transform.eulerAngles = new Vector3(direction == 1 ? 0 : -180, 0, 0);

            if(slimeHealth.CanRun())animator.Run();
        }

        if (rotationProgress < 1 && rotationProgress >= 0){
            rotationProgress += Time.deltaTime * 4.5f;

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress);
        }
    }

    Quaternion startRotation;
    Quaternion endRotation;
    float rotationProgress = -1;

    void StartRotating(float xPosition){

        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(xPosition, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        rotationProgress = 0;
    }

    //QUEST ONLY
    private int jumps=0;

    public void ChangeDirection(){
        if(!SlimeHealth.IsAlive)return;
        
        landEffect.Play();

        direction *= -1;
        playedEffect=false;
        rotating=true;

        StartRotating(direction == 1 ? 180 : -180);

        animator.Jump();

        sound.PlayJump();

        jumps++;
        if(jumps >= 50){
            quests.CompleteQuest("jump_master");
        }
//        Debug.Log(jumps);
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag=="Ground"){
            landEffect.Play();
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.tag=="Ground"){
            landEffect.Play();
        }
    }
}
