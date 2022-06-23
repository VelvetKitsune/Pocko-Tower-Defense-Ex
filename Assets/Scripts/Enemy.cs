using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	
	private bool isEnemyDead = false;
	public float startSpeed = 10f;
	public float startHealth = 10;
	[HideInInspector]
	public float speed;	
	private float health;
	public int moneyDropped = 10;
	
	public GameObject deathEffect;
	
	[Header ("Unity Stuff")]
	public Image healthBar;
	
	void Start()
	{
		speed = startSpeed;
		health = startHealth;
	}
	
	public void TakeDamage (float amount)
	{
		health -= amount;
		healthBar.fillAmount = health / startHealth;
		
		if (health <= 0 && !isEnemyDead)
		{
			Die();
			isEnemyDead = true;
		}
	}
	
	public void Slow (float pct)
	{
		speed = startSpeed * (1f - pct);
	}
	
	void Die()
	{
		PlayerStats.Money += moneyDropped;
		GameObject effect = (GameObject)Instantiate (deathEffect, transform.position, Quaternion.identity);
		Destroy (effect, 5f);
		
		WaveSpawner.EnemiesAlive--;
		
		Destroy(gameObject);
	}
}
