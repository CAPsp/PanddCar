using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Original.Debug;

public class ObjectiveKillController : MonoBehaviour {

    // 定数
    public const int DIGIT_NUMER = 3;

    [SerializeField] Sprite[] numbers_      = new Sprite[10];
    [SerializeField] int objectiveKillNum_  = 100;              // 目標キル数

    float imageWidth_;
    GameObject[] imageObjects_ = new GameObject[DIGIT_NUMER];

    void Awake() {

        // 例外処理
        DebugCommon.CheckArraysNull(numbers_);
        DebugCommon.Assert(0 < objectiveKillNum_ && objectiveKillNum_ < Mathf.Pow(10f, (float)(DIGIT_NUMER + 1)));

        Rect parentRect = GetComponent<RectTransform>().rect;

        // Imageを構成(newで簡単に生成できないのでこうした)
        imageWidth_ = parentRect.width / (float)DIGIT_NUMER;
        for (int i = 0; i < DIGIT_NUMER; i++) {

            imageObjects_[i] = new GameObject();

            RectTransform rectTrans = imageObjects_[i].AddComponent<RectTransform>(); ;
            rectTrans.SetParent(this.transform);
            rectTrans.anchoredPosition = new Vector2((i * imageWidth_) + (imageWidth_ / 2f), 0f);
            rectTrans.anchorMin = new Vector2(0f, 0.5f);
            rectTrans.anchorMax = new Vector2(0f, 0.5f);
            rectTrans.sizeDelta = new Vector2(imageWidth_, parentRect.height);

            imageObjects_[i].AddComponent<Image>();

        }

        Draw();
        
    }

    // キルしたらこれを呼ぶ
    public void Kill() {
        objectiveKillNum_--;
        Draw();

        // クリア判定
        if (objectiveKillNum_ <= 0) {
            //TODO
        }

    }

    void Draw() {

        float parentOffset = 0f;

        int number = objectiveKillNum_;
        for (int i = DIGIT_NUMER - 1; i >= 0; i--) {

            if (i != (DIGIT_NUMER - 1) && number <= 0) {
                imageObjects_[i].gameObject.SetActive(false);
                parentOffset += (imageWidth_ / 2f);
                continue;
            }

            imageObjects_[i].GetComponent<Image>().sprite = numbers_[number % 10];

            number /= 10;

        }

        // 桁がずれたときの表示の補正
        Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-parentOffset, pos.y);

    }
    
}
