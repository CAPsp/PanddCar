using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Original.UI;
using UnityEngine.SceneManagement;

public class GameEndButton : MonoBehaviour {

    [SerializeField] Image displayImage_;

    ButtonProcess buttonProcess_;

    void Awake() {

        buttonProcess_ = new ButtonProcess(  displayImage_,
                                             0.5f,
                                             GetComponent<AudioSource>());
    }

    void Update() {

        if (buttonProcess_.Effect()) {
            Application.Quit();
        }
    }

    public void Push() {

        buttonProcess_.Push();
    }
}
