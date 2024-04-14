using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GridData", menuName = "GridData")]
public class GridData : ScriptableObject
{
	public Dictionary<string, GameObject> grid = new Dictionary<string, GameObject>();
}