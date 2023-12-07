using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrintDrawer : MonoBehaviour
{
    [SerializeField]
    private BluePrintLine linePrefab; // A Line osztály prefabja, amit a Unity szerkesztőben kell hozzárendelni

    private List<Vector3> bluePrintPoints; // A pontok listája, ahol a vonalakat kirajzoljuk

    BluePrintDrawer(List<Vector3> bluePrintPoints)
    {
        this.bluePrintPoints = bluePrintPoints;
        this.bluePrintPoints.Add(bluePrintPoints[0]);
    }

    public void Init(List<Vector3> bluePrintPoints)
    {
        this.bluePrintPoints = bluePrintPoints;
        bluePrintPoints.Add(this.bluePrintPoints[0]);
    }

    void Start()
    {
        bluePrintPoints = new List<Vector3>();
    }

    public void DrawBluePrint()
    {
        if (linePrefab == null)
        {
            Debug.LogError("A linePrefab nincs beállítva!");
            return;
        }

        BluePrintLine currentLine = null;

        for (int i = 0; i < bluePrintPoints.Count; i++)
        {
            if (i == 0 || Vector3.Distance(bluePrintPoints[i - 1], bluePrintPoints[i]) > .1)
            {
                if (currentLine == null)
                {
                    currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                }
                currentLine.UpdateLine(bluePrintPoints[i]);
            }
            else
            {
                currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                currentLine.UpdateLine(bluePrintPoints[i]);
            }
        }
    }
}
