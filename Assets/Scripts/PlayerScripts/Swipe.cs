using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
	private GameManager gm;

	private Vector2 fingerDownPos;
	private Vector2 fingerUpPos;

	public bool detectSwipeAfterRelease = false;

	public float SWIPE_THRESHOLD = 20f;

	[SerializeField]
	private LayerMask tileMask;     // 타일 마스크 받아오기

	private void Start()
	{
		gm = GameManager.GetInstance();
	}

	// Update is called once per frame
	void Update()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
				Ray2D ray = new Ray2D(pos, Vector2.zero);
				RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1, tileMask);
				Tile tile = null;
				if (hit.collider != null)
				{
					if (hit.collider.tag == "Tile")
					{
						tile = hit.collider.GetComponent<Tile>();
						tile.StartTouch();
					}
				}
				fingerUpPos = touch.position;
				fingerDownPos = touch.position;
			}

			//Detects Swipe while finger is still moving on screen
			if (touch.phase == TouchPhase.Moved)
			{
				if (!detectSwipeAfterRelease)
				{
                    Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
                    Ray2D ray = new Ray2D(pos, Vector2.zero);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1, tileMask);
                    Tile tile = null;
                    if (hit.collider != null)
                    {
						if (hit.collider.tag == "Tile")
						{
							tile = hit.collider.GetComponent<Tile>();
							tile.SwipeTouch(tile.transform.position);
						}
                    }

                    fingerDownPos = touch.position;
					DetectSwipe();
				}
			}

			//Detects swipe after finger is released from screen
			if (touch.phase == TouchPhase.Ended)
			{
				fingerDownPos = touch.position;
				DetectSwipe();

				if (gm.player_move.isStart)
				{
					gm.battleController.ResetHandler();
				}
			}
		}
	}

	void DetectSwipe()
	{

		if (VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
		{
			Debug.Log("Vertical Swipe Detected!");
			if (fingerDownPos.y - fingerUpPos.y > 0)
			{
				OnSwipeUp();
			}
			else if (fingerDownPos.y - fingerUpPos.y < 0)
			{
				OnSwipeDown();
			}
			fingerUpPos = fingerDownPos;

		}
		else if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
		{
			Debug.Log("Horizontal Swipe Detected!");
			if (fingerDownPos.x - fingerUpPos.x > 0)
			{
				OnSwipeRight();
			}
			else if (fingerDownPos.x - fingerUpPos.x < 0)
			{
				OnSwipeLeft();
			}
			fingerUpPos = fingerDownPos;

		}
		else
		{
			Debug.Log("No Swipe Detected!");
		}
	}

	float VerticalMoveValue()
	{
		return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
	}

	float HorizontalMoveValue()
	{
		return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
	}

	// 위쪽
	void OnSwipeUp()
	{
		//gm.player_move.Move(0, 3);
	}

	// 아래쪽
	void OnSwipeDown()
	{
		//gm.player_move.Move(0, -3);
	}

	// 왼쪽
	void OnSwipeLeft()
	{
		//gm.player_move.Move(-3, 0);
	}

	// 오른쪽
	void OnSwipeRight()
	{
		//gm.player_move.Move(3, 0);
	}
}