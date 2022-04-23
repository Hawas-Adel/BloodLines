using System.Linq;
using UltEvents;
using UnityEngine;

public class GroundedStateMonitor : MonoBehaviour
{
	[SerializeField] [Min(0)] private float _GroundCheckRadius = 0.25f;

	public UltEvent OnBecameGrounded;
	public UltEvent OnBecameNotGrounded;

	private bool isGrounded = true;
	[NaughtyAttributes.ShowNativeProperty]
	public bool IsGrounded
	{
		get => isGrounded;
		private set
		{
			if (isGrounded != value)
			{
				isGrounded = value;
				if (value) OnBecameGrounded.Invoke();
				else OnBecameNotGrounded.Invoke();
			}
		}
	}
	private void Update()
	{
		var Colliders = Physics.OverlapSphere(transform.position, _GroundCheckRadius).ToList();
		Colliders.RemoveAll(coll => coll.attachedRigidbody ? coll.attachedRigidbody.gameObject == gameObject : coll.gameObject == gameObject);
		IsGrounded = Colliders.ConvertAll(Coll => Coll.gameObject.layer).Distinct().Any(L => !Physics.GetIgnoreLayerCollision(L, gameObject.layer));
	}

	private void OnDrawGizmosSelected() => Gizmos.DrawWireSphere(transform.position, _GroundCheckRadius);
}
