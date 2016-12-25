using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// このエリアに入ったら敵がActiveになる
// エリアは円状に展開される
public class ActiveArea : MonoBehaviour {

	[SerializeField, Range(10f, 100f)]
	float radial_ 		= 30f;

	[SerializeField]
	float activeTime_ 	= 2f;		// このスクリプトが動作するようになるタイミング(こうしないと最初の敵が落ちてこない)

	[SerializeField]
	GameObject parentEnemy_;

	bool activate_ 	= false;	// 初めの動作でtrueに
	float seconds_ 	= 0f;

	void Update(){

		if (seconds_ < activeTime_) {
			seconds_ += Time.deltaTime;
			return;
		}

		// 全ての敵を探索する処理
		SpawnObject[] spawnObjects = parentEnemy_.GetComponentsInChildren<SpawnObject> ();
		for (int i = 0; i < spawnObjects.Length; i++) {

			List<GameObject> enemyList = spawnObjects [i].GetEnemyList ();
			foreach(GameObject enemy in enemyList){

				// 円状のエリアに入っていたらActiveにする
				float x = enemy.transform.position.x - transform.position.x;
				float z = enemy.transform.position.z - transform.position.z;
				if( (x * x + z * z) <= (radial_ * radial_) ){
					enemy.SetActive (true);
				}
				else{
					enemy.SetActive (false);
				}

			}

		}

		activate_ = true;
	}

	public bool GetActivate(){ return activate_; }

}
