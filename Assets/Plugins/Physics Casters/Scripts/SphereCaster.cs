using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace PhysicsCasters
{
	[AddComponentMenu("Physics Casters/Sphere Caster")]
	public class SphereCaster : PhysicsCasterImplementer
	{
		[Header("Sphere Settings")]
		[Tooltip("Radius of Sphere used in SphereCast")]
		[Min(0)]
		public float Radius = 1;

		public override bool Cast(out RaycastHit hit)
		{
			foreach (var Caster in CasterTransforms)
			{
				if (Caster && Physics.SphereCast(Caster.position, Radius, Caster.forward, out hit, CastLength, LayerMask.value, QueryTriggerInteraction))
					return true;
			}
			hit = default;
			return false;
		}
		public override IEnumerable<RaycastHit> CastAll()
		{
			var Result = new List<RaycastHit>();
			foreach (var Caster in CasterTransforms)
			{
				if (Caster)
				{
					Result.AddRange(Physics.SphereCastAll(Caster.position, Radius, Caster.forward, CastLength, LayerMask.value, QueryTriggerInteraction));
				}
			}
			return Result.Distinct();
		}

		protected override void CasterGizmo(Transform T)
		{
			Gizmos.matrix = T.localToWorldMatrix;
			Gizmos.DrawLine(Vector3.zero, Mathf.Min(CastLength, 1000) * Vector3.forward);
			Gizmos.DrawWireSphere(Vector3.zero, Radius);
			Gizmos.DrawWireSphere(Mathf.Min(CastLength, 1000) * Vector3.forward, Radius);
		}
	}
}
