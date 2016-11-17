using UnityEngine;
using System.Collections;

public class CarHitAction : MonoBehaviour {
	
	[SerializeField]
	ObjectiveKillController objectiveKill_;

	[SerializeField]
	float impulseVol_ = 1f;    // この値が大きいほど吹き飛ばしが強くなる

	AudioSource audioHit_;

    void Awake() {
        audioHit_ = GetComponent<AudioSource>();
    }

	void OnCollisionEnter(Collision other) {

		// 敵ならSEを流して吹っ飛ばす
		if(other.gameObject.tag == "Enemy") {

			if (audioHit_.isPlaying) {
				audioHit_.Stop();
			}

			audioHit_.Play();

			objectiveKill_.Kill();  // 1キルしたこととする


			// ----ここから下は敵の内部処理----

			// 管理しているListから削除(どっちにしろStageLimitActionで消すが、ここでも削除しないと敵が下に落ちていかない)
			other.transform.parent.GetComponent<SpawnObject> ().GetEnemyList ().Remove (other.gameObject);

			other.gameObject.GetComponent<Collider>().isTrigger = true;	// 地面とのあたり判定を消す

			// 衝突した向きに少し上向きの補正をかけて吹っ飛ばす
			Vector3 blowAngle   = (-1f * other.contacts[0].normal + new Vector3(0f, 1f, 0f)).normalized;
			other.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(
				(other.relativeVelocity.magnitude * impulseVol_) * blowAngle,
				other.contacts[0].point,
				ForceMode.Impulse);

		}

	}

	/*
	void OnTriggerEnter(Collider other) {

        // 敵ならSEを流して吹っ飛ばす
        if(other.gameObject.tag == "Enemy") {

            if (audioHit_.isPlaying) {
                audioHit_.Stop();
            }

            audioHit_.Play();

            objectiveKill_.Kill();  // 1キルしたこととする


			// ----ここから下は敵の内部処理----

			other.gameObject.GetComponent<EnemyForce> ().Damage();

			// 衝突した向きに少し上向きの補正をかけて吹っ飛ばす
			Vector3 blowAngle = other.transform.position - transform.position;

			blowAngle = (blowAngle + new Vector3 (0f, 1f, 0f)).normalized;
			other.gameObject.GetComponent<EnemyForce> ().AddImpulse (
				impulseVol_ * blowAngle
			);

        }

    }
    */


}
