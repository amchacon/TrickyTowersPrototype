using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PieceController : MonoBehaviour
{
	public GameConfig gameConfig;
	public float dashForce = 3;
	public bool canRotate = true;
	[Tooltip("Collision Layers to validate that a piece was placed")]
	public LayerMask filterLayers;

	[Header("Component References")]
	[SerializeField] private Rigidbody myRigidbody;
	[SerializeField] private Transform myTransform;
	public DestroyOnFell destroyOnFellScript;

	[Header("GameEvents")]
	public GameEvent OnPieceFell;
	public GameEvent OnPieceDestroyed;
	public GameEvent OnPiecePlaced;

	[Header("SyncVars")]
	public BoolValue paused;
	public FloatValue currentPieceSpeed;
	
	private bool canMove = false;

	private void Awake()
	{
		if (myRigidbody == null) myRigidbody = GetComponent<Rigidbody>();
		if (myTransform == null) myTransform = transform;
	}

	private void Start()
	{
		canMove = true;
		if (gameConfig.gameMode == GameConfig.GameMode.Versus)
			StartCoroutine(AIMove());
	}

	private void FixedUpdate()
	{
		if (!canMove || paused.value) return;
		VerticalMove();
	}

	private void VerticalMove()
	{
		var nextPosition = myTransform.position + Vector3.down * currentPieceSpeed.value * Time.fixedDeltaTime;
		myRigidbody.MovePosition(nextPosition);
	}

	#region Movements contolled by AI
	IEnumerator AIMove()
	{
		yield return new WaitForSeconds(Random.Range(0.25f, 1f));
		int dir = Random.Range(-1, 2); //0- esquerda; 1- direita
		int mag = Random.Range(1, 4);
		int rot = Random.Range(0, 3);
		int dashChance = Random.Range(0, 100);

		if (canMove)
		{
			for (int i = 0; i < mag; i++)
			{
				if (rot > 0)
				{
					Rotate();
					rot--;
				}
				Move(new Vector3(dir, 0, 0));
				yield return new WaitForSeconds(Random.Range(0.1f, 0.75f));
				if (dashChance >= 40)
				{
					Dash();
				}
			}
		}
	}
	#endregion

	#region Movements by Player Input (setted by inspector on EventListener component)
	public void Rotate()
	{
		if (!canMove || paused.value || !canRotate) return;
		myTransform.Rotate(new Vector3(0, 0, 90));
	}

	public void MoveLeft()
	{
		Move(Vector3.left);
	}

	public void MoveRight()
	{
		Move(Vector3.right);
	}

	private void Move(Vector3 direction)
	{
		if (!canMove || paused.value) return;
		//TODO: clamp position
		myTransform.position += direction;
	}

	public void Dash()
	{
		if (!canMove || paused.value) return;
		myRigidbody.AddForce(Vector3.down * dashForce, ForceMode.Impulse);
	}
	#endregion

	//TODO: create specific component for CollisionCheck
	private void OnCollisionEnter(Collision collision)
	{
		if (!canMove) return;
		LayerMask otherColliderLayer = collision.gameObject.layer;
		if (filterLayers.Contains(otherColliderLayer))
		{
			StopAllCoroutines();

			canMove = false;
			myRigidbody.drag = 0.5f;
			myRigidbody.useGravity = true;
			OnPiecePlaced.Raise();
		}
	}

	//Object is destroyed by OnBecameInvisible (script attached to the child obj (w/MeshRenderer))
	private void OnDestroy() //TODO: consider to create specific component for this too
	{
		if (canMove) //free fall: piece didnt touch the base or any other piece before become invisible
		{
			//The piece fell without touching the base or another piece! (Spawner is observing this event)
			OnPieceFell.Raise();
		}
		//Anyway, touching the base/another piece or not, if the piece falls, the player loses a life
		ActivePieces.RemovePiece(this);
		OnPieceDestroyed.Raise();
	}

	/// Freeze all placed pieces
	public void OnMilestoneReachedHandler()
	{
		destroyOnFellScript.milestoneReached = true; //avoid pieces to be destroyed if become invisible
		myRigidbody.isKinematic = true;
		myRigidbody.useGravity = false;
		this.enabled = false;
		//TODO: apply fancy shader to all pieces milestoned         
	}
}