using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : Obstacle
{
    [SerializeField] float horizontalMovementSpeed = 5f;
    [SerializeField] float horizontalDistance = 10f;
    [SerializeField] float rotateSpeed = 5f;

    Vector3 positionVector;
    float posX;
    float posY;
    float posZ;

    IEnumerator Start()
    {
        posY = transform.localPosition.y;
        posZ = transform.localPosition.z;
        
        positionVector.y = posY;
        positionVector.z = posZ;

        while (true)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            
            posX = Mathf.PingPong(Time.time * horizontalMovementSpeed, horizontalDistance * 2) - horizontalDistance;
            positionVector.x = posX;
            transform.localPosition = positionVector;

            yield return null;
        }
    }
}
