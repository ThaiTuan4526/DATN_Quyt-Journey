using UnityEngine;

public class CameraGame : MonoBehaviour
{
	internal bool isFollow;

	internal bool isGoToPos;

	private Transform heroT;

	internal Transform cameraT;

	private Vector3 targetPos;

	internal Vector3 calculateSmoothPos;

	private float dist;

	private float offsetY = 3f;

	private float timeX = 1f;

	private float speedGo;

	private Vector2 borderX;

	internal float checkYpos;

	public bool isPause;

	private float speedGoY;

	internal float directX;

	internal float maxX = 1.5f;

	internal float maxDownY = -50f;

	internal float maxUpY = 50f;

	internal float speedBigY = 3f;

	internal const float speedBigY_CONST = 3f;

	private Camera cam;

	internal bool followY = true;

	private float size1;

	private float size2;

	private float sizeLerp;

	public void Init()
	{
		cameraT = base.gameObject.transform;
		isPause = false;
		speedGoY = 0.6f;
		directX = 3f;
		cam = base.gameObject.GetComponent<Camera>();
	}

	public void SetBorders(Vector2 n)
	{
		borderX = n;
		if (Main.aspectRatio < 1.4f)
		{
			borderX.x -= 2.5f;
			borderX.y += 2.5f;
		}
		else if (Main.aspectRatio < 1.7f)
		{
			borderX.x -= 1f;
			borderX.y += 1f;
		}
	}

	public void SetFollowPos(Vector3 p)
	{
		cameraT.position = p;
		calculateSmoothPos = cameraT.position;
		directX = maxX;
	}

	public void SetPos(Vector3 hT)
	{
		cameraT.position = new Vector3(hT.x, hT.y + offsetY, -10f);
		calculateSmoothPos = cameraT.position;
	}

	public void ToFollow(Transform hT)
	{
		heroT = hT;
		isFollow = true;
		targetPos.z = -10f;
		calculateSmoothPos = cameraT.position;
		calculateSmoothPos.z = -10f;
	}

	public void GoToPos(Vector3 n, float s)
	{
		isGoToPos = true;
		speedGo = s * 60f;
		targetPos = n;
		BreakFollow();
	}

	public void SwitchOffsetY(float n)
	{
		offsetY = n;
		checkYpos = 10f;
		speedBigY = 1.2f;
	}

	public void BreakFollow()
	{
		isFollow = false;
	}

	public void SetDefaultSize()
	{
		cam.orthographicSize = 5.4f;
	}

	public void SetSize(float n)
	{
		cam.orthographicSize = n;
	}

	public void SetSmoothChangeSize(float n)
	{
		sizeLerp = 0f;
		size1 = cam.orthographicSize;
		size2 = n;
		Singleton<ManagerFunctions>.use.removeFunction(SmoothChangeSize);
		Singleton<ManagerFunctions>.use.addFunction(SmoothChangeSize);
	}

	private void SmoothChangeSize()
	{
		sizeLerp += Time.deltaTime;
		cam.orthographicSize = Mathf.Lerp(size1, size2, sizeLerp);
		if (sizeLerp >= 1f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(SmoothChangeSize);
		}
	}

	private void Update()
	{
		if (isPause)
		{
			return;
		}
		if (isGoToPos)
		{
			cameraT.position = Vector3.Lerp(cameraT.position, targetPos, speedGo * Time.deltaTime);
			if ((double)Vector3.Distance(cameraT.position, targetPos) < 0.05)
			{
				isGoToPos = false;
			}
		}
		if (!isFollow)
		{
			return;
		}
		if (Main.hero.onPlatform)
		{
			if (Main.hero.direct)
			{
				directX += 3f * Time.deltaTime;
				if (directX > maxX)
				{
					directX = maxX;
				}
			}
			else
			{
				directX -= 3f * Time.deltaTime;
				if (directX < 0f - maxX)
				{
					directX = 0f - maxX;
				}
			}
		}
		targetPos.x = heroT.position.x + directX;
		if (Mathf.Abs(cameraT.position.x - targetPos.x) > 0.2f)
		{
			if (Mathf.Abs(cameraT.position.x - targetPos.x) < 2f && timeX > 1f)
			{
				timeX -= 1.2f * Time.deltaTime;
			}
			calculateSmoothPos.x = Mathf.Lerp(cameraT.position.x, targetPos.x, 1.2f * timeX * Time.deltaTime);
			if (timeX < 4f)
			{
				timeX += 0.9f * Time.deltaTime;
			}
		}
		if (followY)
		{
			targetPos.y = heroT.position.y + offsetY;
			checkYpos += Time.deltaTime;
			if (Mathf.Abs(cameraT.position.y - targetPos.y) > 2f)
			{
				checkYpos = 3f;
			}
			if (checkYpos > 2f)
			{
				calculateSmoothPos.y = Mathf.Lerp(cameraT.position.y, targetPos.y, speedGoY * Time.deltaTime);
				if (Mathf.Abs(cameraT.position.y - targetPos.y) > 4.5f)
				{
					speedGoY = speedBigY;
				}
				else if (Mathf.Abs(cameraT.position.y - targetPos.y) < 0.1f)
				{
					checkYpos = 0f;
					speedGoY = 0.6f;
					speedBigY = 3f;
				}
			}
		}
		else
		{
			calculateSmoothPos.y = cameraT.position.y;
		}
		cameraT.position = calculateSmoothPos;
		if (cameraT.position.x > borderX.y)
		{
			cameraT.position = new Vector3(borderX.y, cameraT.position.y, cameraT.position.z);
		}
		else if (cameraT.position.x < borderX.x)
		{
			cameraT.position = new Vector3(borderX.x, cameraT.position.y, cameraT.position.z);
		}
		if (cameraT.position.y < maxDownY)
		{
			cameraT.position = new Vector3(cameraT.position.x, maxDownY, cameraT.position.z);
		}
		else if (cameraT.position.y > maxUpY)
		{
			cameraT.position = new Vector3(cameraT.position.x, maxUpY, cameraT.position.z);
		}
	}
}
