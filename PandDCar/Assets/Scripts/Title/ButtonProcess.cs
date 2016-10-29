using UnityEngine.UI;
using UnityEngine;

namespace Original.UI {

    // ボタンの基本処理を請け負う
    public class ButtonProcess {

        Image displayImage_;        // 暗転処理に使う
        float effectChangeSpeed_;
        AudioSource audioSource_;   // 押したときになる

        bool isPushed_          = false;
        DarkChange darckChange_ = null;

        public ButtonProcess(Image dis = null, float speed = 1f, AudioSource audio = null) {

            displayImage_ = dis;
            effectChangeSpeed_ = speed;
            audioSource_ = audio;

        }

        public void Push() {
            isPushed_ = true;

            if(audioSource_ != null) {
                audioSource_.Play();
            }

            if (displayImage_ != null) {
                darckChange_ = new DarkChange(displayImage_, effectChangeSpeed_);
            }
        }

        public bool Effect() {

            if (!isPushed_) {
                return false;
            }

            return darckChange_.CallAtUpdate();

        }

    }

}