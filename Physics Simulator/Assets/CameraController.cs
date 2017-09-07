using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private GameObject followTarget;
    private Vector3 TargetPosition;
    private float moveSpeed;
    private float buffer = 0.1f;
    private float speedMod = 0.8f;
    private float freeSpeed = 0.5f;
    private bool isFreeRoam;

    private static bool CameraExists;
    // Use this for initialization
    void Start()
    {

        if (CameraExists == false)
        {
            CameraExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int particle = UiController.instances.DropBoxCameraTarget.value;
        if (particle == 0)
        {
            //Free roam
            isFreeRoam = true;

        }
        else
        {
            isFreeRoam = false;
            try {
                followTarget = SimulateController.ParticleInstances[particle - 1];
                Debug.Log("Target found");
                moveSpeed = SimulateController.ParticleInstances[particle-1].GetComponent<Rigidbody>().velocity.magnitude * speedMod;
                TargetPosition = new Vector3(
                    followTarget.transform.position.x + buffer,
                    followTarget.transform.position.y + buffer,
                    transform.position.z);
                transform.position = Vector3.Lerp(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
            }
            catch
            {

            }

        }

        if (isFreeRoam)
        {
            float input_x = Input.GetAxisRaw("Horizontal");
            float input_y = Input.GetAxisRaw("Vertical");
            transform.position += new Vector3(
                input_x,
                input_y,
                0.0f).normalized * freeSpeed;

        }


    }
}
