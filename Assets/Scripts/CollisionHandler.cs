using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem crashParticles;
    float levelLoadDelay = 1f;

    void OnTriggerEnter(Collider other) 
    {
        StartCrashSequence();
    }

    void ReloadLevel()
    {
        int currentLevel =  SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }

    void StartCrashSequence()
    {
        crashParticles.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
}
