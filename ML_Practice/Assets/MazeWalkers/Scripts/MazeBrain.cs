using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBrain : MonoBehaviour 
{
	private const int DNA_LENGTH = 2;
	/*
	 0: forward
	 1: back
	 2: left
	 3: right
	 */
	private const int DNA_MAXVALUE = 4;

	public DNA Dna;
	public float Distance;
	private Vector3 m_startPos;

	public void Init()
	{
		Dna = new DNA (2, 4);
		m_startPos = Vector3.zero;
	}

	void OnCollisionEnter(Collision obj)
	{
		if(obj.gameObject.tag == "dead")
		{
			this.gameObject.SetActive (false);
			Distance = 0f;
		}
	}

	private	void Update()
	{
		RaycastHit hit;
		Physics.Raycast (transform.position, transform.forward * 2f, out hit);
		bool isHitWall = false;;
		if (hit.collider != null)
		{
			if (hit.collider.gameObject.tag == "wall")
			{
				isHitWall = true;
			}
		}

		float move = 0f;
		float turn = 0f;

		if (isHitWall)
		{
			if (Dna.GetGene (1) == 0)
			{
				move = 1f;
			} else if (Dna.GetGene (1) == 1)
			{
				move = -1f;
			} else if (Dna.GetGene (1) == 2)
			{
				turn = 1f;
			} else if (Dna.GetGene (1) == 3)
			{
				turn = -1f;
			}

		} else
		{
			if (Dna.GetGene (0) == 0)
			{
				move = 1f;
			} else if (Dna.GetGene (0) == 1)
			{
				move = -1f;
			} else if (Dna.GetGene (0) == 2)
			{
				turn = 1f;
			} else if (Dna.GetGene (0) == 3)
			{
				turn = -1f;
			}
		}

		transform.Translate (new Vector3 (0, 0, move * 5f * Time.deltaTime));
		transform.Rotate( new Vector3(0,90f * turn , 0));

		Distance = Vector3.Distance (m_startPos, transform.position);
	}

}
