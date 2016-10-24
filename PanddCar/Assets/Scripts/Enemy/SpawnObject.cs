using UnityEngine;
using System.Collections;

// Object、範囲、周期を指定すると、範囲内のランダムな位置にObjectが出現する
public class SpawnObject : MonoBehaviour {

	[SerializeField] GameObject[] spawnObj_;
	[SerializeField] float intervalSeconds_;
	[SerializeField] float spawnCircleRad_;		// 起点からの出現範囲を示す円の半径

	float currentSeconds_;	// 計測地点から今までの時間
	Spawn spawn_;

	void Awake(){
		currentSeconds_ = 0f;
		spawn_ 			= null;
	}

	// Update is called once per frame
	void Update () {
	
		// 次の周期が来てないなら出現処理はしない
		if (currentSeconds_ <= intervalSeconds_) {
			currentSeconds_ += Time.deltaTime;
			return;
		}

		// 出現位置と出現オブジェクトをランダムに決めてSpawnオブジェクトを生成
		if (spawn_ == null) {

			// 起点位置(このObj自体) + xz面に並行な円内部のランダムな点 = 出現位置
			Vector2 randCircle = Random.insideUnitCircle * spawnCircleRad_;
			Vector3 spawnPoint = transform.position + new Vector3 (randCircle.x, 0, randCircle.y);

			// C#既存の乱数生成クラスを使う(戻り値がintで使いやすい)
			System.Random cRand = new System.Random ();
			spawn_ = new Spawn (spawnObj_[cRand.Next(spawnObj_.Length)], spawnPoint);
		}

		GameObject spawnedObj = spawn_.Update ();
		if (spawnedObj != null) {
			
			spawnedObj.transform.parent = this.transform;
			spawn_ 						= null;
			currentSeconds_ 			= 0f;
		}

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
			GameObject spawnedObj 			= Instantiate(obj_) as GameObject;
			spawnedObj.transform.position 	= point_;

			return spawnedObj;
		}

	}

}
