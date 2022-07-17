using System.Collections;
using UnityEngine;

public class HalfDonut : Obstacle
{
    [SerializeField] Transform donutStick;
    [SerializeField] float distance = 0.1f;
    [SerializeField] float speed;

    IEnumerator Start()
    {
        while (true)
        {
            donutStick.transform.localPosition = new Vector3(Mathf.PingPong(Time.time * speed, distance * 2) - distance, 0, 0);
            yield return null;
        }
    }
}
