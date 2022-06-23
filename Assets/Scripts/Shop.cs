using UnityEngine;

public class Shop : MonoBehaviour
{
	public TurretBlueprint standardTurret;
	public TurretBlueprint missileLauncher;
	public TurretBlueprint laserBeamer;
	
	BuildManager buildmanager;
	
	void Start()
	{
		buildmanager = BuildManager.instance;
	}
	
	public void SelectStandardTurret() //For the Arrow Tower
	{
		Debug.Log ("Arrow Tower!");
		buildmanager.SelectTurretToBuild (standardTurret);
	}
	public void SelectAnotherTurret() //For the Siege Tower
	{
		Debug.Log ("Siege Tower!");
		buildmanager.SelectTurretToBuild (missileLauncher);
	}
	public void SelectLaserTurret() //For the Laser Tower
	{
		Debug.Log ("Laser Tower!");
		buildmanager.SelectTurretToBuild (laserBeamer);
	}
}
