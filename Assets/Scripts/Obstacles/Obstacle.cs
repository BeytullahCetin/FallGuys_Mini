using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Collider[] colliders;
    public Collider[] GetColliders { get { return colliders; } }
}
