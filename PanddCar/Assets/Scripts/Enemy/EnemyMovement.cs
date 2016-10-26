using UnityEngine;
using System.Collections;

// うろちょろ、たまにぴょんぴょん
public class EnemyMovement : MonoBehaviour {

	[SerializeField] float intervalSeconds_ = 1f;		// 次の行動に移る周期(待機行動含む)

	Jump jump_ 				= null;
	Move move_ 				= null;
	float currentSeconds_ 	= 0f;
	Rigidbody rigibody_;

	void Awake(){
		rigibody_ = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		// 何もしてないとき
		if (jump_ == null && move_ == null && rigibody_.velocity.y == 0f ) {

			if (currentSeconds_ < intervalSeconds_) {
				currentSeconds_ += Time.deltaTime;
				return;
			}

			// ランダムに行動を決定する処理
			// 確率はそれぞれ
			// ジャンプ	： 低い
			// 移動		： それなり
			// 待機		： 高い
			float randVal = Random.value;
			if (randVal <= 0.1f) 		{ jump_ = new Jump (rigibody_); }
			else if (randVal <= 0.5f) 	{ move_ = new Move (transform); }

			currentSeconds_ = 0f;

		}
		else {

			if (jump_ != null && jump_.Update()) {
				jump_ = null;
			}
			else if (move_ != null && move_.Update()) {
				move_ = null;
			}

		}

	}

	// ジャンプを制御する内部クラス
	class Jump{

		Rigidbody rigidbody_;

		public Jump(Rigidbody rigidbody){
			rigidbody_ = rigidbody;
		}

		// true で動作終了
		public bool Update(){

			rigidbody_.velocity = new Vector3 (0f, -Physics.gravity.y, 0f);

			return true;
		}

	}

	// 移動(xz平面)を制御する内部クラス
	class Move{

		const float MAX_DISTANCE = 5f;
		const float SPEED = 1f;

		float distance_;
		Vector3 angle_;
		Transform transform_;

		public Move(Transform transform){

			transform_ = transform;

			// 移動量と方向をランダムに決める
			angle_ = Vector3.zero;
			angle_.x = Random.Range(-1f, 1f);
			angle_.z = Random.Range(-1f, 1f);
			angle_.Normalize();

			distance_ = Random.Range(0f, MAX_DISTANCE);

			// 決めた方向にオブジェクトの向きを変える
			transform.LookAt(angle_ * (-1f) + transform.position);

		}

		// true で動作終了
		public bool Update(){

			float moveDiff = Time.deltaTime * SPEED;
			transform_.position += angle_ * moveDiff;

			distance_ -= moveDiff;
			if (distance_ <= 0f) {
				return true;
			}

			return false;
		}

	}

}
