using UnityEngine;

public class DestroyObjOnLoadS : MonoBehaviour
{
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}
}
