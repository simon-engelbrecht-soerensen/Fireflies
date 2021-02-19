using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireflyBehaviour : MonoBehaviour
{
    public float stationaryDuration = 5;
    //find point by raycasting in a circle
    private float refSpeed;
    private Vector3 startPos;
    private float rnd;
    Vector3 GetLandingPoint()
    {
        Vector3 startPoint = Random.insideUnitCircle * 10;
        startPoint = new Vector3(startPoint.x, 50, startPoint.z);
        Ray ray = new Ray(startPoint, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 500))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    void Start()
    {
        startPos = transform.position;
        rnd = Random.Range(0.5f, 3);
    }
    private void Update()
    {
        refSpeed += Time.deltaTime;
        float x = Mathf.Cos (refSpeed * rnd);
        // float y = Mathf.Sin (refSpeed);
        float z = Mathf.Sin (refSpeed * rnd);
        transform.position = startPos + new Vector3 (x, 0, z);
    }
}
