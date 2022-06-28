using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{

    private static BallShooter _instance;
    private int _ballsDestroyed = 0;

    public static BallShooter Instance
    {
        private set { _instance = value; }
        get { return _instance; }
    }

    public GameObject BallPrefab;
    public Text TimeLogField;
    public Text BallsLog;


    public float TimeStep;
    public float Velocity = 2;
    private float _timeAccumulated;
    private float _fullPressedTime;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBall();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _timeAccumulated += Time.deltaTime;
            _fullPressedTime += Time.deltaTime; 

            if (_timeAccumulated >= TimeStep)
            {
                _timeAccumulated = 0;

                ShootBall();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _timeAccumulated = 0;
        }
        TimeLogField.text = _fullPressedTime.ToString();


        BallsLog.text = _ballsDestroyed.ToString();
    }


    private void ShootBall()
    {
        var ball = Instantiate(BallPrefab);
        ball.GetComponent<BallBehaviour>().Velocity = Velocity;
        ball.transform.position = TrajectoryManager.Instance.Leftmost;
    }

    public void OnBallDestroyed()
    {
        _ballsDestroyed++;
    }
}


