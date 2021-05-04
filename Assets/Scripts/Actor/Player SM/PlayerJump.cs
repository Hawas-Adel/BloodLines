using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
	private Rigidbody Rigidbody = default;
	[SerializeField] private ActorStats ActorStats = default;
	//private ActorAnimator ActorAnimator = default;

	private void Awake() => Rigidbody = GetComponentInParent<Rigidbody>();//ActorAnimator = actor.GetComponentInChildren<ActorAnimator>();

	private void JumpAction_performed(InputAction.CallbackContext obj)
	{
		if (ActorStats.TryGetStat("Jump Force", out Stat JF))
		{
			Rigidbody.AddForce(0, JF.Value, 0);
			//ActorAnimator.AnimateJump();
		}
	}
}
