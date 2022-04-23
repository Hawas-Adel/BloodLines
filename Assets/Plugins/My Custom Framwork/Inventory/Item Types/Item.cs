using NaughtyAttributes;
using UltEvents;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item", order = 0)]
public class Item : VirtualGO
{
	public const int _TextureSize = 512;

	[OneLine.Separator("Item Properties")]
	[Min(0)] public float Weight = 0;
	[Min(0)] public int Value = 0;
	[ResizableTextArea] public string Description = "";

	public UltEvent<Inventory, int> OnAddedOrRemovedFromInventory;

	public override void OnInstanceAwake(VirtualGOInstance instance)
	{
		if (!instance.GetComponent<WorldItem>())
		{
			instance.gameObject.AddComponent<WorldItem>();
		}
	}

	public virtual void HandleDescriptionUI(ItemDescriptionUI UI)
	{
		if (UI)
		{
			if (UI.TitleText)
				Instantiate(UI.TitleText, UI.TitleText.transform.parent).text = name;

			if (UI.Image)
				Instantiate(UI.Image, UI.Image.transform.parent).sprite = Sprite.Create(RuntimePreviewGenerator.GenerateModelPreview(WorldModel.transform, _TextureSize, _TextureSize), new Rect(0, 0, _TextureSize, _TextureSize), 0.5f * Vector2.one);

			if (UI.GenericText && !string.IsNullOrWhiteSpace(Description))
				Instantiate(UI.GenericText, UI.GenericText.transform.parent).text = Description;
		}
	}
}
