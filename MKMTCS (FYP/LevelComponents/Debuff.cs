using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Debuff : MonoBehaviour
{
    private FirstPersonController firstPersonController;

    private void Start()
    {
        firstPersonController = GetComponent<FirstPersonController>();
    }

    public void AffectSpeed(float affectedSpeed)
    {
        firstPersonController.CurrentWalkingSpeed = affectedSpeed;
        firstPersonController.CurrentRunningSpeed = affectedSpeed;
        firstPersonController.CurrentJumpSpeed = affectedSpeed;

        Invoke("ReturnNormal", 5f);
    }

    private void ReturnNormal() 
    {
        firstPersonController.CurrentWalkingSpeed = 5;
        firstPersonController.CurrentRunningSpeed = 10;
        firstPersonController.CurrentJumpSpeed = 10;
    }
}
