using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionUI : MonoBehaviour
{
	public TextMeshProUGUI TitleText = default;
	public TextMeshProUGUI GenericText = default;
	public Image Image = default;

	[SerializeField] private GameObject ParentCanvas = default;

	public void SetUIForItem(ItemUI itemUI)
	{
		ParentCanvas.SetActive(itemUI && itemUI.Item);
		if (ParentCanvas.activeSelf)
		{
			transform.position = itemUI.transform.position;
			itemUI.Item.HandleDescriptionUI(this);
			for (int i = 3 ; i < transform.childCount ; i++)
				transform.GetChild(i).gameObject.SetActive(true);
		}
		else
		{
			for (int i = 3 ; i < transform.childCount ; i++)
				Destroy(transform.GetChild(i).gameObject);
		}
	}
}
