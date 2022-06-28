using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TrajectoryManager : MonoBehaviour
{
    private static TrajectoryManager _instance;

    public static TrajectoryManager Instance
    {
        private set { _instance = value; }
        get { return _instance; }
    }

    public float Amplitude, Frequency, Step;

    private LineRenderer _renderer;

    private Vector3 _leftmost = Vector3.zero, 
                    _rightmost = Vector3.zero;

    private Vector3[] _positions;

    public Vector3 Leftmost => _leftmost;
    public Vector3 Rightmost => _rightmost;

    private bool _boundsChanged = false;

    private void Awake()
    {
        Instance = this;

        _renderer = gameObject.AddComponent<LineRenderer>();
        _renderer.startWidth = _renderer.endWidth = 0.01f;
    }

    private void Update()
    {
        CheckParameters();
    }

    private void CheckParameters()
    {
        var newLeftmost = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var newRightmost = Camera.main.ScreenToWorldPoint(Vector3.right * Camera.main.scaledPixelWidth);
        if (newLeftmost != _leftmost || newRightmost != _rightmost)
        {
            _leftmost = newLeftmost;
            _rightmost = newRightmost;

            _boundsChanged = true;
        }

        DrawTrajectory();

        _boundsChanged = false;
    }

    public Vector3 GetPosition(float x)
    {
        return new Vector3(x, Mathf.Sin(x * Frequency) * Amplitude, 0f);
    }

    private void DrawTrajectory()
    {
        _positions = new Vector3[(int)((_rightmost - _leftmost).x / Step)];

        _renderer.positionCount = _positions.Length;
        _renderer.SetPositions(CalculatePoints());
    }
    

    private Vector3[] CalculatePoints()
    {
        var positions = new Vector3[_positions.Length];
        for(int i = 0; i < _positions.Length; i++)
        {
            positions[i] = GetPosition((Leftmost + Vector3.right * Step * i).x);
        }
        return positions;
    }
}
