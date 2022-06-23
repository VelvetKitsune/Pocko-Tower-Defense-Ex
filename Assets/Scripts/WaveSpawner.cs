using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public static int EnemiesAlive = 0;
	
	public Wave[] waves;
	
	public Transform spawnPoint;
	
	public float timeBetweenWaves = 10f; //Subsequent wave spawn timer.
	private float countdown = 2f; //First use is first wave spawn timer.
	
	public Text waveCountdownText;
	
	private int waveIndex = 0;
	
	void Update()
	{
		if (EnemiesAlive > 0)
			return;
		
		if (countdown <= 0f)
		{
			StartCoroutine (SpawnWave() );
			countdown = timeBetweenWaves;
			return;
		}
		
		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp (countdown, 0f, Mathf.Infinity);
		waveCountdownText.text = string.Format ("{0:00}", countdown);
	}
	
	IEnumerator SpawnWave()
	{
		PlayerStats.Rounds++;
		
		Wave wave = waves [waveIndex];
		
		EnemiesAlive = wave.count;
		
		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy (wave.enemy);
			yield return new WaitForSeconds (1f / wave.rate);
		}
		waveIndex++;
		
		if (waveIndex == waves.Length)
		{
			waveIndex--;
			Debug.Log ("Win!");
			//this.enabled = false;
		}
	}
	
	void SpawnEnemy (GameObject enemy)
	{
		Instantiate (enemy, spawnPoint.position, spawnPoint.rotation);
	}
}