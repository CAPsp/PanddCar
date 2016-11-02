using UnityEngine;
using System.Collections;
using Original;

public class EnemyHitAction : MonoBehaviour {

    [SerializeField] float impulseVol_      = 5f;    // この値が大きいほど吹き飛ばしが強くなる

    // 車にぶつかった場合、派手に吹っ飛ぶ
    void OnCollisionEnter(Collision other) {

        if(other.gameObject.tag == "Player") {

            // 地面とのあたり判定を消す
            GetComponent<Collider>().isTrigger = true;
            
            // 衝突した向きに少し上向きの補正をかけて吹っ飛ばす
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            Vector3 blowAngle   = (other.contacts[0].normal + new Vector3(0f, 1f, 0f)).normalized;
            rigidbody.AddForceAtPosition( (other.relativeVelocity.magnitude * 5f) * blowAngle,
                                          other.contacts[0].point,
                                          ForceMode.Impulse);

        }

    }

}
