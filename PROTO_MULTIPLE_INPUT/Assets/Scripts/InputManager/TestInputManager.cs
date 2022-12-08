using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputManager : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (InputManager.XButton())
        {
            Debug.Log(InputManager.MainJoystick());
        }
    }
}
