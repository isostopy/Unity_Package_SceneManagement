using UnityEngine;

/// <summary>
/// Scriptable Object que contiene el nombre de una escena. </summary>

[CreateAssetMenu(menuName = "Isostopy/Scenes Package/Scene Reference", fileName = "new SceneReference")]
public class SceneReference : ScriptableObject
{
	[Space]
	public string sceneName = "sceneName";


	// -----------------------------------------------------------------------------
	#region Cargar Escena

	/// <summary> Llama al IsosSceneManager para que cargue esta escena. </summary>
	public void LoadScene()
	{
		var manager = FindObjectOfType<IsosSceneManager>();

		if (manager != null)
			manager.LoadScene(sceneName, false);
		else
			LoadSceneWithoutManager();		
	}

	/// <summary> Llama al IsosSceneManager para que cargue esta escena pasando por la panalla de carga. </summary>
	public void LoadSceneAsync()
	{
		var manager = FindObjectOfType<IsosSceneManager>();

		if (manager != null)
			manager.LoadScene(sceneName, true);
		else
			LoadSceneWithoutManager();
	}

	/// <summary> Llama al IsosSceneManager para que cargue esta escena usando un fade. </summary>
	public void FadeToScene()
	{
		var manager = FindObjectOfType<IsosSceneManager>();

		if (manager != null)
			manager.FadeToScene(sceneName, false);
		else
			LoadSceneWithoutManager();
	}

	/// <summary> Llama al IsosSceneManager para que cargue esta escena usando un fade y la pantalla de carga. </summary>
	public void FadeToSceneAsync()
	{
		var manager = FindObjectOfType<IsosSceneManager>();

		if (manager != null)
			manager.FadeToScene(sceneName, true);
		else
			LoadSceneWithoutManager();
	}

	/// Carga esta escena si utilizar el IsosSceneManager.
	void LoadSceneWithoutManager()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
	}

	#endregion
}

