using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Sprites;

public class MyCustomEditor : EditorWindow {

    int myInt;
    bool myToggle;
    bool myFold;

    float minFloat = 1f;
    float maxFloat = 100f;
    float selectedMin = 1f;
    float selectedMax = 10f;

    string[] myString = { "new", "hello", "third"};
    int myChoice = 0;

    Sprite mySprite;

    [MenuItem("My Tool/ My window %z")]
	private static void showMyWindow()
    {
        EditorWindow.GetWindow<MyCustomEditor>(true, "My Custom Tool");
    }

    private void OnGUI()
    {
        myChoice = EditorGUILayout.Popup(myChoice, myString);
        if (myChoice == 0)
        {

            myToggle = EditorGUILayout.BeginToggleGroup("My Toggel", myToggle);
            EditorGUILayout.HelpBox("This is a message.", MessageType.Info);

            myInt = EditorGUILayout.IntField("My Int", myInt);
            if (myInt < 0)
            {
                myInt = 0;
            }

            EditorGUILayout.LabelField(myInt.ToString());
            EditorGUILayout.EndToggleGroup();
            myInt = EditorGUILayout.DelayedIntField("Delay Int", myInt);

            EditorGUILayout.Space();

            myFold = EditorGUILayout.Foldout(myFold, "Fold out");
            if (myFold)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Min Value: " + minFloat.ToString(), GUILayout.MaxWidth(80.0f));
                EditorGUILayout.LabelField("Max Value: " + minFloat.ToString(), GUILayout.MaxWidth(80.0f));
                EditorGUILayout.LabelField("Selected Min" + selectedMin.ToString("F2"), GUILayout.MaxWidth(125.0f));
                EditorGUILayout.LabelField("Selected Max" + selectedMax.ToString("F2"), GUILayout.MaxWidth(125.0f));
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.LabelField("My Slider");
            EditorGUILayout.MinMaxSlider(ref selectedMin, ref selectedMax, minFloat, maxFloat);

        }
        if(myChoice == 1)
        {
            mySprite = EditorGUILayout.ObjectField(mySprite, typeof(Sprite), true)as Sprite;
            if(mySprite != null)
            {
                Texture2D atext = SpriteUtility.GetSpriteTexture(mySprite, false);
                GUILayout.Label(atext);
            }
        }

    }
}
