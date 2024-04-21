using System.Collections.Generic;
using JAssets.Scripts_SC;
using UnityEngine;

public class MatrixRowList : MonoBehaviour
{
	public List<MatrixRow> matrixRows = new();

	public MatrixRow StickRow;
	public MatrixRow BoneRow;
	public MatrixRow StoneRow;
	public MatrixRow VineRow;
	public MatrixRow ScrapRow;

	private void Start()
	{
		PopulateList();
		PopulateMatrix();
	}
	
	private void PopulateMatrix()
	{
		foreach (var row in matrixRows)
		{
			if (CraftingMatrix.instance.matrix.ContainsKey(row.name)) return;
			CraftingMatrix.instance.matrix[row.name] = row;
		}
	}

	private void PopulateList()
	{
		matrixRows.Add(StickRow);
		matrixRows.Add(BoneRow);
		matrixRows.Add(StoneRow);
		matrixRows.Add(VineRow);
		matrixRows.Add(ScrapRow);
	}
}
