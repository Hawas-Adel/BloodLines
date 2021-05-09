using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace PhysicsCasters
{
	[AddComponentMenu("Physics Casters/Capsule Caster")]
	public class CapsuleCaster : PhysicsCasterImplementer
	{
		[Header("Capsule Settings")]
		[Tooltip("Local Center of a Sphre at end of the Capsule")]
		public Vector3 LocalPoint1;
		[Tooltip("Local Center of a Sphre at end of the Capsule")]
		public Vector3 LocalPoint2;
		[Tooltip("Radius of the 2 Spheres at the ends of the Capsule")]
		[Min(0)] public float Radius = 0.2f;

		public override bool Cast(out RaycastHit hit)
		{
			foreach (var Caster in CasterTransforms)
			{
				if (Caster && Physics.CapsuleCast(Caster.TransformPoint(LocalPoint1), Caster.TransformPoint(LocalPoint2),
					Radius, Caster.forward, out hit, CastLength, LayerMask.value, QueryTriggerInteraction))
				{
					return true;
				}
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
					Result.AddRange(Physics.CapsuleCastAll(Caster.TransformPoint(LocalPoint1), Caster.TransformPoint(LocalPoint2),
						Radius, Caster.forward, CastLength, LayerMask.value, QueryTriggerInteraction));
				}
			}
			return Result.Distinct();
		}

		protected override void CasterGizmo(Transform T)
		{
			Gizmos.matrix = T.localToWorldMatrix;

			Gizmos.DrawWireSphere(LocalPoint1, Radius);
			Gizmos.DrawLine(LocalPoint1, LocalPoint2);
			Gizmos.DrawWireSphere(LocalPoint2, Radius);

			Gizmos.DrawLine(Vector3.zero, Mathf.Min(CastLength, 1000) * Vector3.forward);
			Gizmos.DrawLine(LocalPoint1, LocalPoint1 + Mathf.Min(CastLength, 1000) * Vector3.forward);
			Gizmos.DrawLine(LocalPoint2, LocalPoint2 + Mathf.Min(CastLength, 1000) * Vector3.forward);

			Gizmos.DrawWireSphere(LocalPoint1 + Mathf.Min(CastLength, 1000) * Vector3.forward, Radius);
			Gizmos.DrawLine(LocalPoint1 + Mathf.Min(CastLength, 1000) * Vector3.forward, LocalPoint2 + Mathf.Min(CastLength, 1000) * Vector3.forward);
			Gizmos.DrawWireSphere(LocalPoint2 + Mathf.Min(CastLength, 1000) * Vector3.forward, Radius);
		}
	}
}
