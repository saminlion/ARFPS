using UnityEngine;
using System.Collections;

public class GyroCamera4 : MonoBehaviour
{
    public Gyroscope gyro;

    // Use this for initialization
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            GameObject camParent = new GameObject("camParent");
            camParent.transform.position = transform.position;
            transform.parent = camParent.transform;
        }
    }
	
    // Update is called once per frame
    void Update()
    {
        Invoke("gyroUpdate", 0.1f);
    }

    void gyroUpdate()
    {
        transform.Rotate(gyro.rotationRateUnbiased.x, -gyro.rotationRateUnbiased.y, 0);
//        Quaternion transquat = transform.rotation;
//        transquat.w = gyro.attitude.w;
//
//        transquat.x = gyro.attitude.x;
//        transquat.y = gyro.attitude.y;
//        transquat.z = 0;
//
//        transform.rotation = Quaternion.Euler(0, 0, 0) * transquat;
    }
}
