using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StageLimitAction : MonoBehaviour {

    private Rigidbody rb;
    public GameObject timer;

    void OnTriggerEnter(Collider other) {

        // 車なら
        if (other.gameObject.tag == "Player") {

            //フェードイン
            Fader.instance.BlackIn(4);

            //初期地点にワープ
            timer.SendMessage("Drop");
            rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            other.transform.rotation = Quaternion.Euler(0, 0, 0);
            other.transform.position = new Vector3(0, 0, 0);

        }

        // 他のものなら(主に敵)
        else {
            if(other.gameObject.tag == "Enemy")
            Destroy(other.gameObject);

        }
        
    }

}
