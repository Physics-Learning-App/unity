using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class monsterResponse    {
	public int id;
	public string name;
	public string image;
	public string difficulty;
	public List<Problem> Problems;
}

[Serializable]
public class Problem    {
	public int id;
	public string question;
	public string answer; // 
	public int monster_id;
	public string choices; // split by ',' 
	public string explanation;
}
