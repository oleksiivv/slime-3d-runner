using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundSpawn : MonoBehaviour
{
    public List<GameObject> backgroundObjets;
    public Vector3 spawnPosition;
    public Vector2 yRange;

    public float delay;

    void Start(){
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){
        while(SlimeHealth.IsAlive){
            var newObject = backgroundObjets[Random.Range(0, backgroundObjets.Count)];
            Instantiate(
                newObject,
                new Vector3(spawnPosition.x, Random.Range(yRange.x, yRange.y), spawnPosition.z),
                newObject.transform.rotation
            );

            yield return new WaitForSeconds(delay + Random.Range(0.2f, 1f));
        }
    }
}
