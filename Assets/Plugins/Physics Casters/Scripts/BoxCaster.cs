using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace PhysicsCasters
{
	[AddComponentMenu("Physics Casters/Box Caster")]
	public class BoxCaster : PhysicsCasterImplementer
	{
		[Header("Box Settings")]
		[Tooltip("Radius of Sphere used in SphereCast")]
		public Vector3 BoxHalfExtends;

		public override bool Cast(out RaycastHit hit)
		{
			foreach (var Caster in CasterTransforms)
			{
				if (Caster && Physics.BoxCast(Caster.position, BoxHalfExtends, Caster.forward,
					out hit, Quaternion.identity, CastLength, LayerMask.value, QueryTriggerInteraction))
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
					Result.AddRange(Physics.BoxCastAll(Caster.position, BoxHalfExtends, Caster.forward, Quaternion.identity, CastLength,
						LayerMask.value, QueryTriggerInteraction));
				}
			}
			return Result.Distinct();
		}

		protected override void CasterGizmo(Transform T)
		{
			Gizmos.matrix = T.localToWorldMatrix;
			Gizmos.DrawLine(Vector3.zero, Mathf.Min(CastLength, 1000) * Vector3.forward);
			Gizmos.DrawWireCube(Vector3.zero, BoxHalfExtends * 2);
			Gizmos.DrawWireCube(Mathf.Min(CastLength, 1000) * Vector3.forward, BoxHalfExtends * 2);
		}
	}
}
