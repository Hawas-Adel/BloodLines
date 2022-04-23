using UnityEngine;

public abstract class PassiveEffect : ScriptableObject
{
	public abstract bool ApplyEffect(GameObject EffectTarget, params float[] Magnitude);
	public abstract bool RemoveEffect(GameObject EffectTarget, params float[] Magnitude);
	public abstract string GetEffectDescription(params float[] Magnitude);
}

[System.Serializable]
public struct PassiveEffectMagnitude
{
	[OneLine.Weight(2)] public PassiveEffect PassiveEffect;
	public float[] Magnitudes;

	public bool ApplyEffect(GameObject EffectTarget)
	{
		try
		{
			return PassiveEffect.ApplyEffect(EffectTarget, Magnitudes);
		}
		catch
		{
			return false;
		}
	}

	public bool RemoveEffect(GameObject EffectTarget)
	{
		try
		{
			return PassiveEffect.RemoveEffect(EffectTarget, Magnitudes);
		}
		catch
		{
			return false;
		}
	}

	public string GetEffectDescription()
	{
		try
		{
			return PassiveEffect.GetEffectDescription(Magnitudes);
		}
		catch
		{
			return null;
		}
	}
}
