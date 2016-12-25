using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NowLoading : MonoBehaviour {

	[SerializeField] TimerControl timerControl_;
	[SerializeField] float fadeOutSpeed_ = 1f;
	[SerializeField] ActiveArea activeArea_;

	// Use this for initialization
	void Start () {
		timerControl_.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		// 遠くの敵を消す処理やってなかったらNowLoadingを出し続ける
		if (!activeArea_.GetActivate ()) {
			return;
		}

		if (fadeOut ()) {
			timerControl_.enabled 	= true;
			this.enabled 			= false;
		}
	}

	// trueでフェードアウト完了
	bool fadeOut(){

		GetComponent<Image> ().color = Color.Lerp (GetComponent<Image> ().color, Color.clear, Time.deltaTime * fadeOutSpeed_); 
		if (GetComponent<Image> ().color.a <= 0.1f) {	// ほとんど透明なら
			return true;
		}

		return false;
	}
}
