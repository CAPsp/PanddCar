using UnityEngine;
using System.Collections;

public class CarHitAction : MonoBehaviour {
	
	[SerializeField]
	ObjectiveKillController objectiveKill_;

	[SerializeField]
	float impulseVol_ = 1f;    			// この値が大きいほど吹き飛ばしが強くなる

	[SerializeField]
	float intervalWallHitSec = 0.5f;	// これ以内なら連続で当たったとみなし、壁と車の判定はしない

	AudioSource audioHit_;
	float hitTimer_;

    void Awake() {
        audioHit_ = GetComponent<AudioSource>();
		hitTimer_ = 0f;
    }

	void OnCollisionEnter(Collision other) {
		
		if (hitTimer_ < intervalWallHitSec) {
			hitTimer_ += Time.deltaTime;
		}

		// 敵ならSEを流して吹っ飛ばす
		if (other.gameObject.tag == "Enemy") {

			if (audioHit_.isPlaying) {
				audioHit_.Stop ();
			}

			audioHit_.Play ();

			objectiveKill_.Kill ();  // 1キルしたこととする


			// ----ここから下は敵の内部処理----

			// 管理しているListから削除(どっちにしろStageLimitActionで消すが、ここでも削除しないと敵が下に落ちていかない)
			other.transform.parent.GetComponent<SpawnObject> ().GetEnemyList ().Remove (other.gameObject);

			other.gameObject.GetComponent<Collider> ().isTrigger = true;	// 地面とのあたり判定を消す

			// 衝突した向きに少し上向きの補正をかけて吹っ飛ばす
			Vector3 blowAngle = (-1f * other.contacts [0].normal + new Vector3 (0f, 1f, 0f)).normalized;
			other.gameObject.GetComponent<Rigidbody> ().AddForceAtPosition (
				(other.relativeVelocity.magnitude * impulseVol_) * blowAngle,
				other.contacts [0].point,
				ForceMode.Impulse);

		}
		else if(hitTimer_ >= intervalWallHitSec){	// 壁なら速度を殺さない程度に反発するようにする

			Vector2 normal = new Vector2 (other.contacts [0].normal.x, other.contacts [0].normal.z);
			Vector2 xzVec  = (new Vector2 (other.relativeVelocity.x, other.relativeVelocity.z)) * (-1f);

			// 反射ベクトルを計算
			Vector2 reflec	   = xzVec + 2f * Vector2.Dot(normal, xzVec * (-1f)) * normal;
			Vector3 reflecVec3 = new Vector3 (reflec.x, other.relativeVelocity.y * (-1f), reflec.y);

			// 減退を意識して反発
			GetComponent<Rigidbody> ().AddForceAtPosition (reflecVec3 * 0.5f, other.contacts[0].point, ForceMode.VelocityChange);

			hitTimer_ = 0f;
		}

	}

}
