using Sirenix.OdinInspector;
using UnityEngine;

public class CraftingController : MonoBehaviour
{
	public static CraftingController instance { get; private set; }
	
	// [Header("Recipe Matrix")][ShowInInspector]
	// public ConsumableMatrix recipeDict = new();

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
