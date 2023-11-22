using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Componente que hace de intermediario entre la escena y el IsosSceneManager (que es persistente). </summary>

[AddComponentMenu("Isostopy/Scene Package/Scene Loader")]
public class SceneLoader : MonoBehaviour
{
	IsosSceneManager manager = null;

	/// <summary> Si tiene o no que cargar la siguiente escena automaticamente en el Start(). </summary>
	[Space][SerializeField] bool loadOnStart = true;

	/// Para cargar la escena automaticamente ¿Usamos un nombre o una referencia?
	[SerializeField] bool useSceneReference = false;
	/// Referencia a la escena asignada en el editor.
	[SerializeField] SceneReference targetSceneReference = null;
	/// El nombre de la escena asignado en el editor.
	[SerializeField] string targetSceneName = "";
	/// Nombre de la escena que hay que cargar automaticamente en base a lo indicado en el editor.
	private string targetScene
	{
		get
		{
			if (useSceneReference)
				return targetSceneReference.sceneName;
			else
				return targetSceneName;
		}
	}

	/// <summary> El tiempo que espera antes de cargar la escena automaticamente. </summary>
	[Min(0)][SerializeField] float delay = 0;
	/// <summary> Si tiene o no que hacer fade antes de cargar otra escena. </summary>
	[SerializeField] bool useFade = true;
	/// <summary> Si tiene o no que pasar por la pantalla de carga antes de cargar la escena automaticamente. </summary>
	[SerializeField] bool useLoadingScreen = false;


	// ---------------------------------------------
	#region Start

	private void Start()
	{
		// Busca el manager.
		manager = FindObjectOfType<IsosSceneManager>();

		// Si se ha indicado, cargar la escena automaticamente.
		if (loadOnStart)
			StartCoroutine(LoadWithDelay());
	}

	/// <summary> Corrutina que carga la escena indicada en el inspector. </summary>
	IEnumerator LoadWithDelay()
	{
		// Esperar el tiempo indicado.
		if (delay > 0)
			yield return new WaitForSecondsRealtime(delay);
		else
			yield return new WaitForEndOfFrame();

		// Cargar con las opciones indicadas.
		if (useFade)
		{
			if (useLoadingScreen)
				FadeToSceneAsync(targetScene);
			else
				FadeToScene(targetScene);
		}
		else
		{
			if (useLoadingScreen)
				LoadSceneAsync(targetScene);
			else
				LoadScene(targetScene);
		}
	}

	#endregion


	// ---------------------------------------------
	#region Cargar escena

	/// <summary>
	/// Llama al IsosSceneManager para que cargue la escena indicada directamente. </summary>
	public void LoadScene(string scene)
	{
		if (manager != null)
				manager.LoadScene(scene, false);
		else
			LoadSceneWithoutManager(scene);
	}

	public void LoadScene(SceneReference sceneReference) => LoadScene(sceneReference.sceneName);


	/// <summary>
	/// Llama al IsosSceneManager para que cargue la escena indicada pasando por la pantalla de carga. </summary>
	public void LoadSceneAsync(string scene)
	{
		if (manager != null)
			manager.LoadScene(scene, true);
		else
			LoadSceneWithoutManager(scene);
	}
	
	public void LoadSceneAsync(SceneReference sceneReference) => LoadSceneAsync(sceneReference.sceneName);


	/// <summary>
	/// Llama al IsosSceneManager para que cargue la escena indicada con un fade. </summary>
	public void FadeToScene(string scene)
	{
		if (manager != null)
			manager.FadeToScene(scene);
		else
			LoadSceneWithoutManager(scene);
	}

	public void FadeToScene(SceneReference sceneReference) => FadeToScene(sceneReference.sceneName);


	/// <summary>
	/// Llama al IsosSceneManager para que cargue la escena indicada con un fade y la pantalla de carga. </summary>
	public void FadeToSceneAsync(string scene)
	{
		if (manager != null)
			manager.FadeToScene(scene, true);
		else
			LoadSceneWithoutManager(scene);
	}

	public void FadeToSceneAsync(SceneReference sceneReference) => FadeToSceneAsync(sceneReference.sceneName);


	/// <summary>
	/// Carga la escena objetivo sin mas, sin manager, ni fade, ni nada. </summary>
	void LoadSceneWithoutManager(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	#endregion
}
