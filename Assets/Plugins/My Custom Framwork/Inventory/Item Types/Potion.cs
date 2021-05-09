using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potion", order = 5)]
public class Potion : UsableItem
{
	public ActiveEffect[] Effects;

	public override bool UseItem(GameObject USER)
	{
		bool AnyEffectApplied = Effects.Any(E => E.ApplyEffect(USER));
		if (AnyEffectApplied)
		{
			var Inv = USER.GetComponentInChildren<Inventory>();
			if (Inv) Inv.AddItem(this, -1);
		}
		return AnyEffectApplied;
	}

	public override void HandleDescriptionUI(ItemDescriptionUI UI)
	{
		if (UI)
		{
			if (UI.TitleText)
				Instantiate(UI.TitleText, UI.TitleText.transform.parent).text = name;

			if (UI.Image)
				Instantiate(UI.Image, UI.Image.transform.parent).sprite = Sprite.Create(RuntimePreviewGenerator.GenerateModelPreview(WorldModel.transform, _TextureSize, _TextureSize), new Rect(0, 0, _TextureSize, _TextureSize), 0.5f * Vector2.one);

			if (UI.GenericText)
			{
				for (int i = 0 ; i < Effects.Length ; i++)
				{
					var D = Effects[i].EffectDescription;
					if (!string.IsNullOrWhiteSpace(D))
						Instantiate(UI.GenericText, UI.GenericText.transform.parent).text = D;
				}
			}

			if (UI.GenericText && !string.IsNullOrWhiteSpace(Description))
				Instantiate(UI.GenericText, UI.GenericText.transform.parent).text = Description;
		}
	}
}
