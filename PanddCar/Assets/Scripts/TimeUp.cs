using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeUp : MonoBehaviour
{
    //Playerを取得
    private GameObject Car;
    private Rigidbody rb;
    private 

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Text>().enabled = false;
        Car = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Zero()
    {
        gameObject.GetComponent<Text>().enabled = true;
        rb = Car.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        Destroy(Car.GetComponent<BoxCollider>());

        //ここにシーン遷移の処理をお願いします。

    }
}