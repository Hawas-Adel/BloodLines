using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorStats : MonoBehaviour
{
	private Dictionary<(System.Type, string), MonoBehaviour> StatsMap = new Dictionary<(System.Type, string), MonoBehaviour>();

	public bool TryRegisterStat<T>(string ID, out T stat, UnityAction<T> OnInitialized = null) where T : MonoBehaviour
	{
		if (!string.IsNullOrWhiteSpace(ID) && !StatsMap.ContainsKey((typeof(T), ID)))
		{
			var GO = transform.Find(ID);
			if (!GO)
			{
				GO = new GameObject(ID).transform;
				GO.SetParent(transform, false);
			}
			if (!GO.TryGetComponent(out stat)) stat = GO.gameObject.AddComponent<T>();
			StatsMap.Add((typeof(T), ID), stat);
			OnInitialized?.Invoke(stat);
			return true;
		}
		stat = default;
		return false;
	}

	public bool TryGetStat<T>(string ID, out T stat) where T : MonoBehaviour
	{
		if (StatsMap.ContainsKey((typeof(T), ID)))
		{
			stat = (T)StatsMap[(typeof(T), ID)];
			return true;
		}
		stat = default;
		return false;
	}
	public T GetStat<T>(string ID) where T : MonoBehaviour
	{
		TryGetStat(ID, out T Result);
		return Result;
	}
}
