using UnityEngine;
using System.Collections;
using Original.Debug;

// 円周上に沿いながら注視点を見続ける動き
public class TurningCamera : MonoBehaviour {

    [SerializeField] Transform gazePoint_;          // 注視するobj
    [SerializeField] float radiusOfCircle_;         // 円の半径
    [SerializeField] float pointY_;                 // y軸点(固定)
    [SerializeField] float speedPerSec_ = 10f;

    float angleEulerOnCircle_ = 0f;         // 円周上の角度

    void Awake() {

        MoveCamera();
    }

    void Update() {

        MoveCamera();        

        angleEulerOnCircle_ += speedPerSec_ * Time.deltaTime;
        if(angleEulerOnCircle_ >= 360f) {
            angleEulerOnCircle_ -= 360f;
        }

    }

    void MoveCamera() {

        // 固定されたy軸点と距離、円周上の角度から位置を変化
        float rad   = angleEulerOnCircle_ * Mathf.Deg2Rad;
        float x     = Mathf.Cos(rad) * radiusOfCircle_;
        float z     = Mathf.Sin(rad) * radiusOfCircle_;
        transform.position = new Vector3(x, pointY_, z);

        // 方向変化
        transform.LookAt(gazePoint_);

    }

}
