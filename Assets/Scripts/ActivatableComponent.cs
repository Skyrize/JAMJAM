using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableComponent : MonoBehaviour
{
    [SerializeField] private float m_activationTimeOffset = 0.0f;
    [SerializeField] private float m_lifeTime = 10.0f;
    private Vector3 m_origin;

    public float activatePos => m_origin.x - m_activationTimeOffset * GameManager.airplane.horizontalSpeed;
    
    Transform m_reference;
    float m_distanceForDestruction;

    public void Activate()
    {
        gameObject.SetActive(true);
        StartCoroutine(StartLifeTime());

    }

    private IEnumerator StartLifeTime()
    {
        yield return new WaitForSeconds(m_lifeTime);
        Destroy(gameObject);
    }
    
    private void Start()
    {
        m_origin = transform.position;
        if (TryGetComponent(out SimpleMoveComponent simpleMoveComponent))
            transform.position = m_origin - simpleMoveComponent.computeMoveOffset(m_activationTimeOffset);
        
        gameObject.SetActive(false);
    }
}
