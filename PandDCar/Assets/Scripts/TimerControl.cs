using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Original.Debug;

// 制限時間は DIGIT_NUMER 桁まで設定可能
public class TimerControl : MonoBehaviour {

    // 定数
    public const int DIGIT_NUMER = 3;

    [SerializeField] Sprite[] numbers_   = new Sprite[10];
    [SerializeField] int limitSeconds_;

    float currentTimer_;
    int beforeSeconds_;
    float imageWidth_;
    GameObject[] imageObjects_ = new GameObject[DIGIT_NUMER];
    public bool dropFlag = false;

    void Awake() {

        // 例外処理
        DebugCommon.CheckArraysNull(numbers_);
        DebugCommon.Assert(0 < limitSeconds_ && limitSeconds_ < Mathf.Pow(10f, (float)(DIGIT_NUMER + 1)));

        Rect parentRect = GetComponent<RectTransform>().rect;

        // Imageを構成(newで簡単に生成できないのでこうした)
        imageWidth_ = parentRect.width / (float)DIGIT_NUMER;
        for(int i = 0; i < DIGIT_NUMER; i++) {

            imageObjects_[i] = new GameObject();

            RectTransform rectTrans = imageObjects_[i].AddComponent<RectTransform>(); ;
            rectTrans.SetParent(this.transform);
            rectTrans.anchoredPosition  = new Vector2((i * imageWidth_) + (imageWidth_ / 2f), 0f);
            rectTrans.anchorMin         = new Vector2(0f, 0.5f);
            rectTrans.anchorMax         = new Vector2(0f, 0.5f);
            rectTrans.sizeDelta         = new Vector2(imageWidth_, parentRect.height);

            imageObjects_[i].AddComponent<Image>();
            
        }

        // タイマー画像の設定処理
        currentTimer_  = 0f;
        beforeSeconds_ = limitSeconds_ - (int)currentTimer_;
        Draw(beforeSeconds_);
        

    }
	
	// Update is called once per frame
	void Update () {

        currentTimer_ += Time.deltaTime;

        int seconds = limitSeconds_ - (int)currentTimer_;
        if (dropFlag == true) {
            limitSeconds_ -= 10;
            dropFlag = false;
        }
        if (seconds < 0) {
            return; // TODO: GAME OVER処理
        }
        if(beforeSeconds_ != seconds) {
            Draw(seconds);
            beforeSeconds_ = seconds;
        }

	}

    void Draw(int seconds) {

        float parentOffset = 0f;

        for(int i = DIGIT_NUMER - 1; i >= 0; i--) {

            if (i != (DIGIT_NUMER - 1) && seconds <= 0) {
                imageObjects_[i].gameObject.SetActive(false);
                parentOffset += (imageWidth_ / 2f);
                continue;
            }

            imageObjects_[i].GetComponent<Image>().sprite = numbers_[seconds % 10];

            seconds /= 10;

        }

        // 桁がずれたときの表示の補正
        Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-parentOffset, pos.y);

    }

    void Drop()
    {
        dropFlag = true;       
    }
}
