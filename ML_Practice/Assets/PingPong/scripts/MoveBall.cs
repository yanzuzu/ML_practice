using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
	public class MoveBall : MonoBehaviour 
	{
		private const float BALL_SPEED = 600;

		public AudioSource Blip;
		public AudioSource Blop;

		private Vector3 m_startPos;
		private Rigidbody2D m_rigid;

		private int m_playerScore = 0;
		private int m_aiScore = 0;

		void Start () 
		{
			m_startPos = transform.position;
			m_rigid = GetComponent<Rigidbody2D> ();
			ResetBall ();
		}

		void OnCollisionEnter2D( Collision2D colld )
		{
			if (colld.gameObject.tag == "frontWall")
			{
				Blop.Play ();
				m_aiScore++;
			} else if (colld.gameObject.tag == "backwall")
			{
				Blop.Play ();
				m_playerScore++;
			}else
			{
				Blip.Play ();
			}
		}

		private void ResetBall()
		{
			transform.position = m_startPos;
			m_rigid.velocity = Vector3.zero;
			Vector3 force = new Vector3 (Random.Range (100, 300),
				                Random.Range (-100, 100), 0).normalized;
			m_rigid.AddForce (force * BALL_SPEED);
		}

		void Update () 
		{
			if (UnityEngine.Input.GetKeyDown (KeyCode.Space))
			{
				ResetBall ();
				ResetScore ();
			}
		}

		public int GetScore(bool isPlayer )
		{
			return isPlayer ? m_playerScore : m_aiScore;
		}

		public void ResetScore()
		{
			m_playerScore = 0;
			m_aiScore = 0;
		}
	}
}
