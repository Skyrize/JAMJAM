using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorComponent : MonoBehaviour
{
    [SerializeField] float m_xThresholdActivation = 50;
    [SerializeField] float m_xThresholdDestruction= 50;

    List<ActivatableComponent> m_activatables;

    Vector3 m_position => transform.position;
    ActivatableComponent m_activatable => m_activatables[m_currentIndex];
    Vector3 m_activatablePosition => m_activatable.transform.position;
    private void Awake()
    {
        m_activatables = new List<ActivatableComponent>(FindObjectsByType<ActivatableComponent>(FindObjectsInactive.Include, FindObjectsSortMode.None));

        foreach (ActivatableComponent component in m_activatables)
        {
            float xDistance = Mathf.Abs(m_position.x - component.transform.position.x);
            if (xDistance > m_xThresholdActivation)
            {
                component.gameObject.SetActive(false);
            }
        }
    }

    int m_currentIndex = 0;
    // Update is called once per frame
    void Update()
    {
        if (m_activatables.Count == 0)
            return;

        float xDistance = Mathf.Abs(m_position.x - m_activatablePosition.x);
        if (xDistance <= m_xThresholdActivation)
        {
            m_activatable.gameObject.SetActive(true);
            m_activatable.DestroyByDistance(transform, m_xThresholdDestruction);

            m_currentIndex++;
            if (m_currentIndex == m_activatables.Count)
            {
                m_activatables.Clear();
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (ActivatableComponent component in FindObjectsByType<ActivatableComponent>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            float xDistance = m_position.x - component.transform.position.x;

            if (Mathf.Abs(xDistance) <= m_xThresholdActivation)
            {
                Gizmos.color = xDistance > 0 ? Color.magenta : Color.red;
                Gizmos.DrawLine(m_position, component.transform.position);
            }
            else
            {
                Gizmos.color = Color.grey;
                Gizmos.DrawLine(m_position, component.transform.position);
            }
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(m_position, transform.right * m_xThresholdActivation);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(m_position, -transform.right * m_xThresholdDestruction);
    }
}
