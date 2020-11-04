using UnityEngine;

public class TouchController : MonoBehaviour
{
	[Header("Events")]
	public GameEvent eventMoveLeft;
	public GameEvent eventMoveRight;
	public GameEvent eventRotate;
	public GameEvent eventDash;

	Vector2 touchMovement;

	[Range(50, 150)]
	public int minDragDistance = 50;
	[Range(20, 250)]
	public int minSwipeDistance = 20;

	enum Direction { none, left, right, up, down }
	float timeToNextDrag;
	float timeToNextSwipe;

	[Range(0.05f, 1f)]
	public float minTimeToDrag = 0.075f;

	[Range(0.05f, 1f)]
	public float minTimeToSwipe = 0.2f;

	void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];

			if (touch.phase == TouchPhase.Began)
			{
				touchMovement = Vector2.zero;
			}
			else if (touch.phase == TouchPhase.Moved)
			{
				touchMovement += touch.deltaPosition;
				if (touchMovement.magnitude > minDragDistance)
				{
					DoDragMove(touchMovement);
				}
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				if (touchMovement.magnitude > minSwipeDistance)
				{
					DoSwipeMove(touchMovement);
				}
			}
		}
	}

	private void DoSwipeMove(Vector3 direction)
	{
		if (GetDirection(direction) == Direction.up && Time.time > timeToNextSwipe)
		{
			eventRotate.Raise();
		}
		else if (GetDirection(direction) == Direction.down && Time.time > timeToNextSwipe)
		{
			eventDash.Raise();
		}
		timeToNextSwipe = Time.time + minTimeToSwipe;
	}

	private void DoDragMove(Vector3 direction)
	{
		if (GetDirection(direction) == Direction.right && Time.time > timeToNextDrag)
		{
			eventMoveRight.Raise();
			timeToNextDrag = Time.time + minTimeToDrag;
		}
		else if (GetDirection(direction) == Direction.left && Time.time > timeToNextDrag)
		{
			eventMoveLeft.Raise();
			timeToNextDrag = Time.time + minTimeToDrag;
		}
	}

	Direction GetDirection(Vector2 swipeMovement)
	{
		var swipeDir = Direction.none;
		if (Mathf.Abs(swipeMovement.x) > Mathf.Abs(swipeMovement.y))
		{
			swipeDir = (swipeMovement.x >= 0) ? Direction.right : Direction.left;
		}
		else
		{
			swipeDir = (swipeMovement.y >= 0) ? Direction.up : Direction.down;
		}
		return swipeDir;
	}
}
