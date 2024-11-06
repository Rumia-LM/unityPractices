using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyManager candyManager;
    public int reward;
    public GameObject effectPrefab;
    public Vector3 effectRptation;
    void OnTriggerEnter(Collider other){
        candyManager.AddCandy(reward);
        if(other.gameObject.tag=="Candy"){
            Destroy(other.gameObject);

            if(effectPrefab != null){
                Instantiate(
                    effectPrefab,
                    other.transform.position,
                    Quaternion.Euler(effectRptation)
                );
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
