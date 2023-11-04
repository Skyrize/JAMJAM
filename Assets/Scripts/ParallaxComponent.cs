using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParallaxComponent : MonoBehaviour
{
    SpriteRenderer[] m_backgrounds = new SpriteRenderer[2];

    float m_xOffset;

    Transform m_camera;
    float m_replaceThreshold => m_backgrounds[1].transform.position.x;
    float m_y => m_backgrounds[1].transform.transform.position.y;
    float m_z => m_backgrounds[1].transform.transform.position.z;
    float m_currentPosition => m_camera.transform.position.x;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main.transform;
        SetupBackgrounds();
    }

    void SetupBackgrounds()
    {
        m_backgrounds = GetComponentsInChildren<SpriteRenderer>();
        if (m_backgrounds.Length != 2)
            throw new UnityException("Missing backgrounds");
        m_xOffset = m_backgrounds[0].sprite.bounds.size.x * transform.lossyScale.x;
        ReplaceBackground();
    }

    void ReplaceBackground()
    {
        float xExcess = m_replaceThreshold - m_currentPosition;
        Vector3 basePosition = new Vector3(m_camera.position.x - xExcess, m_y, m_z);
        m_backgrounds[0].transform.position = basePosition;
        basePosition.x += m_xOffset;
        m_backgrounds[1].transform.position = basePosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (m_currentPosition >= m_replaceThreshold)
        {
            ReplaceBackground();
        }
    }
}
