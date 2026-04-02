using UnityEngine;

public class IdleStateBehaviour : StateMachineBehaviour
{
    // Called when entering the Idle state
    public override void OnStateEnter(Animator animator,
        AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Change bike color to green when Idle
        var renderer = animator.GetComponent<Renderer>();
        renderer.material.color = Color.green;
    }
}