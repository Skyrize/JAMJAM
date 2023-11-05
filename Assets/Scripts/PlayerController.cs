using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    AirplaneComponent m_airplaneController;
    public AirplaneComponent airplayController => m_airplaneController; 

    private void Awake()
    {
        m_airplaneController = GetComponent<AirplaneComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        m_airplaneController.IsFlying = Input.GetKey(KeyCode.Space);
    }
}
