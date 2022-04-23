using System.Collections;
using System.Collections.Generic;
using OneLine;
using UltEvents;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public UltEvent<Item, int> OnInventoryChanged;

	private Dictionary<Item, int> Content = new Dictionary<Item, int>();

	public IEnumerable<KeyValuePair<Item, int>> GetContent() => Content;

	/// <summary>
	/// Manipulate Content of Inventory
	/// </summary>
	/// <param name="item">item to change</param>
	/// <param name="count">0 => no changes , +ve => add item , -ve => remove item</param>	
	public int AddItem(Item item, int count = 1)
	{
		if (item && count != 0)
		{
			if (count > 0)
			{
				if (!Content.ContainsKey(item))
					Content.Add(item, 0);
			}
			else
			{
				count = Mathf.Max(-HasItem(item), count);
			}
			if (Content.ContainsKey(item))
			{
				Content[item] += count;
				if (Content[item] <= 0)
					Content.Remove(item);
			}
			OnInventoryChanged?.Invoke(item, count);
			item.OnAddedOrRemovedFromInventory?.Invoke(this, count);
			return count;
		}
		return 0;
	}

	public int DropItem(Item item, int count = 1)
	{
		count = -AddItem(item, -count);
		if (count > 0)
		{
			Vector3 RandP = Random.insideUnitCircle.normalized;
			RandP = new Vector3(RandP.x, 0, RandP.y) + Vector3.up;
			var instance = item.Spawn(transform.TransformPoint(RandP), transform.rotation);
			instance.GetComponent<WorldItem>().Count = count;
		}
		return 0;
	}

	/// <summary>
	/// check if item exists in inventory
	/// </summary>
	/// <param name="item"></param>
	/// <returns>number of Stack of item in inventory</returns>
	public int HasItem(Item item) => (item && Content.ContainsKey(item)) ? Content[item] : 0;

#if UNITY_EDITOR
	[NaughtyAttributes.ShowNativeProperty] private int ItemCount => Content.Count;
#endif
}

[System.Serializable]
public struct StartingItem
{
	[Weight(3)] public Item Item;
	[Min(1)] public int Count;
	public bool E;
}
