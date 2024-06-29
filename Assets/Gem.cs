using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public ParticleSystem destroyEffect;

    public BoxCollider triggeredCollider;

    void Start(){
        Invoke(nameof(CleanUp), 2f);
    }

    void CleanUp(){
        if(transform.parent==null){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Ground" && gameObject.transform.parent==null){
            if(!other.gameObject.GetComponent<GroundMoveController>().IsInvisible){
                gameObject.transform.parent=other.gameObject.transform;
                triggeredCollider.enabled=false;
            }
        }
        if(other.gameObject.tag.ToLower().Equals("gem")){
            if(other.gameObject.transform.parent == null){
                Destroy(other.gameObject);
            }else{
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
        if(other.gameObject.tag.ToLower().Equals("enemy")){
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.tag.ToLower().Equals("gem")){
            if(other.gameObject.transform.parent == null){
                Destroy(other.gameObject);
            }else{
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag.ToLower().Equals("gem")){
            if(other.gameObject.transform.parent == null){
                Destroy(other.gameObject);
            }else{
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
        if(other.gameObject.tag.ToLower().Equals("enemy")){
            Destroy(gameObject);
        }
    }

    void OnCollisionStay(Collision other){
        if(other.gameObject.tag.ToLower().Equals("gem")){
            if(other.gameObject.transform.parent == null){
                Destroy(other.gameObject);
            }else{
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
    }

    public void HandleDestroy(){
        destroyEffect.gameObject.transform.parent = null;
        destroyEffect.Play();

        Destroy(gameObject);
    }
}
