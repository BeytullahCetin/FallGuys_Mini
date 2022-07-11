using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] Transform[] parts;

    private void FixedUpdate()
    {
            for (int i = 0; i < parts.Length; i++)
            {
                if (i % 2 == 0)
                {
                    parts[i].Rotate(Vector3.forward * Time.deltaTime * speed);
                }
                else
                {
                    parts[i].Rotate(Vector3.back * Time.deltaTime * speed);
                }
            }
    }
}
