using UnityEngine;

public class PlayerRotateWithCamera : MonoBehaviour
{
	private Rigidbody Rigidbody = default;
	private Transform MainCamera = default;
	[SerializeField] private float AngularSpeed = 240;

	private void Awake()
	{
		Rigidbody = GetComponentInParent<Rigidbody>();
		MainCamera = Camera.main.transform;
	}

	private void FixedUpdate() => Rigidbody.MoveRotation(Quaternion.RotateTowards(Rigidbody.rotation, Quaternion.AngleAxis(MainCamera.eulerAngles.y, Vector3.up), AngularSpeed * Time.fixedDeltaTime));
}
