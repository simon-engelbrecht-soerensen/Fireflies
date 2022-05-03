using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireflyBehaviour : MonoBehaviour
{
    public float stationaryDuration = 5;
    //find point by raycasting in a circle
    private float refSpeed;
    private Vector3 startPos;
    private float rnd;
    public Light pointLight;

    [HideInInspector]
    public Vector3 normalAtSpot;
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
        pointLight.intensity = Random.Range(8f, 15f);
        pointLight.range = Random.Range(5f, 20f);
        DoSpiral();
        pointLight.DOIntensity(0, Random.Range(3, 20)).SetLoops(-1, LoopType.Yoyo);
        // pointLight.DOIntensity()
        startPos = transform.position;
        rnd = Random.Range(0.5f, 3);
        
        
    }
    private void Update()
    {
        // refSpeed += Time.deltaTime;
        // float x = Mathf.Cos (refSpeed * rnd);
        // // float y = Mathf.Sin (refSpeed);
        // float z = Mathf.Sin (refSpeed * rnd);
        // transform.position = startPos + new Vector3 (x, 0, z);
    }


    
    IEnumerator MoveToNewPosition(Vector3 newPos)
    {
        yield return null;
    }

    void DoSpiral()
    {
        float duation = Random.Range(3, 10);
        
        // bool on = true;
        // float t = 0;
        transform.DOSpiral(duation, normalAtSpot, SpiralMode.ExpandThenContract, Random.Range(0.05f, 0.2f), 5, 0.5f).SetLoops(2, LoopType.Yoyo). SetDelay(Random.Range(1, 100)).OnComplete(DoSpiral);

        // while (on)
        // {
        //     
        //     yield return null;
        // }
    }
    
    

    
}
