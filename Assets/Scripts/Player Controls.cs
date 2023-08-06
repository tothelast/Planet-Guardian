using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast the player moves when pushing the buttons")]
    [SerializeField] float speed = 25f;
    [Tooltip("How far the player moves horizontally")]
    [SerializeField] float xRange = 7f;
    [Tooltip("How far the player moves vertically")]
    [SerializeField] float yRange = 10f;

    [Header("Screen Position Based Tuning")]
    [SerializeField] float pitchFactor = -2f;
    [SerializeField] float yawFactor = 2f;
    [SerializeField] float rollFactor = -25f;

    [Header("Player Input Based Tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlYawFactor = 15f;

    [Header("Laser Gun Array")]
    [Tooltip("Add all game lasers here")]
    [SerializeField] GameObject[] lasers;

    float xThrow, yThrow;

    void Update()
    {
        ProcessPosition();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitch = transform.localPosition.y * pitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * yawFactor + xThrow * controlYawFactor;
        float roll = xThrow * rollFactor;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    void ProcessPosition()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * speed;
        float yOffset = yThrow * Time.deltaTime * speed;

        float newXPos = transform.localPosition.x + xOffset;
        float newYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    { 
        if (Input.GetKey(KeyCode.Space))
        {
            SetLasersActive(true);   
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
            
        }
    }
}
