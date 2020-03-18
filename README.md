# Pipeline Dreams
Pipeline Dreams is a turn-based 3D roguelike game. The player is a robot which suddenly became self-aware in the middle of what remains of a technologically advanced civilization. You venture through the immense underground network which survived the apocalypse. 

# Core
Pipeline Dreams' core provides extensive tools to build a roguelike game. Every class is under the namespace PipelineDreams.

PDObject

The base class of every object that is held by some entity. Items, buffs, abilities are derived from this. To initialize a PDObject, a corresponding PDData is necessary.


PDData

The base class of readonly data object. PDData is a ScriptableObject that could store data to initalize PDObjects.


PDObjectContainer<T>

The base class of every object that contains multiple PDObjects of the same kind and hold by the same entity. ItemContainer, AbilityContainer, BuffContainer are derived from this. PDObjectContainer should be held by an entity that holds every PDObject in it.


Entity

The base class of player, monsters, and tile. Entity class only holds variables for the entity and throws general events. Every other functions of the entity is performed by modules under Assets/Scripts/Entity folder. Every entities in the scene is held by EntityDataContainer.


EntityDataContainer

The container of entities. Could spawn and search entities in the scene through this class.


TaskManager

Manages the flow of the time. Time should be spent through this class. Performs IClockTasks accordingly. Anything happning in the game, including interactions between entities and moving, should be inserted into this class, wrapped by an IClockTask implementation.


IClockTask

Interfaces every task performed throughout the game. Includes a coroutine to be ran by the TaskManager. Task Priorities should be set carefully; Running a task before another task unintentionally is a common source of bug.


MapFeatData

Contains the position of rooms and paths.


MapGenerator

The base class of all ScriptableObjects that Generate MapFeatData.


MapRenderer

The base class of all ScriptableObjects that spawn tile entities according to a given MapFeatData.


MutableValue.FunctionChain

Is a wrapper class of a float value(could be get by FunctionChain.Value). Functions could be added to morph the final value. To do this, subscribe to the OnEvalRequest event of the FunctionChain. When OnEvalRequest is called, call FunctionChain.AddFunction() to add an IFunction. EvalAtNextGet is a trigger; it should be turned true every time a change is anticipated for any IFunction in the chain. When Value is requested, every added IFunction will be evaluated only if EvalAtNextGet is true; otherwise, the old Value is returned unchanged. IFunction adder is responsible for turning on the trigger.


IFunction

Interfaces a float to float function. Priority determines the order of IFunctions within a FunctionChain.

