using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 diff;
    [SerializeField] Transform objectToFollow;



    void Start()
    {
        diff = objectToFollow.position - transform.position;
    }

    void Update()
    {
        transform.position = objectToFollow.position - diff;
    }
}
