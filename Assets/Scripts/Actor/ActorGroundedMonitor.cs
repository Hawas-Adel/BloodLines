using NaughtyAttributes;
using UnityEngine;

public class ActorGroundedMonitor : MonoBehaviour
{
	[SerializeField] [Min(0.01f)] private float Radius = 0.1f;
	[SerializeField] private LayerMask LayerMask = default;

	[ShowNativeProperty] public bool IsGrounded { get; private set; }


	private void FixedUpdate() => IsGrounded = Physics.CheckSphere(transform.position, Radius, LayerMask.value);

	private void OnDrawGizmosSelected() => Gizmos.DrawWireSphere(transform.position, Radius);
}
