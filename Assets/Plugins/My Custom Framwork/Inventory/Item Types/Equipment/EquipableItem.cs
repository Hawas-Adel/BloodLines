using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UltEvents;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment/Equipable Item", order = 1)]
public class EquipableItem : UsableItem
{
	[OneLine.Separator("Equippable Item Properties")]
	[Required] public EquipSlot EquipSlot = default;

	private GameObject _EquippedModel;
	public GameObject EquippedModel { get => _EquippedModel ? _EquippedModel : WorldModel; set => _EquippedModel = value; }

	public UltEvent<Equipment> OnEquipped;
	public UltEvent<Equipment> OnUnEquipped;

	public override bool UseItem(GameObject USER)
	{
		var EQ = USER.GetComponentInChildren<Equipment>();
		return EQ ? (EQ.IsEquipped(this) ? EQ.UnEquipItem(this) : EQ.EquipItem(this)) : false;
	}
}
