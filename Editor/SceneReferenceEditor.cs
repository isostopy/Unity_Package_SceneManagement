using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// Dibuja un inspector personalizado para el scriptable object <see cref="SceneReference"/>. </summary>

[CustomEditor(typeof(SceneReference))]
public class SceneReferenceEditor : Editor
{
	/// Referencia al scriptable object que estamos inspeccionando.
	SceneReference inspectedObject;
	/// Path donde esta la escena que referncia el scriptable object.
	string scenePath = "";


	// ---------------------------------------------------------------------------

	private void OnEnable()
	{
		inspectedObject = target as SceneReference;

		FindScenePath();
	}

	/// Busca y guarda el path de la escena elegida en el scriptable object.
	private void FindScenePath()
	{
		if (inspectedObject == null)
			return;

		string sceneName = inspectedObject.sceneName;
		string[] searchingPaths = { "Assets" };
		string[] guids = AssetDatabase.FindAssets("t:Scene", searchingPaths);

		foreach (string guid in guids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			string name = Path.GetFileNameWithoutExtension(path);

			if (name == sceneName)
			{
				scenePath = path;
				return;
			}
		}

		scenePath = "";
	}


	// ---------------------------------------------------------------------------

	public override void OnInspectorGUI()
	{
		// Dibujar el inspector por defecto del objeto.
		string oldName = inspectedObject.sceneName;
		base.OnInspectorGUI();
		string newName = inspectedObject.sceneName;

		// Si ha cambiado el nombre de la escena, actualizar el path de la escena.
		if (oldName != newName)
			FindScenePath();

		EditorGUILayout.Space();

		// Dibujamos un botón que carga la escena.
		if (GUILayout.Button("Load Scene"))
		{
			EditorSceneLoader.LoadScene(scenePath);
		}
	}
}
