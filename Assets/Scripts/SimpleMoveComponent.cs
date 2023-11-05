using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveComponent : MonoBehaviour
{
    [SerializeField] private Vector2 m_direction = Vector2.right;
    [SerializeField] private float m_speed = 1f;
    [SerializeField] private bool m_speedRelativeToAirplane = true;

    // Update is called once per frame
    void Update()
    {
        
        transform.position += computeMoveOffset(Time.deltaTime);    
    }

    public Vector3 computeMoveOffset(float _deltaTime)
    {
        Vector3 movement = m_direction * m_speed * _deltaTime;
        if(m_speedRelativeToAirplane)
            movement += Vector3.right * GameManager.airplane.horizontalSpeed * _deltaTime;
        return movement;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, m_direction * m_speed);
    }
}
