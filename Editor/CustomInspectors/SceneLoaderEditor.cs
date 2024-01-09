using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor que personaliza el aspecto en el inspector del componente <see cref="SceneLoader"/>. </summary>

[CustomEditor(typeof(SceneLoader))]
[CanEditMultipleObjects]
public class SceneLoaderEditor : Editor
{
	SerializedProperty loadOnStart;

	SerializedProperty useSceneReference;
	SerializedProperty targetSceneReference;
	SerializedProperty targetSceneName;

	SerializedProperty delay;
	SerializedProperty useFade;
	SerializedProperty useLoadingScreen;


	// -------------------------------------------------------------------

	public void OnEnable()
	{
		loadOnStart = serializedObject.FindProperty("loadOnStart");

		useSceneReference = serializedObject.FindProperty("useSceneReference");
		targetSceneReference = serializedObject.FindProperty("targetSceneReference");
		targetSceneName = serializedObject.FindProperty("targetSceneName");

		delay = serializedObject.FindProperty("delay");
		useLoadingScreen = serializedObject.FindProperty("useLoadingScreen");
		useFade = serializedObject.FindProperty("useFade");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		/// Si no se va a cargar en el Start, desactivar los campos que no se van a usar.
		EditorGUILayout.PropertyField(loadOnStart);
		if (!loadOnStart.boolValue)
			GUI.enabled = false;

		EditorGUILayout.Space();

		// Dibujar opciones para elegir si cargar la escena por nombre o por referencia.
		string[] options = { "Use Scene Name", "Use Scene Reference" };
		int selected = useSceneReference.boolValue ? 1 : 0;
		selected = EditorGUILayout.Popup(selected, options);
		useSceneReference.boolValue = selected == 0 ? false : true;

		// Dibujar uno u otro segun lo elegido.
		if (useSceneReference.boolValue)
			EditorGUILayout.PropertyField(targetSceneReference);
		else
			EditorGUILayout.PropertyField(targetSceneName);

		// Dibujar el resto de opciones del componente.
		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(delay);

		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(useFade);
		EditorGUILayout.PropertyField(useLoadingScreen);

		GUI.enabled = true;

		serializedObject.ApplyModifiedProperties();
	}
}
