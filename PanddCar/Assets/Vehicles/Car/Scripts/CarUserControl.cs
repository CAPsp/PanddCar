using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = Input.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif

			// ひっくり返らないように調整
			Vector3 rotationEuler = gameObject.GetComponent<Rigidbody>().rotation.eulerAngles;
			if (rotationEuler.x >= 20f && rotationEuler.x <= 340f) {
				rotationEuler.x = (rotationEuler.x <= 180f) ? 20f : 340f;
			}
			if (rotationEuler.z >= 20f && rotationEuler.z <= 340f) {
				rotationEuler.z = (rotationEuler.z <= 180f) ? 20f : 340f;
			}
			transform.rotation = Quaternion.Euler(rotationEuler);
		
		}
    }
}
