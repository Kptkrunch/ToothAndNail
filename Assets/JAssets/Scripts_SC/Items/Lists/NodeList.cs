using System;
using System.Collections;
using System.Collections.Generic;
using JAssets.Scripts_SC;
using MoreMountains.Tools;
using UnityEngine;

public class NodeList : MonoBehaviour
{
	public List<MMSimpleObjectPooler> nodesList;

	public MMSimpleObjectPooler NGrassNode;
	public MMSimpleObjectPooler NGearNode;
	public MMSimpleObjectPooler NBoneNode;
	public MMSimpleObjectPooler NRootNode;
	public MMSimpleObjectPooler NRubbleNode;
	public MMSimpleObjectPooler NDebrisNode;

	private void Start()
	{
		PopulateList();
		PopulateLibrary();
	}

	private void PopulateLibrary()
	{
		foreach (var pool in nodesList)
		{
			Library.instance.nodesDict[pool.GetPooledGameObject().name] = pool;
		}
	}

	private void PopulateList()
	{
		nodesList.Add(NGrassNode);
		nodesList.Add(NGearNode);
		nodesList.Add(NBoneNode);
		nodesList.Add(NRootNode);
		nodesList.Add(NRubbleNode);
		nodesList.Add(NDebrisNode);
	}
}
