using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// うろちょろ、たまにぴょんぴょん
// 敵のオブジェクトにそのまま持たせると重くなるので、その親オブジェクトが持つ
public class EnemyAction : MonoBehaviour {

	[SerializeField]
	float intervalSeconds_  = 1f;       // 次の行動に移る周期

	[SerializeField, Range(0f, 1f)]
	float activePercent_    = 0.2f;     // 子objの中で行動する割合(%)

	float currentSeconds_ 	= 0f;

	// Update is called once per frame
	void FixedUpdate () {

		// 行動周期待ち
		if (currentSeconds_ < intervalSeconds_) {
			currentSeconds_ += Time.deltaTime;
			return;
		}
		currentSeconds_ = 0f;

		int maxSize = transform.childCount;

		// 集合から重複なしランダムな数列を生成する。改良Fisher-Yates法を使用。
		Dictionary<int, int> children = new Dictionary<int, int>();
		for(int i = 0; i < maxSize; i++) {
			children.Add(i, i);
		}

		int activeNum 				= (int)((float)maxSize * activePercent_);
		System.Random randGen 		= new System.Random( System.Environment.TickCount );
		Rigidbody[] childrenRigid 	= GetComponentsInChildren<Rigidbody>();
		int randValue, index;
		for (int i = 0; i < activeNum; i++) {
			
			randValue 				= randGen.Next (maxSize);
			index 					= children[ randValue ];

			// ジャンプ確率20% : 移動確率80%
			if (Random.value <= 0.2f) {	// ジャンプ
				childrenRigid[index].velocity = new Vector3 (0f, -Physics.gravity.y, 0f);
			}
			else {						// 移動
				
				if (childrenRigid [index].gameObject.GetComponent<EnemyMove> () == null) {
					childrenRigid [index].gameObject.AddComponent <EnemyMove>();
				}
			}

			children[ randValue ] = children[ maxSize - 1 ];
			children.Remove ( maxSize - 1 );
			maxSize--;
		}

	}


}