using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class MCAnimatorController : MonoBehaviour
{
    private Animator MCAnimator;
    private FirstPersonController firstPersonController;

    private int isWalkingHash;
    private int isRunningHash;
    private int isCastingHash;
    private int isJumpingHash;
    private int doActionHash;
    private int runningSpeedHash;


    // Start is called before the first frame update
    private void Start()
    {
        MCAnimator = GetComponent<Animator>();
        firstPersonController = GetComponentInParent<FirstPersonController>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isCastingHash = Animator.StringToHash("isCasting");
        isJumpingHash = Animator.StringToHash("isJumping");
        doActionHash = Animator.StringToHash("doingAction");
        runningSpeedHash = Animator.StringToHash("runningSpeed");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (MCAnimator != null)
        {
            //Movement Control
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            float runningSpeed = MCAnimator.GetFloat(runningSpeedHash);
            //Debug.Log(firstPersonController.CurrentSpeed);
            if (horizontal != 0 || vertical != 0)
            {
                MCAnimator.SetBool(isWalkingHash, true);

                if (firstPersonController.CurrentSpeed == runningSpeed)
                {
                    MCAnimator.SetBool(isRunningHash, true);
                }
                else
                    MCAnimator.SetBool(isRunningHash, false);
            }
            else
                MCAnimator.SetBool(isWalkingHash, false);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("space bar pressed");
                MCAnimator.SetTrigger(isJumpingHash);
            }

            //Action Control
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                MCAnimator.SetLayerWeight(1, 1f);
                if (Input.GetMouseButton(1))
                {
                    MCAnimator.SetTrigger(isCastingHash);
                }

                if (Input.GetMouseButton(0))
                {
                    MCAnimator.SetTrigger(doActionHash);
                }
            }
        }
    }
}
