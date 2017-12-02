using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PointMaker : MonoBehaviour
{
    [SerializeField]
    LineRenderer m_lr;

    // Use this for initialization
    void Start()
    {
		
    }
	
    // Update is called once per frame

    [ExecuteInEditMode]
    void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.T))
        {
            Debug.Log("d");
            m_lr.positionCount++;
            m_lr.SetPosition(m_lr.positionCount - 1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3[] curvedPoints = new Vector3[m_lr.positionCount ];
            m_lr.GetPositions(curvedPoints);
            curvedPoints = CurveLine(new List<Vector3>(curvedPoints));
            List<Vector3> scum = new List<Vector3>(curvedPoints);
            scum.Add(curvedPoints[0]);
            curvedPoints = scum.ToArray();
            m_lr.positionCount = curvedPoints.Length;// + 1;
            m_lr.SetPositions(curvedPoints);
        }
            
    }

    Vector3[] CurveLine(List<Vector3> _OGpoints)
    {
        List<Vector3> returnMe = new List<Vector3>();

        for (int i = 0; i < _OGpoints.Count + 1; i++)
        {
            Vector3 pAminus1 = _OGpoints[GetCorrectedPoint(i - 1, _OGpoints)];
            Vector3 pA = _OGpoints[GetCorrectedPoint(i, _OGpoints)];                //Really this is first point
            Vector3 pB = _OGpoints[GetCorrectedPoint(i + 1, _OGpoints)];            //And this is the second point

            if (i > _OGpoints.Count - 1)
            {
                pB = returnMe[0]; 
            }
            //returnMe.Add(pA + ((pAminus1 - pA) * 0.25f));
            returnMe.Add(pA + ((pB - pA) * 0.45f));
        }
        return returnMe.ToArray();
    }

    int GetCorrectedPoint(int _index, List<Vector3> _array)
    {
        if (_index < 0)
            return _array.Count + _index;
        if (_index > _array.Count - 1)
            return _index - _array.Count;
        
        return _index;
    }

    int GetCorrectedPoint(int _index, Vector3[] _array)
    {
        if (_index < 0)
            return _array.Length + _index;
        if (_index > _array.Length - 1)
            return _index - _array.Length;

        return _index;
    }

    int distanceToNearestPointSquared(Vector3 point, Vector3[] points)
    {
        long nearestDistanceSquared = long.MaxValue;

        foreach (var p in points)
        {
            int dx = p.X - point.X;
            int dy = p.Y - point.Y;

            long distanceSquared = dx * dx + dy * dy;

            nearestDistanceSquared = Math.Min(nearestDistanceSquared, distanceSquared);
        }

        return nearestDistanceSquared;
    }
}
