using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }
    public static AirplaneComponent airplane => instance.player.airplayController;
    
    [SerializeField] float m_worldTopBoundary = 10;
    [SerializeField] float m_worldBotBoundary = -10;
    PlayerController m_player;

    public PlayerController player => m_player;

    float playerAltitude => m_player.transform.position.y;

    private void Awake()
    {
        m_player = FindAnyObjectByType<PlayerController>();
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAltitude > m_worldTopBoundary || playerAltitude < m_worldBotBoundary)
        {
            //Todo : kill player + real
            ReloadCurrentScene();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Vector3.up * m_worldTopBoundary, Vector3.right * 10000000);
        Gizmos.DrawRay(Vector3.up * m_worldTopBoundary, -Vector3.right * 10000000);

        Gizmos.DrawRay(Vector3.up * m_worldBotBoundary, Vector3.right * 10000000);
        Gizmos.DrawRay(Vector3.up * m_worldBotBoundary, -Vector3.right * 10000000);
    }
}
