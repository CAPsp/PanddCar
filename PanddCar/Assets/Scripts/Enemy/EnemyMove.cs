using UnityEngine;
using System.Collections;

// 移動(xz平面)を制御する内部クラス
// 処理が割ったら自動で消える
public class EnemyMove : MonoBehaviour {

	const float MAX_DISTANCE 	= 5f;
	const float SPEED 			= 1f;

	float distance_;
	Vector3 angle_;
	Transform transform_;

	void Awake(){
		
		transform_ = GetComponent<Transform>();

		// 移動量と方向をランダムに決める
		angle_ 		= Vector3.zero;
		angle_.x 	= Random.Range(-1f, 1f);
		angle_.z 	= Random.Range(-1f, 1f);
		angle_.Normalize();

		distance_ = Random.Range(0f, MAX_DISTANCE);

		// 決めた方向にオブジェクトの向きを変える
		transform_.LookAt(angle_ + transform_.position);
	}

	void Update(){

		float moveDiff = Time.deltaTime * SPEED;
		transform_.position += angle_ * moveDiff;

		distance_ -= moveDiff;
		if (distance_ <= 0f) {
			Destroy (this);		// 移動処理が終わったら消える
		}

	}

}

