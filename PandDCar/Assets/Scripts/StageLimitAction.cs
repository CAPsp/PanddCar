using UnityEngine;
using System.Collections;

public class StageLimitAction : MonoBehaviour {

    [SerializeField] TimerControl timerControl_;

    Vector3 carRespwanPoint_;
    private Rigidbody rb;

    void Start() {
        carRespwanPoint_ = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void OnTriggerEnter(Collider other) {

        // 車なら
        if(other.gameObject.tag == "Player") {
            timerControl_.Drop();
            rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            other.transform.rotation = Quaternion.Euler(0, 0, 0);
            other.gameObject.transform.position = carRespwanPoint_ + new Vector3(0f, 0f, 0f);
            Fader.instance.BlackIn();
        }

        // 敵なら
        else if(other.gameObject.tag == "Enemy"){
            Destroy(other.gameObject);
        }
        
    }

}
