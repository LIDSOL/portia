# Animar un NPC

## Pasos

- Importar modelo con animaciones y esqueleto 
- Crear Blend Spaces
- Crear Animation Blueprint
	- En AnimGraph
		- Creamos nuevo estado
		- Asignamos un punto de entrada a una animacion de inicio (Idle)
		- Arrastramos un Blend Spaces
			- Asignamos varibales correspondientes
		- Conectamos Idle con el BS introducido
		- Asignamos logica a la transicion
	- Los pasos anteriores se puden combinar entre BS y Animaciones
- Creamos Blueprint del personaje
- Creamos el Blackboard
- Creamos el controlador de AI
- Arrastramos el BP del caracter
-  

## Recursos

- [Setting Up Character Movement in Blueprints](https://docs.unrealengine.com/latest/INT/Gameplay/HowTo/CharacterMovement/Blueprints/index.html)