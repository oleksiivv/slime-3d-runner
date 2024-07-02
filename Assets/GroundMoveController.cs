using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoveController : MonoBehaviour
{
    public int finishPos;
    public int instPos=24;
    public static float speed=1.4f;
    protected bool cleanChilds=true;

    public PlayerScore score;

    public List<BoxCollider> colliders;
    public List<MeshRenderer> meshes;

    public static bool HasInvisible=false;

    public bool IsInvisible=false;

    public bool CanBeInvisible=false;

    void Start(){
        //instPos=24;
        cleanChilds=true;

        Reset();
    }

    public void Update(){
        if(SlimeHealth.IsAlive){
            transform.Translate(Vector3.right/15*Time.timeScale*speed);
            speed+=0.000001f*Time.timeScale;
        }

        if((int)transform.position.x==finishPos){
            for(int i=0;i<gameObject.transform.childCount;i++){
                if(gameObject.transform.GetChild(i).gameObject.tag!="wall" && cleanChilds){
                    Destroy(gameObject.transform.GetChild(i).gameObject);
                }
            }
            Reset();

            gameObject.transform.position-=new Vector3(instPos,0,0);            
        }
    }

    void Reset(){
        HasInvisible=false;
        IsInvisible = false;

        Visible();

        if(score && !HasInvisible){
            if(score.IsHardMode()){
                RandomMakeInvisible();
            }
        }
    }

    void OnTriggerEnter(Collider other){
//        Debug.Log(other.gameObject.name);

        if(other.gameObject.tag.ToLower().Equals("player") && IsInvisible && !meshes[0].enabled){
            other.gameObject.GetComponent<SlimeHealth>().HandleExternalDamage();
        }
    }

    void RandomMakeInvisible(){
        if(Random.Range(1, 6) != 2 || !CanBeInvisible){
            return;
        }

        Debug.Log("Random make invisible");

        IsInvisible=true;
        HasInvisible=true;

        foreach(var collider in colliders){
            //collider.isTrigger = true;
        }
        foreach(var mesh in meshes){
            mesh.enabled = false;
        }
    }

    void Visible(){
        foreach(var collider in colliders){
            //collider.isTrigger = false;
        }
        foreach(var mesh in meshes){
            mesh.enabled = true;
        }
    }
}
