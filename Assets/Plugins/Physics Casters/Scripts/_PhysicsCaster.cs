using System.Collections.Generic;

using UnityEngine;

namespace PhysicsCasters
{
	public abstract class PhysicsCaster : MonoBehaviour
	{
		/// <summary>
		/// Cast rays from all refrenced <seealso cref="Transform"/>s using their position and forward direction
		/// </summary>
		/// <param name="hit">The resulting hit if any</param>
		/// <returns>If any ray has hit a <seealso cref="Collider"/></returns>
		public abstract bool Cast(out RaycastHit hit);

		/// <summary>
		/// Cast rays from all refrenced <seealso cref="Transform"/>s using their position and forward direction
		/// </summary>
		/// <returns>A List of all Unique <seealso cref="RaycastHit"/>s from all <seealso  cref="Transform"/>s</returns>
		public abstract IEnumerable<RaycastHit> CastAll();
	}


	public abstract class PhysicsCasterImplementer : PhysicsCaster
	{
		[Header("Base Settings")]
		[Tooltip("Transforms that act as starting point and dirction(transform.forward) for Casting")]
		public List<Transform> CasterTransforms = new List<Transform>();

		public LayerMask LayerMask;
		public QueryTriggerInteraction QueryTriggerInteraction = QueryTriggerInteraction.UseGlobal;

		[Min(0)]
		[Tooltip("Length of Raycast")]
		public float CastLength = Mathf.Infinity;

		[Header("Gizmos")]
		public bool ShowGizmos = true;
		public Color GizmoColor = Color.magenta;

		protected virtual void CasterGizmo(Transform T) { }

		private void OnDrawGizmos()
		{
			if (ShowGizmos)
			{
				Gizmos.color = GizmoColor;
				foreach (var Caster in CasterTransforms)
				{
					if (Caster) CasterGizmo(Caster);
				}
			}
		}

		private void Reset() => CasterTransforms.Add(transform);
	}
}