## Solución
Se propone una solución basada en el desarrollo de un Cliente UWP de Reddit que permita la visualización de aquellos posts que han generado mayor repercusión entre los usuarios de la red social.

### Características
-   *Soporte de Windows*
      >   **Versión de Destino**: Windows 10 [v1903] (10.0; Build 18362)
      >   **Versión Mínima**:     Windows 10 [v1809] (10.0; Build 17763)

### Tecnologias
-   UWP
-   C#

### Funcionalidades
-   Autenticación frente a Reddit API.
-   Consumo de Reddit API.
-   Visualización global de posts principales.
-   Visualización detallada de post principal.

### Datos de Interés
-   Uso de patrón Master/Detail con visibilidad "Side-by-Side".
-   Animaciones sobre Interfaz de Usuario.
-   Adaptabilidad de títulos de acuerdo a longitud.
-   Consumo de API mediante HttpClient Class.
-   Variables de Entorno como medio de almacenamiento y extracción de credenciales Reddit.

### Configuración de Variables de Entorno
-   **REDDIT_USER**: *Corresponde con el nombre de nuestro usuario Reddit.*
-   **REDDIT_PASSWORD**: *Contraseña de nuestro usuario Reddit.*
-   **REDDIT_CLIENT_ID**: *Combinación alfanumérica que Reddit asigna a nuestra aplicación como identificador.*
-   **REDDIT_CLIENT_SECRET**: *Combinación alfanumérica que Reddit asigna a nuestra aplicación como clave secreta.*

### Recursos
-   **Reddit API** (http://www.reddit.com/dev/api)
-   **Windows Documentation** (https://docs.microsoft.com/en-us/windows/uwp/)