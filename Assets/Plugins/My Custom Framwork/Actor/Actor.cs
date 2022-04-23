using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using OneLine;
using UltEvents;
using UnityEngine;

[CreateAssetMenu(menuName = "Actors/Actor")]
public class _Actor : VirtualGO
{
	[Separator("Actor Properties")]
	[Min(0)] public float Weight = 75f;
	[Min(0)] public float Height = 2;
	[Range(0, 1)] public float VCAMAttachmentHeight = 0.8f;
	[Min(0)] public float Radius = 0.35f;

	[ShowNativeProperty] public GameObject Current { get; private set; }
	public ActorStats ActorStats { get; private set; }

	#region Animator
	[Separator("Animator")]
	[SerializeField] private RuntimeAnimatorController _DefaultAnimCtrlr;
	private RuntimeAnimatorController _AnimCtrlr;
	public RuntimeAnimatorController AnimCtrlr
	{
		get => _AnimCtrlr ? _AnimCtrlr : _DefaultAnimCtrlr;
		set
		{
			_AnimCtrlr = value;
			if (Animator)
				Animator.runtimeAnimatorController = _AnimCtrlr ? _AnimCtrlr : _DefaultAnimCtrlr;
		}
	}

	public Animator Animator { get; private set; }
	private ActorAnimationEvents AAE;
	[System.NonSerialized] public UltEvent<string, object> OnAnimationEvent = new UltEvent<string, object>();
	#endregion

	#region Locomotion
	public GroundedStateMonitor ActorGroundedStateMonitor { get; private set; }
	public ActorLocomotion ActorLocomotion { get; private set; }
	[Separator("Locomotion")] [Min(0)] public float MovmentSpeed = 4;
	[Min(0)] public float JumpForce = 15000;
	[CurveRange(-1, 0, 1, 1)]
	public AnimationCurve MovmentSpeedAngularMultiplier = new AnimationCurve(new Keyframe(-1, 0.5f), new Keyframe(1, 1));
	#endregion

	#region Inventory & Equipment
	public Inventory Inventory { get; private set; }
	public Equipment Equipment { get; private set; }
	[SerializeField]
	[Separator("Inventory")]
	[OneLineWithHeader] [HideLabel] private List<StartingItem> StartingItems = default;
	#endregion

	#region Combat
	public ActorCombat ActorCombat { get; private set; }
	#region Offense
	[Separator("Combat -  Offense")]
	[Min(0)] public float Damage = 10f;
	#endregion
	#region Defense
	[Separator("Combat -  Defense")]
	[Min(0)] public float Armor = 20f;
	[Min(0)] public float Health = 100f;
	[Range(0, 1)] public float CurrentHealth = 1;
	#endregion
	#endregion

	public override void OnInstanceAwake(VirtualGOInstance instance)
	{
		if (Current)
		{
			Destroy(instance);//destroy New and stop
			return;
		}
		Current = instance.gameObject;//assign new

		System.Array.ForEach(Current.GetComponentsInChildren<_IActorREF>(), Ref => Ref.Actor = this);

		#region Assigning Actor Stats
		ActorStats = Current.GetComponentInChildren<ActorStats>();
		ActorStats.TryRegisterStat(nameof(Weight), out Stat _, S => { S.BaseValue = Weight; S.Min = 0; });
		ActorStats.TryRegisterStat(nameof(Height), out Stat _, S => { S.BaseValue = Height; S.Min = 0; });
		ActorStats.TryRegisterStat(nameof(MovmentSpeed), out Stat _, S => { S.BaseValue = MovmentSpeed; S.Min = 0; });
		ActorStats.TryRegisterStat(nameof(JumpForce), out Stat _, S => { S.BaseValue = JumpForce; S.Min = 0; });
		ActorStats.TryRegisterStat(nameof(Damage), out Stat _, S => { S.BaseValue = Damage; S.Min = 0; });
		ActorStats.TryRegisterStat(nameof(Health), out Stat H, S => { S.BaseValue = Health; S.Min = 0; });
		ActorStats.TryRegisterStat(nameof(Health), out SliderStat _, S => { S.CurrentValue = CurrentHealth * Health; });
		ActorStats.TryRegisterStat(nameof(Armor), out Stat _, S => { S.BaseValue = Armor; });
		#endregion

		ReSyncActorAnimator();

		ActorLocomotion = Current.GetComponentInChildren<ActorLocomotion>();
		ActorGroundedStateMonitor = Current.GetComponentInChildren<GroundedStateMonitor>();

		Equipment = Current.GetComponentInChildren<Equipment>();
		Inventory = Equipment.BackingInventory;
		StartingItems.ForEach(SI =>
		{
			if (SI.Item && SI.Count > 0)
			{
				Inventory.AddItem(SI.Item, SI.Count);
				if (SI.Item is EquipableItem E && SI.E)
					Equipment.EquipItem(E);
			}
		});

		ActorCombat = Current.GetComponentInChildren<ActorCombat>();
	}

	public void ReSyncActorAnimator()
	{
		Animator = Current.GetComponentInChildren<Animator>();
		if (AAE)
			Destroy(AAE);
		if (Animator)
		{
			Animator.applyRootMotion = false;
			Animator.runtimeAnimatorController = AnimCtrlr;
			AAE = Animator.gameObject.AddComponent<ActorAnimationEvents>();
		}
	}

	public override void OnInstanceDestroy(VirtualGOInstance instance)
	{
		if (instance.gameObject == Current)
		{
			Current = default;
		}
	}

	public override void OnGizmosSelected(VirtualGOInstance instance)
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(Vector3.zero, Vector3.up * Height);
		Gizmos.DrawWireSphere(Vector3.zero, Radius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(Vector3.up * Height * VCAMAttachmentHeight, 0.01f);
	}
}

public interface _IActorREF
{
	public _Actor Actor { get; set; }
}
