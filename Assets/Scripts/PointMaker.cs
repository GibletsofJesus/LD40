using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PointMaker : MonoBehaviour
{
    public static PointMaker instance;
    [SerializeField]
    LineRenderer m_lr;

    // Use this for initialization
    void Start()
    {
        instance = this;
    }
	
    // Update is called once per frame
    [ExecuteInEditMode]
    void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.T))
        {
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
        if (Input.GetKeyDown(KeyCode.H))
        {
            List<Vector3> pts = new List<Vector3>();
            for (int i = 0; i < m_lr.positionCount; i++)
            {
                if (i % 2 == 0)
                    pts.Add(m_lr.GetPosition(i));
            }
            m_lr.positionCount = pts.Count;
            m_lr.SetPositions(pts.ToArray());
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

    int lastPoint;

    public Vector3 WhereGo(Vector3 pos, float offset, bool dog)
    {
        Vector3[] points = new Vector3[m_lr.positionCount];
        m_lr.GetPositions(points);

        Vector3 goHere = m_lr.GetPosition(GetCorrectedPoint(FindClosestPoint(pos) + (dog ? 3 : 2), points));
        Vector3 vec = m_lr.GetPosition(GetCorrectedPoint(FindClosestPoint(pos) + (dog ? 4 : 3), points)) - goHere;
        vec.Normalize();
        vec = new Vector3(-vec.y, vec.x, 0);
        vec *= Mathf.Sin(Time.time + offset) / 2;
        if (dog)
            goHere += vec;
        return goHere;
    }

    int FindClosestPoint(Vector3 point)
    {
        float nearest = Mathf.Infinity;
        int returnMe = 0;
        for (int i = 0; i < m_lr.positionCount; i++)
        {
            if (i - lastPoint < 50)
            {
                float d = Vector3.Distance(point, m_lr.GetPosition(i));
                if (d < nearest)
                {
                    returnMe = i;
                    nearest = d;
                }
            }
        }
        lastPoint = returnMe;
        return returnMe;
    }
}
