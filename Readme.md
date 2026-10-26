\# Guía de Trabajo en Equipo y Flujo de Git



Este documento detalla las normas de colaboración y los comandos necesarios para trabajar en el proyecto de forma ordenada.



\---



\# 1. Clonar el Repositorio



Si es tu primera vez trabajando en el proyecto, clona el repositorio en tu equipo:

git clone https://github.com/Ivo-NM/FarmaciaRepsitory.git





\#2 Revisa que estes en la rama principal



git checkout main   ---> Este comando te cambia a la rama principal



\#3 crea una nueva rama para tu trabajo, asi no usas la rama principal



git checkout -b nombre-de-tu-tarea



git pull origin main  --->  Este comando actualiza tu rama





\#4 Cuando hagas cambios y quieras guardar tu progreso guarda todo en tu rama para

no exponer la rama principal



git status  ---> Te ayuda a verificar si estas en tu rama o si hay commits anteriores



git add .



git commit -m "Descripción clara de lo que agregaste o modificaste"



\#5 Haz el push directamente a tu rama



git push origin nombre-de-tu-tarea



\----------------------



Integración a main (Pull Request)



Ingresa a la página del repositorio en GitHub: https://github.com/Ivo-NM/FarmaciaRepsitory.git



Verás una notificación sugiriendo crear un Pull Request (PR) para la rama que acabas de subir.



Haz clic en Compare \& pull request.



Agrega un título descriptivo y explica brevemente qué cambios se implementaron.



Haz clic en Create pull request.



\# Importante!!!



No hagas Apretes en Merge hasta que no estes seguro de que los cambios son correctos, Esto guarda tus cambios
a la rama principal "main"



Una vez revisado y aprobado por el equipo, apreta en Merge para integrar el código a main.



\----------------------



Buenas Prácticas



Consistencia: Mantén siempre tu trabajo dentro de tu rama correspondiente.



Sincronización: Ejecuta git pull origin main en tu rama principal periódicamente para estar al día con lo que suban tus compañeros.



Mensajes claros: Escribe mensajes de commit concisos que expliquen lo que hiciste (por ejemplo: Agregar interfaz de login en lugar de "cambios")

