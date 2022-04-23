using UnityEngine;

public abstract class ActiveEffect : ScriptableObject
{
	public abstract bool ApplyEffect(GameObject EffectTarget, params float[] Magnitudes);
	public abstract string GetEffectDescription(params float[] Magnitudes);
}

[System.Serializable]
public struct ActiveEffectMagnitude
{
	[OneLine.Weight(2)] public ActiveEffect ActiveEffect;
	public float[] Magnitudes;

	public bool ApplyEffect(GameObject EffectTarget)
	{
		try
		{
			return ActiveEffect.ApplyEffect(EffectTarget, Magnitudes);
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
			return ActiveEffect.GetEffectDescription(Magnitudes);
		}
		catch 
		{
			return null;
		}
	}
}
