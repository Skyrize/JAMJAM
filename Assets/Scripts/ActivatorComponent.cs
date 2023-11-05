using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivatorComponent : MonoBehaviour
{
    List<ActivatableComponent> m_activatables;

    Vector3 m_position => transform.position;
    ActivatableComponent m_activatable => m_activatables[m_currentIndex];
    private void Start()
    {
        var activatables = FindObjectsByType<ActivatableComponent>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        m_activatables = activatables.OrderBy(activable => activable.activatePos).ToList();
    }

    int m_currentIndex = 0;
    // Update is called once per frame
    void Update()
    {
        if (m_activatables.Count == 0)
            return;

        while (m_activatables.Count > 0 && m_position.x - m_activatable.activatePos > 0.0f)
        {
            m_activatable.Activate();

            m_currentIndex++;
            if (m_currentIndex == m_activatables.Count)
            {
                m_activatables.Clear();
            }
        }
    }
    /*
    private void OnDrawGizmos()
    {
        foreach (ActivatableComponent component in FindObjectsByType<ActivatableComponent>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            float xDistance = m_position.x - component.transform.position.x;

            if (Mathf.Abs(xDistance) <= GameManager.airplane.horizontalSpeed * m_activatableTimeOffset)
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
    */
}
