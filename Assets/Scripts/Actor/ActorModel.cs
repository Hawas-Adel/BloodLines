using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ActorModel : MonoBehaviour
{
	[SerializeField] [AnimatorParam(nameof(Animator), AnimatorControllerParameterType.Int)] private int RandomizerParam = default;
	[Header("Locomotion")]
	[SerializeField] [AnimatorParam(nameof(Animator), AnimatorControllerParameterType.Float)] private int SpeedXParam = default;
	[SerializeField] [AnimatorParam(nameof(Animator), AnimatorControllerParameterType.Float)] private int SpeedZParam = default;
	[SerializeField] [AnimatorParam(nameof(Animator), AnimatorControllerParameterType.Bool)] private int IsGroundedParam = default;
	[SerializeField] [AnimatorParam(nameof(Animator), AnimatorControllerParameterType.Trigger)] private int JumpParam = default;
	[Header("Combat")]
	[SerializeField] [AnimatorParam(nameof(Animator), AnimatorControllerParameterType.Bool)] private int InCombatParam = default;
	[SerializeField] [AnimatorParam(nameof(Animator), AnimatorControllerParameterType.Trigger)] private int AttackParam = default;
	[SerializeField] [Min(1)] private int NoOfAttackAnimations = 1;
	[SerializeField] [AnimatorParam(nameof(Animator), AnimatorControllerParameterType.Trigger)] private int DeathParam = default;


	private Animator _Animator = default;
	public Animator Animator
	{
		get
		{
			if (!_Animator) _Animator = GetComponent<Animator>();
			return _Animator;
		}
	}

	public void AnimateIsGrounded(bool IsGrounded) => Animator.SetBool(IsGroundedParam, IsGrounded);
	public void AnimateVelocity(Vector3 RelativeVelocity, float DampTime, float DTime)
	{
		Animator.SetFloat(SpeedXParam, RelativeVelocity.x, DampTime, DTime);
		Animator.SetFloat(SpeedZParam, RelativeVelocity.z, DampTime, DTime);
	}
	public void AnimateJump() => Animator.SetTrigger(JumpParam);

	private void RandomizAnimation(int AnimationsCount) => Animator.SetInteger(RandomizerParam, Random.Range(0, AnimationsCount));
	private void UnRandomizAnimation(int AnimNO, int AnimationsCount) => Animator.SetInteger(RandomizerParam, Mathf.Min(AnimNO, AnimationsCount));
	private void HandleRandomizedAnimations(int AnimNO, int AnimationsCount)
	{
		if (AnimNO > -1) UnRandomizAnimation(AnimNO, AnimationsCount);
		else RandomizAnimation(AnimationsCount);
	}

	public void AnimateInCombat(bool InCombat) => Animator.SetBool(InCombatParam, InCombat);
	public void AnimateAttack(int AttackNO = -1)
	{
		HandleRandomizedAnimations(AttackNO, NoOfAttackAnimations);
		Animator.SetTrigger(AttackParam);
	}
	public void AnimateDeath()
	{
		Animator.SetTrigger(DeathParam);
		var RB = GetComponentInParent<Rigidbody>();
		System.Array.ForEach(RB.GetComponentsInChildren<Collider>(), c => c.isTrigger = true);
	}
}
