using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 3;
    [SerializeField] int scorePerDeath = 10;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start()
    {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("Spawn At Runtime");
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }   
    }

    void ProcessHit()
    {
        Instantiate(hitVFX, transform.position, Quaternion.identity);
        hitVFX.transform.parent = parentGameObject.transform;
        hitPoints--;
        scoreBoard.IncreaseScore(scorePerHit);
    }

    void KillEnemy()
    {
        Instantiate(deathFX, transform.position, Quaternion.identity);
        deathFX.transform.parent = parentGameObject.transform;
        Destroy(this.gameObject);
        scoreBoard.IncreaseScore(scorePerDeath);
    }
}
