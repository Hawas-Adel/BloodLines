using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GroundedStateMonitor), typeof(CapsuleCollider))]
public class ActorLocomotion : MonoBehaviour
{
	[SerializeField] private GroundedStateMonitor GSM;
	[SerializeField] private Rigidbody RB;

	[SerializeField] private Stat MovmentSpeed;
	[CurveRange(-1, 0, 1, 1)]
	[SerializeField] private AnimationCurve MovmentSpeedAngularMultiplier = new AnimationCurve(new Keyframe(-1, 0.5f), new Keyframe(1, 1));

	[SerializeField] private Stat JumpForce;

	[SerializeField] [Min(0)] private float AngularSpeed = 270f;
	[SerializeField] [Min(0)] private float UnGroundedDrag = 0.01f;

	private Transform MainCamera;
	private Vector3 LastMovmentDir;
	private int SpeedX;
	private int SpeedY;
	private int SpeedZ;
	private int IsGrounded;

	private void Awake()
	{
		SpeedX = Animator.StringToHash("Speed X");
		SpeedY = Animator.StringToHash("Speed Y");
		SpeedZ = Animator.StringToHash("Speed Z");
		IsGrounded = Animator.StringToHash("Is Grounded");

		MainCamera = Camera.main.transform;
	}


	public void ReSizeCapsule(CapsuleCollider Capsule, Stat Height)
	{
		Capsule.height = Height.Value;
		Capsule.center = Vector3.up * (Capsule.height * 0.5f);
	}
	public void UpdateRigidbodyWeight(Stat Weight) => RB.mass = Weight.Value;

	public void Move(Vector2 DirXZ, float dT, bool Rotate, Transform WorldForwardOverride = null)
	{
		if (GSM.IsGrounded)
		{
			Vector3 Dir = new Vector3(DirXZ.x, 0, DirXZ.y);
			if (WorldForwardOverride)
			{
				Dir = WorldForwardOverride.TransformDirection(Dir);
				Dir.y = 0;
				Dir.Normalize();
			}
			Move_Internal(Dir, dT);
			if (Rotate)
				RotateDir(Dir, dT);
			else if (WorldForwardOverride)
				RotateDir(Vector3.ProjectOnPlane(WorldForwardOverride.forward, Vector3.up).normalized, dT);
		}
	}
	public void Move(Vector3 Dir, float dT)
	{
		if (GSM.IsGrounded)
		{
			Move_Internal(Dir, Time.fixedDeltaTime);
		}
	}
	private void Move_Internal(Vector3 Dir, float dT)
	{
		Dir.y = 0;
		Dir.Normalize();

		if (GSM.IsGrounded)
			LastMovmentDir = Dir;
		else
			LastMovmentDir *= 1 - UnGroundedDrag * dT;

		Vector3 Velocity = LastMovmentDir * GetFinalMovmentSpeed(LastMovmentDir);
		RB.MovePosition(RB.position + Velocity * dT);

		//if (Actor.Animator)
		//{
		//	Velocity = transform.InverseTransformVector(Velocity);
		//	Actor.Animator.SetFloat(SpeedX, Velocity.x);
		//	Actor.Animator.SetFloat(SpeedY, RB.velocity.y);
		//	Actor.Animator.SetFloat(SpeedZ, Velocity.z);
		//}
	}
	public void RotateDir(Vector3 Dir, float dT)
	{
		if (Dir != Vector3.zero)
		{
			RB.MoveRotation(Quaternion.RotateTowards(RB.rotation, Quaternion.LookRotation(Dir, Vector3.up), AngularSpeed * dT));
		}
	}

	private float GetFinalMovmentSpeed(Vector3 Dir) => MovmentSpeed.Value * MovmentSpeedAngularMultiplier.Evaluate(Vector3.Dot(Dir, RB.transform.forward));

	private void FixedUpdate()
	{
		if (!GSM.IsGrounded)
		{
			Move_Internal(LastMovmentDir, Time.fixedDeltaTime);
		}
	}

	public void Jump()
	{
		if (GSM.IsGrounded)
		{
			RB.AddForce(Vector3.up * JumpForce.Value);
		}
	}

	public void OnMovmentInput(InputAction IA, bool RotateWithMovment) => Move(IA.ReadValue<Vector2>(), Time.fixedDeltaTime, RotateWithMovment, MainCamera);
}
