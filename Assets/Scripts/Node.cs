using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	public Color hoverColor;
	public Color notEnoughMoneyColour;
	public Vector3 positionOffset;
	
	[HideInInspector]
	public GameObject turret;
	[HideInInspector]
	public TurretBlueprint turretBlueprint;
	[HideInInspector]
	public bool isUpgraded = false;
	
	private Renderer rend;
	private Color startColor;
	
	BuildManager buildManager;
	
	void Start()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;
		
		buildManager = BuildManager.instance;
	}
	
	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}
	
	void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		
		if (turret != null)
		{
			buildManager.SelectNode (this);
			return;
		}
		
		if (!buildManager.CanBuild)
			return;
		
		BuildTurret (buildManager.GetTurretToBuild());
	}
	
	void BuildTurret (TurretBlueprint blueprint)
	{
		if (PlayerStats.Money < blueprint.cost)
		{
			Debug.Log ("Not enough Ruld!");
			return;
		}
		
		PlayerStats.Money -= blueprint.cost;
		
		GameObject _turret = (GameObject)Instantiate (blueprint.prefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;
		
		turretBlueprint = blueprint;
		
		GameObject effect = (GameObject)Instantiate (buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy (effect, 5f);
		
		//Debug.Log ("Tower built, Ruld remaining: " + PlayerStats.Money);
	}
	
	public void UpgradeTurret()
	{
		if (PlayerStats.Money < turretBlueprint.upgradeCost)
		{
			Debug.Log ("Not enough Ruld!");
			return;
		}
		
		PlayerStats.Money -= turretBlueprint.upgradeCost;
		
		// Remove old tower.
		Destroy (turret);
		
		//Create new upgraded one in its place.
		GameObject _turret = (GameObject)Instantiate (turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;
		
		GameObject effect = (GameObject)Instantiate (buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy (effect, 5f);
		
		isUpgraded = true;
		
		Debug.Log ("Tower upgraded, Ruld remaining: " + PlayerStats.Money);
	}
	
	public void SellTurret()
	{
		PlayerStats.Money += turretBlueprint.sellAmount;
		
		GameObject effect = (GameObject)Instantiate (buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy (effect, 5f);
		
		Destroy (turret);
		turretBlueprint = null;
	}
	
	void OnMouseEnter()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		
		if (!buildManager.CanBuild)
			return;
		
		if (buildManager.HasMoney)
		{
			rend.material.color = hoverColor;
		} else {
			rend.material.color = notEnoughMoneyColour;
		}
	}
	
	void OnMouseExit()
	{
		rend.material.color = startColor;
	}
}
