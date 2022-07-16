using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 500f;
    
    [SerializeField] float pushForce = 10f;
    public float PushForce { get { return pushForce; } }

    bool isClokwise;
    public bool IsClokwise { get { return isClokwise; } }

    [SerializeField] float minTimeToChangeDirection = 2f;
    [SerializeField] float maxTimeToChangeDirection = 5f;

    float timeToChangeDirection;
    float time = 0;

    IEnumerator Start()
    {
        isClokwise = rotateSpeed > 0 ? true : false;

        timeToChangeDirection = Random.Range(minTimeToChangeDirection, maxTimeToChangeDirection);

        while (true)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

            time += Time.deltaTime;
            if (time >= timeToChangeDirection)
            {
                rotateSpeed = -rotateSpeed;
                isClokwise = !isClokwise;
                time = 0;
            }

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        var impactReceiver = other.gameObject.GetComponent<ImpactReceiver>();
        
        if (impactReceiver)
        {
            Vector3 dir = transform.right; ;
            if (false == IsClokwise)
            {
                dir = -dir;
            }
            impactReceiver.AddImpact(dir, PushForce);
        }
    }
}
