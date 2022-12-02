using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class PlayerInput_System : SystemBase
{
    protected override void OnCreate()
    {
        EntityManager.AddComponent<ForwardPlayerInput_Component>(SystemHandle);

    }

    protected override void OnUpdate()
    {
        var forwardImput = new ForwardPlayerInput_Component
        {
            up = Input.GetKey(KeyCode.W),
            right = Input.GetKey(KeyCode.D),
            left = Input.GetKey(KeyCode.A),

            space = Input.GetKey(KeyCode.Space)
        };

        EntityManager.SetComponentData(SystemHandle, forwardImput);

        
    }
}
