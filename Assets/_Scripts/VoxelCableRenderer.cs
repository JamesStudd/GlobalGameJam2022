using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelCableRenderer : MonoBehaviour
{
    public ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] voxels;
    bool voxelsUpdated = false;

    public int WayScale = 25;

    public float voxelScale = 0.1f;
    public float scale = 1f;

    public Transform PointA;
    public Transform PointB;
    public float gridsize = 0.2f;
    public Color CableColor;

    private void Start()
    {
        CalculatePoints();
    }

    [ContextMenu("Calculate")]
    void CalculatePoints()
    {
        Vector3[] points = new Vector3[WayScale];
        var distance = Vector3.Distance(PointA.position, PointB.position);
        var step = distance / WayScale;
        for (int i = 0; i < WayScale; i++)
        {
            points[i] = GetPointOnVector(PointA.position, PointB.position, step*i);
            points[i] = new Vector3(Mathf.Sin(60)* points[i].x, points[i].y,points[i].z);
        }
        SetVoxels(points, CableColor);
    }

    public Vector3 GetPointOnVector(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = x * Vector3.Normalize(B - A) + A;
        return P;
    }

    void Update()
    {
        if(voxelsUpdated)
        {
            _particleSystem.SetParticles(voxels, voxels.Length);
            voxelsUpdated = false;
        }
    }

    public void SetVoxels(Vector3[] positions, Color colors)
    {
        voxels = new ParticleSystem.Particle[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            voxels[i].position = positions[i] * scale;
            voxels[i].startColor = colors;
            voxels[i].startSize = voxelScale;
        }
        Debug.Log("Voxels set! Voxel count: " + voxels.Length);
        voxelsUpdated = true;
    }
}
