using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public struct PosNormPair
{
    public Vector3 pos;
    public Vector3 normal;
}
public class FireflySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fireflyPrefab;
    public List<SpawnOnMesh> spawnOnMeshes;
    public int count;

    [Serializable]
    public class SpawnOnMesh{
        public int count;
        public Transform trans;
    }
    void Awake()
    {
        foreach (var mesh in spawnOnMeshes)
        {
            var parent = new GameObject().transform;
            parent.position = mesh.trans.position;

            for (int i = 0; i < mesh.count; i++)
            {
                var fly = Instantiate(fireflyPrefab, parent);
                var pN = GetRandomPointOnMesh(mesh.trans.GetComponentInChildren<MeshFilter>().sharedMesh);
                fly.transform.position = pN.pos + mesh.trans.position + (pN.normal * 0.5f);
                fly.GetComponent<FireflyBehaviour>().normalAtSpot = pN.normal;
            }
                // var temp = Vector3.Scale(fly.transform.position, spawnOnMesh.transform.lossyScale) ;
                // temp = spawnOnMesh.transform.position;
                // fly.transform.position = temp;
           

                // parent.transform.localScale = mesh.trans.localScale;
                // parent.transform.position -= spawnOnMesh.transform.position;
                parent.transform.rotation = mesh.trans.rotation;
        }
    }

    
    
   PosNormPair GetRandomPointOnMesh(Mesh mesh)
    {
        //if you're repeatedly doing this on a single mesh, you'll likely want to cache cumulativeSizes and total
        float[] sizes = GetTriSizes(mesh.triangles, mesh.vertices);
        float[] cumulativeSizes = new float[sizes.Length];
        float total = 0;

        for (int i = 0; i < sizes.Length; i++)
        {
            total += sizes[i];
            cumulativeSizes[i] = total;
        }

        //so everything above this point wants to be factored out

        float randomsample = Random.value* total;

        int triIndex = -1;
        
        for (int i = 0; i < sizes.Length; i++)
        {
            if (randomsample <= cumulativeSizes[i])
            {
                triIndex = i;
                break;
            }
        }

        if (triIndex == -1) Debug.LogError("triIndex should never be -1");

        Vector3 a = mesh.vertices[mesh.triangles[triIndex * 3]];
        Vector3 b = mesh.vertices[mesh.triangles[triIndex * 3 + 1]];
        Vector3 c = mesh.vertices[mesh.triangles[triIndex * 3 + 2]];

        Vector3 aN = mesh.normals[mesh.triangles[triIndex * 3]];
        Vector3 bN = mesh.normals[mesh.triangles[triIndex * 3 + 1]];
        Vector3 cN = mesh.normals[mesh.triangles[triIndex * 3 + 2]];

        //generate random barycentric coordinates

        float r = Random.value;
        float s = Random.value;

        if(r + s >=1)
        {
            r = 1 - r;
            s = 1 - s;
        }
        //and then turn them back to a Vector3
        Vector3 pointOnMesh = a + r*(b - a) + s*(c - a);
        Vector3 normalOnMesh = aN + r*(bN - aN) + s*(cN - aN);
        var pN = new PosNormPair()
        {
            pos = pointOnMesh,
            normal = normalOnMesh,
        };
        return pN;

    }

    float[] GetTriSizes(int[] tris, Vector3[] verts)
    {
        int triCount = tris.Length / 3;
        float[] sizes = new float[triCount];
        for (int i = 0; i < triCount; i++)
        {
            sizes[i] = .5f*Vector3.Cross(verts[tris[i*3 + 1]] - verts[tris[i*3]], verts[tris[i*3 + 2]] - verts[tris[i*3]]).magnitude;
        }
        return sizes;
    }
}
