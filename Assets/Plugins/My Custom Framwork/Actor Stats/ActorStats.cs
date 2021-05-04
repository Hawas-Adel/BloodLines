using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecutionOrder(-100)]
public class ActorStats : MonoBehaviour
{
	private Dictionary<(System.Type, string), IStat> StatsMap = new Dictionary<(System.Type, string), IStat>();

	public bool TryRegisterStat<T>(T stat) where T : IStat
	{
		if (string.IsNullOrWhiteSpace(stat.ID) || StatsMap.ContainsKey((typeof(T), stat.ID)))
		{
			return false;
		}
		else
		{
			StatsMap.Add((typeof(T), stat.ID), stat);
			return true;
		}
	}

	public bool TryGetStat<T>(string ID, out T stat) where T : IStat
	{
		if (StatsMap.ContainsKey((typeof(T), ID)))
		{
			stat = (T)StatsMap[(typeof(T), ID)];
			return true;
		}
		stat = default;
		return false;
	}

#if UNITY_EDITOR
	private bool IsPlaying => Application.isPlaying;
	[NaughtyAttributes.Button]
	[NaughtyAttributes.EnableIf(nameof(IsPlaying))]
	private void DumpToLog()
	{
		string Output = $"Actor Stats dump : \n";
		foreach (var item in StatsMap)
			Output += $"~ {item.Key.Item2} ({item.Key.Item1.Name}) \n";
		Debug.Log(Output);
	}
#endif
}

public interface IStat
{
	string ID { get; }
}
