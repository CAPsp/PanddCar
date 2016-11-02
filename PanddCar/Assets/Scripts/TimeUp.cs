using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeUp : MonoBehaviour
{
    //Playerを取得
    private GameObject Car;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Text>().enabled = false;
        Car = GameObject.FindGameObjectWithTag("Player");
    }

    void Zero()
    {
        gameObject.GetComponent<Text>().enabled = true;
        rb = Car.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        Destroy(Car.GetComponent<BoxCollider>());

        // ここにシーン遷移の処理をお願いします。
        // （ ＾ω＾）ゞ
        Fader.instance.BlackOut( 3f, SceneManager.GetActiveScene().name );

    }
}