using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
	[Min(0)] public float Radius = 1;

	public Vector3 GetRandomPointInRange() => transform.position + Radius * Random.insideUnitSphere;

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(transform.position, Radius);
	}
}
