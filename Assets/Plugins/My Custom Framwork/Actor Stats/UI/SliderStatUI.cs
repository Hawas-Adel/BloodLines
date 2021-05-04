using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class SliderStatUI : MonoBehaviour
{
	[SerializeField] private ActorStats ActorStats = default;
	[SerializeField] private string SliderStatName = default;
	private Image Image;
	private SliderStat SS;

	private void Awake() => Image = GetComponent<Image>();

	private void OnEnable()
	{
		if (ActorStats && !string.IsNullOrWhiteSpace(SliderStatName) && ActorStats.TryGetStat(SliderStatName, out SS))
		{
			UpdateImageFill();
			SS.OnCurrentValueChanged += UpdateImageFill;
		}
	}

	private void UpdateImageFill() => Image.fillAmount = SS.CurrentValue / SS.MaxValue.Value;

	private void OnDisable()
	{
		if (ActorStats && !string.IsNullOrWhiteSpace(SliderStatName) && ActorStats.TryGetStat(SliderStatName, out SS))
			SS.OnCurrentValueChanged -= UpdateImageFill;
	}
}
