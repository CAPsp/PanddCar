﻿using UnityEngine;
            // 衝突した向きに少し上向きの補正をかけて吹っ飛ばす
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            Vector3 blowAngle   = (other.contacts[0].normal + new Vector3(0f, 1f, 0f)).normalized;
            rigidbody.AddForceAtPosition( (other.relativeVelocity.magnitude * 5f) * blowAngle,