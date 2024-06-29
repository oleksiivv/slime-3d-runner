using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonGravityEnemy : Enemy
{
    public Vector2 yPosRange;

    void Awake()
    {
        useGravity=false;
        transform.position = new Vector3(transform.position.x, Random.Range(yPosRange.x, yPosRange.y), transform.position.z);
    }

    void Start(){
        Invoke(nameof(CleanUp), 1f);
    }

    void CleanUp(){
        if(transform.parent==null){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Ground" && gameObject.transform.parent==null){
            gameObject.transform.parent=other.gameObject.transform;
        }
    }
}
