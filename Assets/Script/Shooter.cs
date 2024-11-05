using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] candyPrefabs;
    public Transform candyParentTransform;
    public GameObject candyPrefab;
    public float shotForce;
    public float shotTorque;
    public float baseWidth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) Shot();
    }

    GameObject SampleCandy(){
        int index = Random.Range(0, candyPrefabs.Length);
        return candyPrefabs[index];
    }

    Vector3 GetInstantiatePosition(){
        float x = baseWidth * (Input.mousePosition.x / Screen.width)-(baseWidth/2);
        return transform.position + new Vector3(x,0,0);
    }
    public void Shot(){
        GameObject candy = (GameObject)Instantiate(
            SampleCandy(),
            GetInstantiatePosition(),
            Quaternion.identity//どの向きで
            //クォータニオン、四次元数 xyzでベクトルを作って、4つ目の引数で回転させている
            //三次元の回転をするとき、ジンバルロックという制御不能な状態に陥ることがあるが、それを回避している
            );
        
        candy.transform.parent = candyParentTransform;

        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        candyRigidBody.AddForce(transform.forward*shotForce);
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));
    }
}
