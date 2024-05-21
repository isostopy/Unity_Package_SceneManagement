using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Componente que anima un elemento de la UI cambiando su trasparencia para hacer fade in y fade out. </summary>
[AddComponentMenu("Isostopy/Scene Package/Scene Fader")]
public class SceneFader : MonoBehaviour
{
	/// <summary> Imagen de la que vamos a cambiar el alpha para hacer fade. </summary>
	[SerializeField] Image image = null;
	/// <summary>  Tiempo que dura el fade. </summary>
	[SerializeField][Min(0)] float fadingTime = 0;

	/// <summary> Si estamos o no haciendo fade ahora mismo. </summary>
	bool fading = false;
	/// <summary> Referencia a la corrutina que esta haciendo fade. </summary>	
	Coroutine fadingRoutine = null;


	// ---------------------------------------------

	private void Reset() => image = GetComponent<Image>();


	// ---------------------------------------------
	#region Fade

	/// <summary> Vuelve la imagen opaca (true), o transparente (false) con una animación. </summary>
	public void Fade(bool state)
	{
		// Si el GameObject esta desactivado, poner directamente en el estado final.
		if (gameObject.activeInHierarchy == false)		/// Las corrutinas no pueden activarse con el GameObject inactivo.
		{
			FadeInmidiatly(state);
			return;
		}

		SetActive(true);

		if (fadingRoutine != null) StopCoroutine(fadingRoutine);
		fadingRoutine = StartCoroutine(FadingRoutine(state));		
	}

	IEnumerator FadingRoutine(bool state)
	{
		float start	= state == true ? 0 : 1;    /* Empieza transparente y termina opaco		*/
		float end	= state == true ? 1 : 0;    /* o empieza opaco y termina transparente.	*/

		// El contador empieza en el punto que sea que empieza el alpha.
		// Para que si el alpha ya esta a medio camino, el fade dure la mitad del tiempo.
		//float timeCounter = Mathf.Lerp( start * fadingTime, end * fadingTime, image.color.a );
		float timeCounter = 0;

		fading = true;		
		// Ir cambiando la transparencia de la imagen segun avance el contador de tiempo.
		while (timeCounter < fadingTime)
		{
			float progress = timeCounter / fadingTime;
			float targetAlpha = Mathf.Lerp(start, end, progress);
			SetAlpha(targetAlpha);

			timeCounter += Time.deltaTime;
			yield return null;
		}

		SetAlpha(end);
		SetActive(state);
		fading = false;
	}

	/// <summary> Vuelve la imagen opaca (true) o transparente (false) directamente, sin animacion. </summary>
	public void FadeInmidiatly(bool state)
	{
		float targetAplha = state ? 1 : 0;
		SetAlpha(targetAplha);
		SetActive(state);
	}

	/// <summary> Activa o desactiva la imagen que hace fade. </summary>
	void SetActive(bool value) => image.enabled = value;

	/// <summary> Cambia el alpha de la imagen que hace fade. </summary>
	void SetAlpha(float alpha)
	{
		Color color = image.color;
		color.a = alpha;
		image.color = color;
	}

	#endregion


	// ---------------------------------------------
	#region Properties

	/// <summary> Duración en segundos del fade. </summary>
	public float FadingTime
	{
		get { return fadingTime; }
		set
		{
			if (value < 0) value = 0;
			fadingTime = value;
		}
	}

	/// <summary> Si se esta o no haciendo fade ahora mismo. </summary>
	public bool Fading => fading;

	#endregion
}
