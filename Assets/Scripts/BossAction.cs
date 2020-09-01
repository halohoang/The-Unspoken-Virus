using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour
{
	public float _speed;

	public float _startWaitTime;

	private float _waitTime;

	public Transform[] moveSpots;
	private int randomSpot;

	 void Start()
	 {
		_waitTime = _startWaitTime;
		randomSpot = Random.Range(0, moveSpots.Length);
	
	 }

	 void Update()
	 {
		transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, _speed * Time.deltaTime);
		if(Vector2.Distance (transform.position, moveSpots[randomSpot].position)< 0.2f)
		{
			if(_waitTime <= 0)
			{
				randomSpot = Random.Range(0, moveSpots.Length);
				_waitTime = _startWaitTime;
			}
			else
			{
				_waitTime -= Time.deltaTime; 
			}
		}
	 }


}
