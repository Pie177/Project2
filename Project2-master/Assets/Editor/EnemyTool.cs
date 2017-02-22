using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Sprites;

public class EnemyTool : EditorWindow {

	public List<Enemies> enemyList = new List<Enemies>();
	string nameString;
	bool nameFlag = false;
	string[] enemyNames;

	int currentChoice = 0;
	int lastChoice;
	List<string> enemyNameList = new List<string>();

	int health;
	int attack;
	int defense;
	int agility;

	float attackTime;
	bool magicUser;
	int mana;

	Sprite enemySprite;

	[MenuItem("Custom Tools/EnemyTool %x")]
		private static void blahblah()
	{
		EditorWindow.GetWindow<EnemyTool> ();
	}

	void Awake()
	{
		GetEnemies ();
	}

	void OnGUI ()
	{
		currentChoice = EditorGUILayout.Popup (currentChoice, enemyNames);



		if (GUILayout.Button ("Button")) 
		{
			GetEnemies ();
		}

		nameString = EditorGUILayout.TextField ("Name", nameString);
		EditorGUILayout.LabelField("Health");
		health = EditorGUILayout.IntSlider (health, 1, 300);

		EditorGUILayout.LabelField("Attack");
		attack = EditorGUILayout.IntSlider (attack, 1, 100);

		EditorGUILayout.LabelField("Defense");
		defense = EditorGUILayout.IntSlider (defense, 1, 100);

		EditorGUILayout.LabelField("Agility");
		agility = EditorGUILayout.IntSlider (agility, 1, 100);

		EditorGUILayout.LabelField("Attack Speed");
		attackTime = EditorGUILayout.FloatField (attackTime);

		magicUser = EditorGUILayout.BeginToggleGroup ("Magic User", magicUser);
		EditorGUILayout.LabelField("Mana Pool");
		mana = EditorGUILayout.IntSlider (mana, 0, 100);
		EditorGUILayout.EndToggleGroup ();

		enemySprite = EditorGUILayout.ObjectField (enemySprite, typeof(Sprite), true) as Sprite;
		if(enemySprite != null)
		{
			Texture2D atext = SpriteUtility.GetSpriteTexture(enemySprite, false);
			GUILayout.Label(atext);
		}

		if (nameFlag) 
		{
			EditorGUILayout.HelpBox ("Name can not be blank", MessageType.Error);
		}
		if (currentChoice == 0) {
			if (GUILayout.Button ("Create")) {
				creatEnemy ();
			}
		} 
		else 
		{
			if (GUILayout.Button ("Save")) 
			{
				alterEnemy ();
			}
		}
		if (currentChoice != lastChoice) 
		{
			if (currentChoice == 0) 
			{
				//blank out feilds for new enemy
				nameString = "";
				health = 1;
				attack = 1;
				defense = 1;
				agility = 1;
			}
			else 
			{
				//load fields with enemy data
				nameString = enemyList[currentChoice - 1].emname;
				health = enemyList [currentChoice - 1].health;
				attack = enemyList [currentChoice - 1].atk;
				defense = enemyList [currentChoice - 1].def;
				agility = enemyList [currentChoice - 1].agi;
				attackTime = enemyList [currentChoice - 1].atkTime;

				magicUser = enemyList [currentChoice - 1].isMagic;
				mana = enemyList [currentChoice - 1].manaPool;
				enemySprite = enemyList [currentChoice - 1].mySprite;
			}
			lastChoice = currentChoice;
		}
	}

	private void GetEnemies()
	{
		enemyList.Clear ();
		enemyNameList.Clear ();
		string[] guids = AssetDatabase.FindAssets ("t:Enemies");
		foreach (string guid in guids) 
		{
			string myString = AssetDatabase.GUIDToAssetPath (guid);

			Enemies enemyInst = AssetDatabase.LoadAssetAtPath (myString, typeof(Enemies)) as Enemies;

			enemyList.Add (enemyInst);
		}

		foreach (Enemies e in enemyList) 
		{
			enemyNameList.Add (e.emname);
		}
		enemyNameList.Insert(0, "New");
		enemyNames = enemyNameList.ToArray();
	}

	private void creatEnemy()
	{
		if (nameString == "") 
		{
			nameFlag = true;
		} 
		else 
		{
			Enemies myEnemy = ScriptableObject.CreateInstance<Enemies>();
			myEnemy.emname = nameString;
			AssetDatabase.CreateAsset (myEnemy, "Assets/Resources/Data/EnemyData/"+myEnemy.emname.Replace(" ", "_")+".asset");
			nameFlag = false;
			GetEnemies ();
		}
	}

	private void alterEnemy()
	{
		enemyList [0].health = health;
		enemyList [0].atk = attack;
		enemyList [0].def = defense;
		enemyList [0].agi = agility;
		enemyList [0].atkTime = attackTime;

		enemyList [0].isMagic = magicUser;
		enemyList [0].manaPool = mana;

		enemyList [0].mySprite = enemySprite;
	}
	
}
