using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveComponent : MonoBehaviour
{
    [SerializeField] private Vector2 m_direction = Vector2.right;
    [SerializeField] private float m_speed = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = m_direction * m_speed * Time.deltaTime;
        transform.position += movement;    
    }
}
