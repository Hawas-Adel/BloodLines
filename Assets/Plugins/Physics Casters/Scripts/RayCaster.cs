using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace PhysicsCasters
{
	[AddComponentMenu("Physics Casters/Ray Caster")]
	public class RayCaster : PhysicsCasterImplementer
	{
		public override bool Cast(out RaycastHit hit)
		{
			foreach (var Caster in CasterTransforms)
			{
				if (Caster && Physics.Raycast(Caster.position, Caster.forward, out hit, CastLength, LayerMask.value, QueryTriggerInteraction))
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
					Result.AddRange(Physics.RaycastAll(Caster.position, Caster.forward, CastLength, LayerMask.value, QueryTriggerInteraction));
				}
			}
			return Result.Distinct();
		}

		protected override void CasterGizmo(Transform T)
		{
			Gizmos.matrix = T.localToWorldMatrix;
			Gizmos.DrawLine(Vector3.zero, Mathf.Min(CastLength, 1000) * Vector3.forward);
		}
	}
}
