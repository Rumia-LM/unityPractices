using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    const int MaxShotPower=5;
    const int RecoverySeconds=3;
    int shotPower = MaxShotPower;
    AudioSource shotSound;
    public GameObject[] candyPrefabs;
    public Transform candyParentTransform;
    public CandyManager CandyManager;
    public GameObject candyPrefab;
    public float shotForce;
    public float shotTorque;
    public float baseWidth;
    // Start is called before the first frame update
    void Start()
    {
        shotSound=GetComponent<AudioSource>();
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
        // mousePositionとsreenWidthの関係について、左側をクリックすると0に近づくし、右側をクリックすると1に近づく
        // その値に対して画面幅の半分(baseWidth/2)の左右に振り分ける
        return transform.position + new Vector3(x,0,0);
    }
    public void Shot(){
        if(CandyManager.GetCandyAmount() <= 0) return;
        if(shotPower <= 0) return;
        //早期リターン、ネストが深くならずに済む
        GameObject candy = (GameObject)Instantiate(
            SampleCandy(),
            GetInstantiatePosition(),
            Quaternion.identity//どの向きで
            //クォータニオン、四次元数 xyzでベクトルを作って、4つ目の引数で回転させている
            //三次元の回転をするとき、ジンバルロックという制御不能な状態に陥ることがあるが、それを回避している
            );
        
        candy.transform.parent = candyParentTransform;
        //Unityでは要素の親子関係を結ぶとき、transformコンポーネントの子にする

        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        candyRigidBody.AddForce(transform.forward*shotForce);
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));

        CandyManager.ConsumeCandy();
        ConsumePower();

        shotSound.Play();
    }

    void OnGUI(){
        GUI.color = Color.black;

        String label="";
        for(int i=0; i<shotPower; i++) label=label+"+";

        GUI.Label(new Rect(50, 65, 100, 30), label);
    }

    void ConsumePower(){
        shotPower--;
        StartCoroutine(RecoverPower());
    }

    IEnumerator RecoverPower(){
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;
    }
}
