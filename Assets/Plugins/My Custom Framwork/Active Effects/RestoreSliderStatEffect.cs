using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Active Effects/Restore Slider Value", order = 0)]
public class RestoreSliderStatEffect : ActiveEffect
{
	public string StatName = "";
	public float Magnitude = 0;

	public override bool ApplyEffect(GameObject EffectTarget)
	{
		var AS = EffectTarget.GetComponentInChildren<ActorStats>();
		if (AS && AS.TryGetStat(StatName, out SliderStat SS))
		{
			SS.CurrentValue += Magnitude;
			return true;
		}
		return false;
	}

	[NaughtyAttributes.ShowNativeProperty]
	public override string EffectDescription => $"Restore {Magnitude} {StatName}";
}
