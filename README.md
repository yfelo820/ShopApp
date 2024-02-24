Este Proyecto esta Pensado para Ejecutarse con Multiple Startup Projects en Visual Studio 

![image](https://github.com/yfelo820/ShopApp/assets/77872958/6e055554-fa07-4a40-9ff5-c79262939626)

En el Orden de Ejecución Priorizar el Proyecto CL ya que es el BackEnd(.Net6)
Igualmente debe agregarse al orden de ejecución el FrontEnd ClientApp (Blazor Web Assemble .Net 6)

![image](https://github.com/yfelo820/ShopApp/assets/77872958/43a0286b-c421-4ded-a354-bc909071974d)


Para que el BackEnd se ejecute correctamente lo primero que debemos hacer es dirijirnos al fichero "appsettings.json"

El mismo esta situado en la raiz del Proyecto que Ejecuta el BackEnd con nombre "CL" de Communication Layer

Una vez abrimos el fichero "appsettings.json" nos dirijimos a la sección "ConnectionStrings" solo hay una cadena definida "DefaultConnection" 

Debemos asegurarnos de introducir una cadena de conexión valida cuando la remplazemos 

Una vez hecho la anterior abrimos el PMC - Package Manager Console 

En el PMC seleccionamos como Default-Project el proyecto CL

Ejecutamos el comando "update-database" y el mismo creará las tablas e insertará datos de prueba

El proyecto esta configurado para exporse por "https://localhost:7177" 

LLegados a este punto podemos correr la aplicación del BackEnd "CL"

Si realizamos todos los pasos correctamente será levantado el SWAGGER donde podremos ver todos los ENDPOINT configurados en el Proyecto

Todos los EndPoints estan protegidos con JWT 

Para generar un JWT podemos acceder la URL "https://localhost:7177/api/JwtGenerator?user=admin&password=123"

La petición deberá hacerse POR GET y los query params user y password puede enviarse cualquier valor es solo una muestra

Con el JWT podremos probar el resto de los EndPoints

Si realizamos todo como en la guía sin cambiar ninguna configuración

Cuando corramos el Proyecto deben de levantarse 2 Navegadores una con el Swagger y otro con la appCliente

La app cliente consume todos los EndPoint que expone nuestra API

No obstante igualmente todos pueden ser consumidor por postman 

El objetivo de la app cliente es proveer una forma más simple de comprobar la API

Si se modifica la URL:PUERTO por donde se expondrá la API 

En el Proyecto Shared/Utils/Constans.cs

Debe modificarse la constante SERVICES_BASE_URL con el valor que modificamos

Este con el fin de que los servicios que se utilizan para consumir los EndPoint sean enrutados correctamente

