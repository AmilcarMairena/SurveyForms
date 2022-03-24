# SurveyForms
Sistema para generar formulario de encuesta!

Proyecto divido en dos partes:
Dotnet core
1. WebApi para la authenticacion mediante Jwt
2. Web App MVC 

- Indicaciones
1 - Dentro del appsettings.js cambiar el string de conexion del servidor local de sql server
      - ve a esta direccion en el proyecto #SurveyFormsWebApp/appsettigs.json/appsettins.Development.json
    - Toma el string de conexion presente abajo y modifica los parametros que estan entre bracker "Recuerda remover los brackets"
    "ConnectionStrings": {
    "SurveyFormDb": "Server=[nombre de tu servidor]; Database=surveyFormDb; Trusted_Connection=True;MultipleActiveResultSets=true; User ID=[tu usuario]; Password=[tu          pass]"
    },
2 - Configurar las propiedades de la solucion para que ambos proyectos se inicien.
3 - Iniciar el ambos proyectos
4 - Las migraciones de los modelos utilizados se realizaran automaticamente a tu base de datos.
5 - para el acceso a las funciones de encuesta y resultados puedes usar las siguientes credenciales: email: admin@admin.com pass: admin
