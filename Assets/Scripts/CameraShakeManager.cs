using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public Camera mainCam;

    float shakeAmount = 0;

    void Awake()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }

    public void Shake(float amt, float length)
    {
        shakeAmount = amt;

        //Repeat the shaking method each 0.01 seconds
        InvokeRepeating("DoShake", 0, 0.01f);
        
        //Stops the shake
        Invoke("StopShake", length);

    }
    // Start is called before the first frame update
    void DoShake()
    {
        if(shakeAmount > 0)
        {
            Vector3 camPos = mainCam.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;
        }
    }

    // Update is called once per frame
    void StopShake()
    {
        CancelInvoke("DoShake");
        mainCam.transform.localPosition = new Vector3(0f, 0f, -20f);
    }
}
