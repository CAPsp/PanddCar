using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Original.UI;
using UnityEngine.SceneManagement;

public class GameEndButton : MonoBehaviour {
    
    ButtonProcess buttonProcess_;
    System.Action end = () => { Application.Quit(); };

    void Awake() {

        buttonProcess_ = new ButtonProcess( GetComponent<AudioSource>());
    }

    public void Push() {

        buttonProcess_.Push();

        Fader.instance.BlackOut( end );
        
        transform.parent.gameObject.SetActive(false);
    }
    
}
