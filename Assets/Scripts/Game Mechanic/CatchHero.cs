using System;
using UnityEngine;

public class CatchHero
{
	private Hero hero;

	private InterfaceDragBox boxS;

	private Transform boxMove;

	private float sizeOfBox;

	private float distPerObj;

	private float directX;

	private bool goRightPos;

	private bool rightPos;

	private bool directAnDrag = true;

	private bool directHero;

	private float posAnDrag;

	private float prevPos;

	public void Init(Hero h)
	{
		hero = h;
	}

	public bool StartDragObj(Transform objT)
	{
		if (hero.onPlatform && !hero.stopActions && !hero.catchMode && !hero.jump && !hero.fall)
		{
			boxMove = objT;
			boxS = boxMove.GetComponent<InterfaceDragBox>();
			hero.maxSpeed = hero.SPEED_MAX * 0.5f;
			sizeOfBox = boxS.getsizeOfBox;
			distPerObj = boxS.getdistPerObj;
			rightPos = false;
			directHero = hero.direct;
			if (hero.heroT.position.x < boxMove.position.x)
			{
				hero.direct = true;
				hero.catchDistFall = -0.5f;
				if (hero.heroT.position.x > boxMove.position.x - distPerObj)
				{
					goRightPos = false;
					directX = -0.025f;
				}
				else
				{
					goRightPos = true;
					directX = 0.025f;
				}
			}
			else
			{
				hero.direct = false;
				hero.catchDistFall = 0.5f;
				if (hero.heroT.position.x < boxMove.position.x + distPerObj)
				{
					goRightPos = true;
					directX = 0.025f;
				}
				else
				{
					goRightPos = false;
					directX = -0.025f;
				}
			}
			hero.sp.flipX = !hero.direct;
			hero.maxSpeed = 0.05f;
			hero.catchMode = true;
			hero.stopActions = true;
			Singleton<ManagerFunctions>.use.addFunction(ToCatch);
			hero.SetAn("TakeBox");
			return true;
		}
		return false;
	}

	private void ToCatch()
	{
		if (!rightPos)
		{
			if (hero.direct)
			{
				if (goRightPos)
				{
					MoveHero();
					if (hero.heroT.position.x >= boxMove.position.x - distPerObj)
					{
						SetToBoxPos();
					}
				}
				else
				{
					MoveHero();
					if (hero.heroT.position.x <= boxMove.position.x - distPerObj)
					{
						SetToBoxPos();
					}
				}
			}
			else if (goRightPos)
			{
				MoveHero();
				if (hero.heroT.position.x >= boxMove.position.x + distPerObj)
				{
					SetToBoxPos();
				}
			}
			else
			{
				MoveHero();
				if (hero.heroT.position.x <= boxMove.position.x + distPerObj)
				{
					SetToBoxPos();
				}
			}
		}
		if (hero.AnCompleted())
		{
			hero.SetAn("DragBox");
			hero.an.speed = 0f;
			posAnDrag = 0f;
			directAnDrag = true;
			hero.blockM = OnBlockCatch;
			SetToBoxPos();
			prevPos = hero.heroT.position.x;
			hero.stopActions = false;
			Singleton<ManagerFunctions>.use.removeFunction(ToCatch);
			Singleton<ManagerFunctions>.use.addFunction(CatchActions);
		}
	}

	private void CatchActions()
	{
		if (Mathf.Abs(hero.goX) > 0f)
		{
			if (Mathf.Abs(prevPos - hero.heroT.position.x) > 0.6f * Time.deltaTime)
			{
				hero.an.speed = 1f;
				boxS.PlaySo(n: true);
				prevPos = hero.heroT.position.x;
			}
			else
			{
				hero.an.speed = 0f;
				boxS.PlaySo(n: false);
			}
			if (hero.goX > 0f)
			{
				if (!directAnDrag)
				{
					posAnDrag = hero.an.GetCurrentAnimatorStateInfo(0).normalizedTime;
					if (directHero)
					{
						hero.an.Play("DragBox", -1, posAnDrag);
					}
					else
					{
						hero.an.Play("DragBox2", -1, posAnDrag);
					}
					directAnDrag = true;
				}
			}
			else if (directAnDrag)
			{
				posAnDrag = hero.an.GetCurrentAnimatorStateInfo(0).normalizedTime;
				if (directHero)
				{
					hero.an.Play("DragBox2", -1, posAnDrag);
				}
				else
				{
					hero.an.Play("DragBox", -1, posAnDrag);
				}
				directAnDrag = false;
			}
		}
		else
		{
			hero.an.speed = 0f;
			boxS.PlaySo(n: false);
		}
		if (boxS.getRbStatusFall())
		{
			hero.actionM = null;
			Hero obj = hero;
			obj.actionM = (Hero.SetMethodAction)Delegate.Combine(obj.actionM, new Hero.SetMethodAction(hero.EmptyFunction));
			CatchBreak();
		}
	}

	private void ToCatchEnd()
	{
		if (hero.AnCompleted())
		{
			hero.idle = false;
			BreakCatch();
			hero.SetIdle();
			hero.stopActions = false;
			hero.maxSpeed = hero.SPEED_MAX;
			Singleton<ManagerFunctions>.use.removeFunction(ToCatchEnd);
		}
	}

	public void CatchBreak()
	{
		hero.maxSpeed = hero.SPEED_MAX;
		hero.an.speed = 1f;
		Singleton<ManagerFunctions>.use.removeFunction(CatchActions);
		Singleton<ManagerFunctions>.use.removeFunction(ToCatch);
		boxS.ReturnBlocks();
		BreakCatch();
		if (hero.heroT.position.x < boxMove.position.x)
		{
			hero.direct = true;
		}
		else
		{
			hero.direct = false;
		}
		hero.stopActions = true;
		Singleton<ManagerFunctions>.use.addFunction(ToCatchEnd);
		hero.SetAn("TakeBox2");
	}

	private void MoveHero()
	{
		hero.heroT.position = new Vector2(hero.heroT.position.x + directX, hero.heroT.position.y);
	}

	private void SetToBoxPos()
	{
		if (hero.direct)
		{
			hero.heroT.position = new Vector2(boxMove.position.x - distPerObj, hero.heroT.position.y);
		}
		else
		{
			hero.heroT.position = new Vector2(boxMove.position.x + distPerObj, hero.heroT.position.y);
		}
		rightPos = true;
	}

	public void SetBoxPos()
	{
		if (hero.heroT.position.x < boxMove.position.x)
		{
			boxMove.position = new Vector2(hero.heroT.position.x + distPerObj, boxMove.position.y);
		}
		else
		{
			boxMove.position = new Vector2(hero.heroT.position.x - distPerObj, boxMove.position.y);
		}
	}

	private bool OnBlockCatch(LayerMask n)
	{
		Vector3 position = boxMove.position;
		position.y += 0.4f;
		Collider2D[] array = Physics2D.OverlapCircleAll(position, sizeOfBox, hero.blockBoxLayer);
		if (array.Length != 0 && isBlockOnHero(array) && ((hero.speedX < 0f && hero.heroT.position.x > array[hero.iC].transform.position.x) || (hero.speedX > 0f && hero.heroT.position.x < array[hero.iC].transform.position.x)))
		{
			return true;
		}
		array = Physics2D.OverlapCircleAll(hero.heroT.position, sizeOfBox, n);
		if (array.Length != 0 && isBlockOnHero(array))
		{
			return true;
		}
		Vector3 position2 = hero.heroT.position;
		position2.y += 0.4f;
		Collider2D[] array2 = Physics2D.OverlapCircleAll(position2, 0.4f, hero.blockBoxLayer);
		if (array2.Length != 0 && isBlockOnHero(array2) && ((hero.speedX < 0f && hero.heroT.position.x > array2[hero.iC].transform.position.x) || (hero.speedX > 0f && hero.heroT.position.x < array2[hero.iC].transform.position.x)))
		{
			return true;
		}
		array2 = Physics2D.OverlapCircleAll(hero.heroT.position, 0.4f, n);
		if (array2.Length != 0 && isBlockOnHero(array2))
		{
			return true;
		}
		return false;
	}

	private bool isBlockOnHero(Collider2D[] colliders)
	{
		float num = hero.heroT.position.y + 0.8f;
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject.GetInstanceID() != boxS.getBlockID && hero.heroT.position.y < colliders[i].transform.position.y + colliders[i].transform.localScale.y / 2f && num > colliders[i].transform.position.y - colliders[i].transform.localScale.y / 2f)
			{
				hero.iC = i;
				return true;
			}
		}
		return false;
	}

	private void BreakCatch()
	{
		hero.catchMode = false;
		hero.blockM = hero.OnBlock;
	}
}
