﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Object、範囲、周期を指定すると、範囲内のランダムな位置にObjectが出現する
// 範囲は長方形で表現され、このオブジェクトが存在する点が中心となる
public class SpawnObject : MonoBehaviour {

	[SerializeField] GameObject[] spawnObj_;
	[SerializeField] float intervalSeconds_ = 1f;
	[SerializeField] float spawnX_;		// 出現範囲を示す長方形の横
	[SerializeField] float spawnZ_;		// 出現範囲を示す長方形の縦
    [SerializeField] int initialEnemyNum_   = 10;

	float currentSeconds_;				// 計測地点から今までの時間
	Spawn spawn_;
	List<GameObject> enemyList_;		// スポーンした敵obj

	void Awake(){
		currentSeconds_ = 0f;
		spawn_ 			= null;
		enemyList_		= new List<GameObject>();
	}

    // 初期値分、敵をスポーンさせる
    void Start() {

        for(int i = 0; i < initialEnemyNum_; i++) {
			GameObject tmp 			= RandomSpawn().InstantSpawn();
            tmp.transform.parent 	= this.transform;
			enemyList_.Add (tmp);
        }
    }

	// Update is called once per frame
	void Update () {
	
		// 次の周期が来てないなら出現処理はしない
		if (currentSeconds_ < intervalSeconds_) {
			currentSeconds_ += Time.deltaTime;
			return;
		}

		// 出現位置と出現オブジェクトをランダムに決めてSpawnオブジェクトを生成
		if (spawn_ == null) {

            spawn_ = RandomSpawn();
		}

		GameObject spawnedObj = spawn_.Update ();
		if (spawnedObj != null) {
			
			spawnedObj.transform.parent = this.transform;
			enemyList_.Add (spawnedObj);
			spawn_ 						= null;
			currentSeconds_ 			= 0f;
		}

	}

	public List<GameObject> GetEnemyList(){
		return enemyList_;
	}

    // 乱数で決めた位置にオブジェクトを出現させる
    Spawn RandomSpawn() {

        // 起点位置(このObj自体) + 決めた長方形の出現範囲内のランダムな点 = 出現位置
		float x = spawnX_ * (Random.value - 0.5f);
		float z = spawnZ_ * (Random.value - 0.5f);
        Vector3 spawnPoint = transform.position + new Vector3(x, 0, z);

        // C#既存の乱数生成クラスを使う(戻り値がintで使いやすい)
        System.Random cRand = new System.Random();
        return new Spawn(spawnObj_[cRand.Next(spawnObj_.Length)], spawnPoint);

    }

	// 出現処理を扱う内部クラス
	class Spawn{

		GameObject obj_;	// 出現するオブジェクト
		Vector3 point_;		// 出現場所

		public Spawn(GameObject obj, Vector3 point){
			obj_ 	= obj;
			point_ 	= point;
		}
			
		// 出現処理。終わったら出現したオブジェクトを返す
		public GameObject Update(){

            // 今は特にエフェクトをかけないので生成するのみ
            return InstantSpawn();

		}

        // 演出なしでいきなり出現させる
        public GameObject InstantSpawn() {

            GameObject spawnedObj = Instantiate(obj_) as GameObject;
            spawnedObj.transform.position = point_;

            return spawnedObj;
        }

	}

	// Debug用、出現範囲の可視化
	void OnDrawGizmosSelected(){
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube (transform.position, new Vector3(spawnX_, 1, spawnZ_));
	}

}
