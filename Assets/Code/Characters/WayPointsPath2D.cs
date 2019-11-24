//
//   CONTRIBUTORS: Montana Games (www.montana-games.com)                  
//   Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com    
//   -------------------------------------------------------------------------------------------------
//

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Универсальный компонент для Путей следования персонажами, или поведения перемещающихся платформ
/// </summary>
public class WayPointsPath2D: MonoBehaviour
{
    [Range(.1f, 2f)]
    [HideInInspector]
    [Header("Настройки редактора")]
    public float controlSize = 0.2f;
    public Color32 controlStyle = Color.white;

    [Header("Настройки пути следования и времени остановки")]
    public List<Vector3> PathPoints = new List<Vector3>();
    public List<float> Times = new List<float>();


    private int currentPoint = 0;
    private bool firstStart = true;
    public bool ShowTooltips = true;
    public bool LoopPath = false;

    public bool Loop
    {
        get { return LoopPath; }
        set { LoopPath = value; }
    }

       
    public Vector3 GetNextPoint()
    {
        if (!firstStart)
            ++currentPoint;
        else
            firstStart = false;

        if (currentPoint >= PathPoints.Count)
        {
            if (LoopPath)
                currentPoint = 0;
            else
                currentPoint = GetPathPointsCount()-1;
        }
            
        return PathPoints[currentPoint];
    }
    public Vector3[] GetAllPoints()
    {
        return PathPoints.ToArray();
    }
    public float[] GetAllPointsTime()
    {
        return Times.ToArray();
    }
    public void ResetPathIndex()
    {
        currentPoint = 0;
    }

    public int GetPathPointsCount()
    {
        return PathPoints.Count;
    }

    public Vector3 GetPointByIndex(int index)
    {
        return PathPoints[index];
    }
    public float GetTimeByIndex(int index)
    {
        return Times[index];
    }

    public void SetPointByIndex(int index,Vector3 value)
    {
        PathPoints[index] = value;
    }
    public void SetTimeByIndex(int index, float value)
    {
        Times[index] = value;
    }
    public float GetCurrentPointTime()
    {
        return Times[currentPoint];
    }

    public void AddNewPathPoint(Vector3 point)
    {
        point.z = transform.position.z;
        PathPoints.Add(point);
        Times.Add(0f);
    }

    public void RemovePointByIndex(int index)
    {
        PathPoints.RemoveAt(index);
        Times.RemoveAt(index);
    }

    public void SetCurrentPointWaiteTime(float newTme,int index)
    {
        Times[index] = newTme;
    }


}
