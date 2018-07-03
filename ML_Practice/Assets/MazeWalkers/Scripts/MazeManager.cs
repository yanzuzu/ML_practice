using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MazeManager : MonoBehaviour 
{

	public GameObject botPrefab;
	public int populationSize = 50;
	List<MazeBrain> population = new List<MazeBrain>();
	public static float elapsed = 0;
	public float trialTime = 10;
	int generation = 1;

	public GameObject StartPosObj;
	public GameObject EndPosObj;

	GUIStyle guiStyle = new GUIStyle();
	void OnGUI()
	{
		guiStyle.fontSize = 25;
		guiStyle.normal.textColor = Color.white;
		GUI.BeginGroup (new Rect (10, 10, 250, 150));
		GUI.Box (new Rect (0,0,140,140), "Stats", guiStyle);
		GUI.Label(new Rect (10,25,200,30), "Gen: " + generation, guiStyle);
		GUI.Label(new Rect (10,50,200,30), string.Format("Time: {0:0.00}",elapsed), guiStyle);
		GUI.Label(new Rect (10,75,200,30), "Population: " + population.Count, guiStyle);
		GUI.EndGroup ();
	}


	// Use this for initialization
	void Start () {
		for(int i = 0; i < populationSize; i++)
		{
			GameObject b = Instantiate(botPrefab, StartPosObj.transform.position, StartPosObj.transform.rotation);
			MazeBrain brain = b.GetComponent<MazeBrain> ();
			brain.Init();
			population.Add(brain);
		}
	}

	MazeBrain Breed(MazeBrain parent1, MazeBrain parent2)
	{
		GameObject offspring = Instantiate(botPrefab,StartPosObj.transform.position, StartPosObj.transform.rotation);
		MazeBrain b = offspring.GetComponent<MazeBrain>();
		if(Random.Range(0,100) == 1) //mutate 1 in 100
		{
			b.Init();
			b.Dna.Mutate();
		}
		else
		{ 
			b.Init();
			b.Dna.Combine(parent1.Dna,parent2.Dna);
		}
		return b;
	}

	void BreedNewPopulation()
	{
		List<MazeBrain> sortedList = population.OrderBy(o => o.Distance).ToList();

		population.Clear();
		for (int i = (int) (sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
		{
			population.Add(Breed(sortedList[i], sortedList[i + 1]));
			population.Add(Breed(sortedList[i + 1], sortedList[i]));
		}
		//destroy all parents and previous population
		for(int i = 0; i < sortedList.Count; i++)
		{
			Destroy(sortedList[i].gameObject);
		}
		generation++;
	}

	// Update is called once per frame
	void Update () {
		elapsed += Time.deltaTime;
		if(elapsed >= trialTime)
		{
			BreedNewPopulation();
			elapsed = 0;
		}
	}
}
