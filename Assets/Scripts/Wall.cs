using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wall : MonoBehaviour
{
    Texture2D emptyWall;

    [SerializeField] GameObject paint;
    [SerializeField] Transform paints;
    bool mouseButtonDown = false;

    Vector3 localHitPoint;

    float paintedPercentage = 0;
    float paintedArea = 0;

    bool canPaint = true;
    float timeOut = 1f;


    private void Start()
    {
    }

    private void Update()
    {
        if (!mouseButtonDown)
            return;

        if (!canPaint)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 2f);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                canPaint = false;
                Paint(hit);
                StartCoroutine(TimeOut());
            }
        }

    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(timeOut);
        canPaint = true;
    }

    public void OnFireInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mouseButtonDown = true;
        }

        if (context.canceled)
        {
            mouseButtonDown = false;
        }
    }

    void Paint(RaycastHit hit)
    {
        GameObject go;
        Transform goTransform;
        Vector2 coll;
        float a;
        float b;

        if (localHitPoint.y < .3 && localHitPoint.x < .3)
        {
            go = Instantiate(paint, hit.point + Vector3.back * .1f, Quaternion.identity, paints);
            goTransform = go.transform;

            paintedArea += 25;
            Debug.Log(paintedArea);

            foreach (Transform paint in paints)
            {
                if (paint == goTransform)
                    continue;


                if (paint.localPosition.x - 5 < goTransform.localPosition.x &&
                goTransform.localPosition.x < paint.localPosition.x + 5)
                {
                    if (paint.localPosition.y - 5 < goTransform.localPosition.y &&
                    goTransform.localPosition.y < paint.localPosition.y + 5)
                    {
                        Debug.Log(paint.name + ": " + paint.localPosition);
                        Debug.Log(goTransform.name + ": " + goTransform.localPosition);
                        Debug.LogWarning("Çakışma var");

                        a = paint.localPosition.x + 5 - goTransform.localPosition.x;
                        b = paint.localPosition.y + 5 - goTransform.localPosition.y;

                        paintedArea -= a * b;


                    }

                }
            }
        }



    }
}
