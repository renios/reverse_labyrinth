using UnityEngine;
using System.Collections;
using System.Linq;
using Enums;

public class Player : MonoBehaviour {

	public GravityDirection initGravityDirection;

	private GameObject presentPositionBlock;
	private GravityDirection presentGravityDirection;
	private TurnViewer turnViewer;
	
	// Use this for initialization
	void Start () {
		turnViewer = FindObjectOfType<TurnViewer>();
		SetInitPosition();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
			MoveNextBlock();
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
			MoveLeftOnBlock();
		else if (Input.GetKeyDown(KeyCode.RightArrow))
			MoveRightOnBlock();
	}

	void SetGravityDirection(GravityDirection direction)
	{
		presentGravityDirection = direction;
	}

	void SetPosition(GameObject block)
	{
		presentPositionBlock = block;
	}

	void UpdatePlayerPosition()
	{
		switch (presentGravityDirection)
		{
			case GravityDirection.Up:
				this.transform.position = presentPositionBlock.transform.position + new Vector3(0, -1.45f, 0);
				this.transform.rotation = Quaternion.Euler(180, 0, 0);
				break;
			case GravityDirection.Down:
				this.transform.position = presentPositionBlock.transform.position + new Vector3(0, 1.45f, 0);
				this.transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
			case GravityDirection.Left:
				this.transform.position = presentPositionBlock.transform.position + new Vector3(1.45f, 0, 0);
				this.transform.rotation = Quaternion.Euler(0, 0, -90);
				break;
			case GravityDirection.Right:
				this.transform.position = presentPositionBlock.transform.position + new Vector3(-1.45f, 0, 0);
				this.transform.rotation = Quaternion.Euler(0, 0, 90);
				break;
		}

		turnViewer.Increase();
	}

	void MoveLeftOnBlock()
	{
		presentGravityDirection = GetLeftDirection(presentGravityDirection);
		UpdatePlayerPosition();
	}

	void MoveRightOnBlock()
	{
		presentGravityDirection = GetRightDirection(presentGravityDirection);
		UpdatePlayerPosition();
	}

	void MoveNextBlock()
	{
		GameObject nextBlock = presentPositionBlock.GetComponent<BaseBlock>().GetNextBlockByDirection(presentGravityDirection);
		if (nextBlock == null) return;
		SetPosition(nextBlock);
		UpdatePlayerPosition();
	}

	GameObject FindStartBlock()
	{
		BaseBlock[] blocks = FindObjectsOfType<BaseBlock>();
		GameObject startBlock = blocks.First(k => k.gameObject.tag == "Start").gameObject;

		return startBlock;
	}

	void SetInitPosition()
	{
		GameObject startBlock = FindStartBlock();
		SetPosition(startBlock);
		SetGravityDirection(initGravityDirection);
		UpdatePlayerPosition();
	}

	GravityDirection GetLeftDirection(GravityDirection direction)
	{
		switch (direction)
		{
			case GravityDirection.Up: return GravityDirection.Left;
			case GravityDirection.Down: return GravityDirection.Right;
			case GravityDirection.Left: return GravityDirection.Down;
			case GravityDirection.Right: return GravityDirection.Up;
			default: return direction;
		}
	}

	GravityDirection GetRightDirection(GravityDirection direction)
	{
		switch (direction)
		{
			case GravityDirection.Up: return GravityDirection.Right;
			case GravityDirection.Down: return GravityDirection.Left;
			case GravityDirection.Left: return GravityDirection.Up;
			case GravityDirection.Right: return GravityDirection.Down;
			default: return direction;
		}
	}
}
