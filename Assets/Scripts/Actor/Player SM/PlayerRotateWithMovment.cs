using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotateWithMovment : MonoBehaviour
{
	private Rigidbody Rigidbody = default;
	private Transform MainCamera = default;
	[SerializeField] private float AngularSpeed = 240;

	private void Awake()
	{
		Rigidbody = GetComponentInParent<Rigidbody>();
		MainCamera = Camera.main.transform;
	}

	private void OnMoveInput(InputAction InputAction)
	{
		if (InputAction != null && Rigidbody && MainCamera)
		{
			var V2 = InputAction.ReadValue<Vector2>();
			if (V2 != Vector2.zero)
			{
				var V3 = MainCamera.TransformDirection(new Vector3(V2.x, 0, V2.y));
				V3.y = 0;
				V3.Normalize();
				Rigidbody.MoveRotation(Quaternion.RotateTowards(Rigidbody.rotation, Quaternion.LookRotation(V3, Vector3.up), AngularSpeed * Time.fixedDeltaTime));
			}
		}
	}
}
