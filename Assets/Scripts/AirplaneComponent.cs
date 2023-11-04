using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneComponent : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float m_horizontalSpeed = 4;
    // best name
    [SerializeField] float m_accelerationSpeed = 4;
    [SerializeField] float m_maxUpAngle = 35;
    [SerializeField] float m_maxDownAngle = -35;
    [SerializeField] float m_rotationLerpSpeed = 4;

    [Header("Fly")]
    [SerializeField] float m_flyMaxSpeed = 10;
    [SerializeField] float m_flyAcceleration = 3;
    [SerializeField] float m_flyTimeToReachAcceleration = 1;
    [SerializeField] AnimationCurve m_flyAccelerationCurve = new AnimationCurve();

    [Header("Fall")]
    [SerializeField] float m_fallMaxSpeed = 2;
    [SerializeField] float m_fallAcceleration = 2;
    [SerializeField] float m_fallTimeToReachAcceleration = 3;
    [SerializeField] AnimationCurve m_fallAccelerationCurve = new AnimationCurve();


    Rigidbody2D m_rigidBody;
    public bool IsFlying = false;
    float m_speed = 0;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    Vector3 lastPos = Vector3.zero;
    Vector3 smoothVelocity;
    private void FixedUpdate()
    {
        m_rigidBody.AddForce(transform.up * m_speed, ForceMode2D.Force);
        m_rigidBody.velocity = new Vector2(m_horizontalSpeed, Mathf.Clamp(m_rigidBody.velocityY, -m_fallMaxSpeed, m_flyMaxSpeed));

        Vector3 direction = transform.position - lastPos;
        if (direction.magnitude < Mathf.Epsilon)
            direction = transform.right;
        m_rigidBody.rotation = Mathf.Lerp(m_rigidBody.rotation, Vector3.SignedAngle(Vector3.right, direction, Vector3.forward), Time.fixedDeltaTime * m_rotationLerpSpeed);
        lastPos = transform.position;
    }

    void SetSpeed(float _newSpeed)
    {
        m_speed = Mathf.Lerp(m_speed, _newSpeed, Time.deltaTime * m_accelerationSpeed);
    }

    float m_timeFlying = 0;
    void UpdateFly()
    {
        m_timeFalling = 0;
        SetSpeed(m_flyAcceleration * m_flyAccelerationCurve.Evaluate(m_rigidBody.velocity.y / m_flyMaxSpeed));

        m_timeFlying += Time.deltaTime;
    }

    float m_timeFalling = 0;
    void UpdateFall()
    {
        m_timeFlying = 0;

        SetSpeed(-m_fallAcceleration * m_fallAccelerationCurve.Evaluate(-m_rigidBody.velocity.y / m_fallMaxSpeed));

        m_timeFalling += Time.deltaTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (IsFlying)
        {
            UpdateFly();
        }
        else
        {
            UpdateFall();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 maxUpDirection = Quaternion.AngleAxis(m_maxUpAngle, Vector3.forward) * transform.right;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(maxUpDirection * 3));
        Gizmos.color = Color.red;
        Vector2 maxDownDirection = Quaternion.AngleAxis(m_maxDownAngle, Vector3.forward) * transform.right;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(maxDownDirection * 3));
    }
}
