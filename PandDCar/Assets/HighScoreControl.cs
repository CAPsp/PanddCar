using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Original;

public class HighScoreControl : MonoBehaviour {

    Text text_;
    Color defaultColor_;
    bool isScoreUpdate_ = false;

    void Awake() {
        text_           = GetComponent<Text>();
        defaultColor_   = text_.color;

        text_.enabled   = false;
    }

    void Update() {

        if (isScoreUpdate_) {
            text_.color = Color.Lerp(defaultColor_, Color.clear, Mathf.PingPong(Time.time, 1f));
        }

    }

    public void CheckScore(int score) {
        
        // スコア更新処理
        ArrayList array = ScoreManagement.Read();
        for (int i = 0; i < ScoreManagement.LIMIT_RANK; i++) {

            if ( score >= (int)array[i]) {
                ScoreManagement.Write(i + 1, score);
                isScoreUpdate_ = true;

                text_.enabled = true;

                break;
            }
        }
    
    }

}
