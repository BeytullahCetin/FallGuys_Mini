using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : Obstacle
{
    [SerializeField] float rotateSpeed = 500f;
    [SerializeField] float minTimeToChangeDirection = 2f;
    [SerializeField] float maxTimeToChangeDirection = 5f;

    float timeToChangeDirection;
    float time = 0;

    IEnumerator Start()
    {
        timeToChangeDirection = Random.Range(minTimeToChangeDirection, maxTimeToChangeDirection);

        while (true)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

            time += Time.deltaTime;
            if (time >= timeToChangeDirection)
            {
                rotateSpeed = -rotateSpeed;
                time = 0;
            }

            yield return null;
        }
    }
}
