using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	private Inventory TargetInventory;

	public void OpenUIForPlayer() => OpenUIForInventory(PlayerREF.Current.GetComponentInChildren<Inventory>());
	public void OpenUIForInventory(Inventory Inv)
	{
		TargetInventory = Inv;
		RegesterEventsListners();
		RebuildItemsUI();
	}

	private void OnDisable() => UnRegesterEventsListners();

	private void RegesterEventsListners()
	{
		if (TargetInventory)
		{
			TargetInventory.OnInventoryChanged += OnTargetInventoryChanged;
			TargetInventory.GetComponent<Equipment>().OnEquipmentChanged += OnTargetEquipmentChanged;
		}
	}
	private void UnRegesterEventsListners()
	{
		if (TargetInventory)
		{
			TargetInventory.OnInventoryChanged -= OnTargetInventoryChanged;
			TargetInventory.GetComponent<Equipment>().OnEquipmentChanged += OnTargetEquipmentChanged;
		}
	}

	private void OnTargetInventoryChanged(Item I = default, int C = default) => RebuildItemsUI();
	private void OnTargetEquipmentChanged(EquipableItem EQ = default) => RebuildItemsUI();

	private void RebuildItemsUI()
	{
		Equipment EQ = TargetInventory.GetComponent<Equipment>();
		var PlayerInvContent = TargetInventory.GetContent();
		for (int i = 0 ; i < PlayerInvContent.Count() ; i++)
		{
			ItemUI Inst = transform.childCount > i ? transform.GetChild(i).GetComponent<ItemUI>() : Instantiate(transform.GetChild(0).GetComponent<ItemUI>(), transform.parent);
			Inst.gameObject.SetActive(true);
			Inst.SetUIForItem(PlayerInvContent.ElementAt(i).Key, TargetInventory, EQ);
		}
		for (int i = PlayerInvContent.Count() ; i < transform.childCount ; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}
}
