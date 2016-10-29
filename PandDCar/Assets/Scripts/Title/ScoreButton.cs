using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Original.UI;
using UnityEngine.SceneManagement;

public class ScoreButton : MonoBehaviour {

    [SerializeField] GameObject scoreObj_;
    [SerializeField] GameObject Buttons_;
    [SerializeField] AudioSource audioSE_;  // 処理の関係上外部のAudioSourceを使う

    ButtonProcess buttonProcess_;

    void Awake() {

        buttonProcess_ = new ButtonProcess(null, 0f, null);
    }

    void Update() {

        if (buttonProcess_.Effect()) {

            audioSE_.Play();
            
            buttonProcess_ = new ButtonProcess(null, 0f, null);

            scoreObj_.SetActive(true);
            Buttons_.SetActive(false);
        }
    }

    public void Push() {

        buttonProcess_.Push();
    }
}
