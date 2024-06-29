using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject : MonoBehaviour
{
    public float destroyPositionX;

    public float speed;

    void Start(){
        speed = Random.Range(2f, 5f);

        transform.localScale *= Random.Range(0.5f, 2f);
    }

    void Update(){
        if(transform.position.x >= destroyPositionX){
            Destroy(gameObject);
        }

        transform.Translate(Vector3.forward * Time.timeScale * speed / 10);
    }
}
