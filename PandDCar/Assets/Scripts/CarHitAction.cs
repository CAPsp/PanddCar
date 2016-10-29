﻿using UnityEngine;
using System.Collections;

public class CarHitAction : MonoBehaviour {

    AudioSource audioHit_;

    void Awake() {
        audioHit_ = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) {

        // 敵ならSEを流す
        if(other.gameObject.tag == "Enemy") {

            if (audioHit_.isPlaying) {
                audioHit_.Stop();
            }

            audioHit_.Play();

        }

    }

}
