using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisctruct : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }
}
