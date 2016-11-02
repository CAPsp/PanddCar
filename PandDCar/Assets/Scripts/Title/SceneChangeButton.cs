using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Original.UI;

public class SceneChangeButton : MonoBehaviour {

    [SerializeField] string nextSceneName_;

    ButtonProcess buttonProcess_;

    void Awake() {

        buttonProcess_ = new ButtonProcess( GetComponent<AudioSource>());
    }

    public void Push() {

        buttonProcess_.Push();

        Fader.instance.BlackOut("game");
        
        transform.parent.gameObject.SetActive(false);
    }

}
