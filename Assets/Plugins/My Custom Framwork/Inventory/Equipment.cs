using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UltEvents;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Equipment : MonoBehaviour
{
	public UltEvent<EquipableItem> OnEquipmentChanged = new UltEvent<EquipableItem>();

	public Inventory BackingInventory { get; private set; }
	private Rigidbody ActorRoot;
	private void Awake()
	{
		BackingInventory = GetComponent<Inventory>();
		BackingInventory.OnInventoryChanged += ReSyncRemovedItems;
		ActorRoot = GetComponentInParent<Rigidbody>();
	}

	private void ReSyncRemovedItems(Item I, int c)
	{
		if (I is EquipableItem e && IsEquipped(e) && c <= 0 && BackingInventory.HasItem(e) <= 0)
			UnEquipItem(e);
	}

	private Dictionary<EquipSlot, (EquipableItem E, GameObject VM)> EquippedItems = new Dictionary<EquipSlot, (EquipableItem E, GameObject VM)>();

	public bool EquipItem(EquipableItem E)
	{
		if (E && (!IsEquipped(E.EquipSlot) || UnEquipItem(E.EquipSlot)))
		{
			var EST = ActorRoot.GetComponentsInChildren<EquipSlotTransform>().FirstOrDefault(EST => EST.EquipSlot == E.EquipSlot);
			GameObject VM = default;
			if (EST) VM = Instantiate(E.EquippedModel, EST.transform);
			EquippedItems.Add(E.EquipSlot, (E, VM));
			E.OnEquipped.InvokeSafe(this);
			OnEquipmentChanged.InvokeSafe(E);
			return true;
		}
		return false;
	}
	public bool UnEquipItem(EquipableItem E) => (E && IsEquipped(E)) ? UnEquipItem(E.EquipSlot) : false;
	public bool UnEquipItem(EquipSlot slot)
	{
		var current = IsEquipped(slot);
		if (slot && current)
		{
			Destroy(EquippedItems[slot].VM);
			EquippedItems.Remove(slot);
			current.OnUnEquipped.InvokeSafe(this);
			OnEquipmentChanged.InvokeSafe(current);
			return true;
		}
		return false;
	}

	public bool IsEquipped(EquipableItem E) => EquippedItems.ContainsKey(E.EquipSlot) && EquippedItems[E.EquipSlot].E == E;
	public EquipableItem IsEquipped(EquipSlot slot) => EquippedItems.ContainsKey(slot) ? EquippedItems[slot].E : null;
}
