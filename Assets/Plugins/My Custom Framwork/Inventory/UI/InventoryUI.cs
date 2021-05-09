using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	[SerializeField] private ItemUI ItemUIPrefap = default;

	private void OnEnable()
	{
		Inventory PlayerInv = Player.Current.GetComponentInChildren<Inventory>();
		var Items = PlayerInv.GetContent();
		foreach (var item in Items)
			Instantiate(ItemUIPrefap, ItemUIPrefap.transform.parent).SetUIForItem(item.Key, PlayerInv);
	}

	private void OnDisable()
	{
		var instances = ItemUIPrefap.transform.parent.GetComponentsInChildren<ItemUI>(true);
		for (int i = 1 ; i < instances.Length ; i++)
			Destroy(instances[i].gameObject);
	}
}
