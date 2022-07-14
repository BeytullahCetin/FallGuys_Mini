using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteRunner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.CompleteLevel();
    }
}
