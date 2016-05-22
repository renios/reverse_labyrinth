using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;

public class BaseBlock : MonoBehaviour {

	private GameObject upBlock;
	private GameObject downBlock;
	private GameObject leftBlock;
	private GameObject rightBlock;

	// Use this for initialization
	void Start () {
		upBlock = GetUpBlock();
		downBlock = GetDownBlock();
		leftBlock = GetLeftBlock();
		rightBlock = GetRightBlock();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject GetNextBlockByDirection(GravityDirection gravityDirection)
	{
		switch (gravityDirection)
		{
			case GravityDirection.Up: return upBlock;
			case GravityDirection.Down: return downBlock;
			case GravityDirection.Left: return leftBlock;
			case GravityDirection.Right: return rightBlock;
			default: return null;
		}
	}

	GameObject GetUpBlock()
	{
		BaseBlock[] blocks = FindObjectsOfType<BaseBlock>();
		IEnumerable<GameObject> upBlocks = blocks.Where(block => ((block.transform.position.x == this.transform.position.x) &&
																  (block.transform.position.y > this.transform.position.y)))
												 .Select(block => block.gameObject);
		IEnumerable<GameObject> sortedBlocksIterator = upBlocks.OrderBy(k => k.transform.position.y);
		return sortedBlocksIterator.FirstOrDefault();
	}

	GameObject GetDownBlock()
	{
		BaseBlock[] blocks = FindObjectsOfType<BaseBlock>();
		IEnumerable<GameObject> downBlocks = blocks.Where(block => ((block.transform.position.x == this.transform.position.x) &&
																    (block.transform.position.y < this.transform.position.y)))
											   	   .Select(block => block.gameObject);
		IEnumerable<GameObject> sortedBlocksIterator = downBlocks.OrderByDescending(k => k.transform.position.y);
		return sortedBlocksIterator.FirstOrDefault();
	}

	GameObject GetLeftBlock()
	{
		BaseBlock[] blocks = FindObjectsOfType<BaseBlock>();
		IEnumerable<GameObject> leftBlocks = blocks.Where(block => ((block.transform.position.y == this.transform.position.y) &&
																    (block.transform.position.x < this.transform.position.x)))
												   .Select(block => block.gameObject);
		IEnumerable<GameObject> sortedBlocksIterator = leftBlocks.OrderByDescending(k => k.transform.position.x);
		return sortedBlocksIterator.FirstOrDefault();
	}

	GameObject GetRightBlock()
	{
		BaseBlock[] blocks = FindObjectsOfType<BaseBlock>();
		IEnumerable<GameObject> rightBlocks = blocks.Where(block => ((block.transform.position.y == this.transform.position.y) &&
																     (block.transform.position.x > this.transform.position.x)))
												    .Select(block => block.gameObject);
		IEnumerable<GameObject> sortedBlocksIterator = rightBlocks.OrderBy(k => k.transform.position.x);
		return sortedBlocksIterator.FirstOrDefault();
	}
}
