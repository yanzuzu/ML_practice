using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
	public class MoveBall : MonoBehaviour 
	{
		private const float BALL_SPEED = 400;

		public AudioSource Blip;
		public AudioSource Blop;

		private Vector3 m_startPos;
		private Rigidbody2D m_rigid;

		void Start () 
		{
			m_startPos = transform.position;
			m_rigid = GetComponent<Rigidbody2D> ();
			ResetBall ();
		}

		void OnCollisionEnter2D( Collision2D colld )
		{
			if (colld.gameObject.tag == "backwall")
			{
				Blop.Play ();
			} else
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
			}
		}
	}
}
