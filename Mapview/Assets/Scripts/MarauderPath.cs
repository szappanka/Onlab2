using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarauderPath : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    private Drawing drawing;

    public void Draw()
    {
        Debug.Log("Draw");
        if(lineRenderer == null)
        {
            Debug.Log("Drawing is null");
            return;
        }

        lineRenderer.positionCount = drawing.coordinates.Count();
        Debug.Log(drawing.coordinates.Count());
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, drawing.coordinates[i]);
        }
    }

    public void SetDrawing(Drawing d)
    {
        drawing = d;
    }

    public override string ToString()
    {
        return $"Az út: {drawing}";
    }
}
