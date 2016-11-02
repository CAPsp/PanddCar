using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Original.UI;
using UnityEngine.SceneManagement;

public class ScoreButton : MonoBehaviour {

    [SerializeField] GameObject scoreObj_;
    [SerializeField] AudioSource audioSE_;  // 処理の関係上外部のButtonProcessは使わない

    public void Push() {

        audioSE_.Play();

        scoreObj_.SetActive(true);

        transform.parent.gameObject.SetActive(false);
    }
}
