using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Original.UI;

public class SceneChangeButton : MonoBehaviour {

    [SerializeField] string nextSceneName_;
    [SerializeField] Image displayImage_;
    [SerializeField] float darckChangeSpeed_ = 1.0f;
    
    ButtonProcess buttonProcess_;

    void Awake() {

        buttonProcess_ = new ButtonProcess(  displayImage_,
                                             darckChangeSpeed_,
                                             GetComponent<AudioSource>());
    }

    void Update() {

        if (buttonProcess_.Effect()) {
            SceneManager.LoadScene(nextSceneName_);
        }
    }

    public void Push() {

        buttonProcess_.Push();
    }

}
