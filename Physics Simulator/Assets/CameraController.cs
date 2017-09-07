using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject followTarget;
    private Vector3 TargetPosition;
    private float moveSpeed;
    private float buffer = 0.5f;
    private float speedMod = 0.7f;

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

        try
        {
            int particle = UiController.instances.DropBoxParticle.value;
            followTarget = SimulateController.ParticleInstances[particle];
            moveSpeed = SimulateController.ParticleInstances[particle].GetComponent<Rigidbody>().velocity.magnitude * speedMod;
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
}
