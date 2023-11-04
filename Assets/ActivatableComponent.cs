using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableComponent : MonoBehaviour
{
    Transform m_reference;
    float m_distanceForDestruction;
    public void DestroyByDistance(Transform _reference, float _distance)
    {
        m_reference = _reference;
        m_distanceForDestruction = _distance;
    }

    private void Update()
    {
        if (m_reference)
        {
            float xDist = m_reference.position.x - transform.position.x;
            if (xDist > m_distanceForDestruction)
            {
                // Todo: real
                Destroy(gameObject);
            }
        }
    }
}
