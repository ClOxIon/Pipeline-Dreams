using UnityEngine;

namespace PipelineDreams {
    public class MeleeAttackClip : StateMachineBehaviour {
        [SerializeField][Range(0,1f)]float MeleeAttackEventTime;
        bool flag1 = false;
        
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime > MeleeAttackEventTime && !flag1)
            {

                animator.GetComponentInParent<Entity.Animator>().OnAnimEvent?.Invoke("MeleeAttack");
                flag1 = true;

            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.GetComponentInParent<Entity.Animator>().OnAnimEvent?.Invoke("ClipEnd");
            flag1 = false;
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}