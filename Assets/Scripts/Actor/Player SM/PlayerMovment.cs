using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
	private Rigidbody Rigidbody = default;
	[SerializeField] private ActorStats ActorStats = default;
	private ActorModel ActorAnimator = default;

	private void Awake()
	{
		Rigidbody = GetComponentInParent<Rigidbody>();
		ActorAnimator = Rigidbody.GetComponentInChildren<ActorModel>();
	}

	private void OnMoveInput(InputAction InputAction)
	{
		Vector3 Velocity = default;
		if (InputAction != null && ActorStats.TryGetStat("Movment Speed", out Stat MS))
		{
			var V2 = InputAction.ReadValue<Vector2>();
			if (V2 != Vector2.zero)
			{
				var V3 = Camera.main.transform.TransformDirection(new Vector3(V2.x, 0, V2.y));
				V3.y = 0;
				V3.Normalize();
				Velocity = V3 * MS.Value;
				Rigidbody.MovePosition(Rigidbody.position + Velocity * Time.fixedDeltaTime);
			}
		}
		ActorAnimator.AnimateVelocity(Rigidbody.transform.InverseTransformVector(Velocity), 0.1f, Time.fixedDeltaTime);
	}
}
