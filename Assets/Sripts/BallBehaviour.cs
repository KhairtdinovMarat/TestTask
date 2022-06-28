using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    Transform _transform;
    private float x;
    public float Velocity = 1f;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        x += Time.deltaTime * Velocity;
        _transform.position = TrajectoryManager.Instance.GetPosition(TrajectoryManager.Instance.Leftmost.x+x);
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        BallShooter.Instance.OnBallDestroyed();
    }
}
