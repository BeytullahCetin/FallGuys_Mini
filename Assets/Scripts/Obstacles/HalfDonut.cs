using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonut : MonoBehaviour
{
    [SerializeField] Transform donutStick;
    [SerializeField] float speed;
    [SerializeField] float distance = 0.1f;

    IEnumerator Start()
    {
        while (true)
        {
            donutStick.transform.localPosition = new Vector3(Mathf.PingPong(Time.time * speed, distance * 2) - distance, 0, 0);

            yield return null;
        }
    }
}
