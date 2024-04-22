using System.Collections.Generic;
using UnityEngine;

namespace JAssets.Scripts_SC.Lists
{
	public class MatrixRowList : MonoBehaviour
	{
		public List<MatrixRow> matrixRows = new();

		public MatrixRow stickRow;
		public MatrixRow boneRow;
		public MatrixRow stoneRow;
		public MatrixRow vineRow;
		public MatrixRow scrapRow;

		private void Start()
		{
			PopulateList();
			PopulateMatrix();
		}
	
		private void PopulateMatrix()
		{
			foreach (var row in matrixRows)
			{
				if (!CraftingMatrix.instance.matrix.TryAdd(row.name, row)) return;
			}
		}

		private void PopulateList()
		{
			matrixRows.Add(stickRow);
			matrixRows.Add(boneRow);
			matrixRows.Add(stoneRow);
			matrixRows.Add(vineRow);
			matrixRows.Add(scrapRow);
		}
	}
}
