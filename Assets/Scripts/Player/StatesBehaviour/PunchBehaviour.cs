using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBehaviour : StateMachineBehaviour
{
    public enum TPunchType
    {
        LEFT_HAND=0,
        RIGHT_HAND,
        FOOT
    }

    [HideInInspector] private PlayerController playerController;
    [SerializeField] private float startPctTime;
    [SerializeField] private float endPctTime;
    [SerializeField] private TPunchType punchType = TPunchType.LEFT_HAND;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        bool enableHandPunch = stateInfo.normalizedTime > startPctTime && stateInfo.normalizedTime < endPctTime;
        if (punchType == TPunchType.LEFT_HAND)
            playerController.EnableLeftHandPunch(enableHandPunch);

        if (punchType == TPunchType.RIGHT_HAND)
            playerController.EnableRightHandPunch(enableHandPunch);

        if (punchType == TPunchType.FOOT)
            playerController.EnableKick(enableHandPunch);

    }

}
