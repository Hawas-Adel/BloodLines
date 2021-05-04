using UnityEngine;

public class PreserveMovmentMomentem : MonoBehaviour
{
	[SerializeField] private UnityEngine.InputSystem.InputActionReference InputAction = default;
	private Rigidbody Rigidbody = default;
	[SerializeField] private ActorStats ActorStats = default;
	private Vector3 Velocity = default;

	private void Awake() => Rigidbody = GetComponentInParent<Rigidbody>();

	private void OnEnable()
	{
		if (InputAction && ActorStats.TryGetStat("Movment Speed", out Stat MS))
		{
			var V2 = InputAction.action.ReadValue<Vector2>();
			if (V2 != Vector2.zero)
			{
				var V3 = Camera.main.transform.TransformDirection(new Vector3(V2.x, 0, V2.y));
				V3.y = 0;
				V3.Normalize();
				Velocity = V3 * MS.Value;
			}
			else
			{
				Velocity = Vector3.zero;
			}
		}
	}

	private void FixedUpdate()
	{
		if (Velocity != Vector3.zero)
		{
			Rigidbody.MovePosition(Rigidbody.position + Velocity * Time.fixedDeltaTime);
		}
	}
}
