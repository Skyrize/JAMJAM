using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    AirplaneComponent airplaneController;

    private void Awake()
    {
        airplaneController = GetComponent<AirplaneComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        airplaneController.IsFlying = Input.GetKey(KeyCode.Space);
    }
}
