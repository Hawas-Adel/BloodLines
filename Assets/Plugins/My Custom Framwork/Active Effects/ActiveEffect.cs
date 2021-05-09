using UnityEngine;

public abstract class ActiveEffect : ScriptableObject
{
	public abstract bool ApplyEffect(GameObject EffectTarget);
	public abstract string EffectDescription { get; }
}
