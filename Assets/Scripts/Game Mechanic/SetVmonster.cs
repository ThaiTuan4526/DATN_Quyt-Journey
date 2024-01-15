using UnityEngine;

public class SetVmonster : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		float axis = Input.GetAxis("Horizontal");
		float axis2 = Input.GetAxis("Vertical");
		base.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(axis, axis2);
	}
}
