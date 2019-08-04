using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBehaviour : MonoBehaviour
{
    public CharacterController CharacterController;

    public void InEndedThrow()
    {
        CharacterController.SetEndThrow();
    }
}
