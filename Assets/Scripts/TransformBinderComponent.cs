using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TransformBinderComponent : MonoBehaviour
{
    [SerializeField] Transform m_target;
    [SerializeField] bool m_followX;
    [SerializeField] bool m_followY;

    Vector2 m_basePosition;

    private void Awake()
    {
        m_basePosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        Follow();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {
        Vector2 newPos = m_basePosition;

        if (m_followX)
            newPos.x = m_target.position.x;
        if (m_followY)
            newPos.y = m_target.position.y;

        transform.position = newPos;
    }
}
