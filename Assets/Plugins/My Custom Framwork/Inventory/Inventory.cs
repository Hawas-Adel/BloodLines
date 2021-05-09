using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] private List<Item> StartingItems = default;
	private void Awake()
	{
		StartingItems.ForEach(I => AddItem(I));
		StartingItems.Clear();
	}

	private Dictionary<Item, int> Content = new Dictionary<Item, int>();

	public IEnumerable<KeyValuePair<Item, int>> GetContent() => Content;

	/// <summary>
	/// Manipulate Content of Inventory
	/// </summary>
	/// <param name="item">item to change</param>
	/// <param name="count">0 => no changes , +ve => add item , -ve => remove item</param>
	public void AddItem(Item item, int count = 1)
	{
		if (item && count != 0)
		{
			if (Content.ContainsKey(item))
			{
				Content[item] += count;
				if (Content[item] <= 0)
					Content.Remove(item);
			}
			else if (count > 0)
			{
				Content.Add(item, count);
			}
		}
	}

	/// <summary>
	/// check if item exists in inventory
	/// </summary>
	/// <param name="item"></param>
	/// <returns>number of instances of item in inventory</returns>
	public int HasItem(Item item) => (item && Content.ContainsKey(item)) ? Content[item] : 0;

#if UNITY_EDITOR
	[NaughtyAttributes.ShowNativeProperty] private int ItemCount => Content.Count;
#endif
}
