using UnityEngine;

public class Timber : MonoBehaviour
{
	public GameObject bodyOfTimber;

	public GameObject blockBody;

	public GameObject mainP;

	private float rotZ;

	private float speedY;

	private float speedX;

	private float acpY;

	private void Start()
	{
		bodyOfTimber.SetActive(value: false);
	}

	public void SetDownMode()
	{
		base.gameObject.GetComponent<BoxCollider2D>().enabled = false;
		base.gameObject.GetComponent<Box>().enabled = false;
		blockBody.SetActive(value: false);
		mainP.SetActive(value: false);
		base.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		base.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
		base.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
		bodyOfTimber.SetActive(value: true);
		rotZ = base.gameObject.transform.eulerAngles.z;
		speedY = 0.05f;
		acpY = 0.0018f;
		Singleton<ManagerFunctions>.use.addFunction(Fall);
	}

	private void Fall()
	{
		if (rotZ < 180f)
		{
			rotZ += 180f * Time.deltaTime;
			base.gameObject.transform.eulerAngles = new Vector3(0f, 0f, rotZ);
			if (rotZ >= 180f)
			{
				rotZ = 180f;
				base.gameObject.transform.eulerAngles = new Vector3(0f, 0f, rotZ);
			}
		}
		if (Mathf.Abs(base.gameObject.transform.position.x - 23f) > 0.2f)
		{
			if (base.gameObject.transform.position.x > 23f)
			{
				speedX = -0.02f;
			}
			else
			{
				speedX = 0.02f;
			}
		}
		else
		{
			speedX = 0f;
		}
		base.gameObject.transform.position = new Vector2(base.gameObject.transform.position.x + speedX * 60f * Time.deltaTime, base.gameObject.transform.position.y - speedY * 60f * Time.deltaTime);
		speedY += acpY;
		acpY += 0.0025f * Time.deltaTime;
		if (base.gameObject.transform.position.y < -9.2f)
		{
			base.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 180f);
			Singleton<ManagerFunctions>.use.removeFunction(Fall);
		}
	}
}
