# Scene Management
Paquete sencillo para gestionar el cambio de escenas en cualquier proyecto. Incluye fade entre escenas y pantallas de carga. 

# Setup
Normalmente añadimos una escena de inicialización que se ejecuta antes de cualquier otra y por la que no se vuelve a pasar nunca. Esa escena contiene solo los objetos y componentes que hace falta iniciar antes que los demás.

En esa escena añadimos un gameobject con el componente ``IsosSceneManager``. Este objeto no se destruye al cambiar de escenas y tiene campos en el inspector para configurar la pantalla de carga.

Para poder cargar escenas, estas tienen que estar añadidas en las build settings.

# Fade entre escenas
Para hacer fade entre escenas hay que usar un componente ``SceneFader``, que modifica la transparencia de una imagen de la UI. Hay dos maneras de usarlo:
- Añadimos, como hijo del ``IsosSceneManager`` en la escena de inicialización, un Canvas con una imagen que ocupa toda la pantalla y que tiene el componente ``SceneFader``. Al ser hijo del scene manager, tampoco se destruye al cambiar de escena. Cada vez que haya que hacer un fade, se usará ese fader.
- Cada escena tiene su propio componente ``SceneFader``. El manager lo encontrará automáticamente si no tiene ya uno. Útil, por ejemplo, para hacer fade en VR colocando una imagen delante de la cámara, ya que el objeto del player puede no estar en la escena inicial, si no que haya una instancia en cada escena.

# Cambiar de escena con Scene References
Una ``ScenesReference`` es un scriptable object que guarda el nombre de una escena y permite cargarla desde cualquier parte. Usando scene references podemos cambiar una escena de nombre y solo tener que actualizarlo en el propio scene reference; y no en todos y cada uno de los posibles sitios en los que se referencia esa escena a lo largo del proyecto. Porque allí donde fuéramos a cargar una escena por nombre, ahora lo cargamos usando el scene reference.

El ``SceneReference`` tiene **funciones públicas** para cargar la escena que está referenciando: directamente, usando un fade y/o usando una pantalla de carga. Estas funciones se pueden llamar incluso desde los eventos expuestos en el inspector de, por ejemplo, botones de la UI.

**Para crear uno** haz clic derecho en el proyecto -> Create -> Isostopy -> Scene Management -> Scene Reference.

# Pantalla de carga
Una pantalla de carga es una escena normal que hemos indicado en el ``IsosSceneManager`` que va a ejercer esa función. Cuando se cambia de escena usando la pantalla de carga, se pone primero esta y se va preparando la nueva de forma asíncrona. Cuando termina de cargarse, se pasa automáticamente a la escena objetivo. Esta pantalla de carga puede contener lo que queramos: textos, imágenes, animaciones, modelos... lo que sea con lo que queramos decorarla.

El paquete incluye un componente ``LoadingProgressBar``, que modifica el fill amount de una imagen de la UI para reflejar el progreso de carga de la escena objetivo. Añádelo a una escena de carga y el manager lo encontrará automáticamente y lo hará funcionar. El componente de imagen que utilices tiene que tener el ``Image Type`` en ``Filled``.

# Scene List Window
Puedes abrir una ventana con la lista de todas las escenas que hay en el proyecto. Permitiéndote localizarlas y moverte entre ellas de forma agil y comoda.

Para abrirla, ve a los menús de arriba a la izquierda: Isostopy -> Scene Loader.
