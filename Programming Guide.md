# Pipeline Dreams Programming Guide

# Namespace

- PipelineDreams
  - Entity
  - Item
    - Weapon
  - Instruction
  - Buff
  - Map
  - MutableValue

# Core
Pipeline Dreams' core provides extensive tools to build a roguelike game. Every class is under the namespace PipelineDreams.

- PDObject

The base class of every object that is held by some entity. Items, buffs, abilities are derived from this. To initialize a PDObject, a corresponding PDData is necessary.


- PDData

The base class of readonly data object. PDData is a ScriptableObject that could store data to initalize PDObjects.


- IPDDataSet

Interfaces a collection of PDDatas. 


- PDObjectContainer<T>

The base class of every object that contains multiple PDObjects of the same kind and hold by the same entity. ItemContainer, AbilityContainer, BuffContainer are derived from this. PDObjectContainer should be held by an entity that holds every PDObject in it.

- TaskManager

Manages the flow of the time. Time should be spent through this class. Performs IClockTasks accordingly. Anything happning in the game, including interactions between entities and moving, should be inserted into this class, wrapped by an IClockTask implementation.

- IClockTask

Interfaces every task performed throughout the game. Includes a coroutine to be ran by the TaskManager. Task Priorities should be set carefully; Running a task before another task unintentionally is a common source of bug.

# Entity
- Entity.Entity

The base class of player, monsters, and tile. Entity class only holds variables for the entity and throws general events. Every other functions of the entity is performed by modules under Assets/Scripts/Entity folder. Every entities in the scene is held by EntityDataContainer. Note that Entities, despite not being PDObject, still needs PDData at the point of initialization.


- Entity.Container

The container of entities. Could spawn and search entities in the scene through this class. This class is different from EntityDataset, which is an implementation of IPDDataSet and thus readonly.

# Map

- Map.MapFeatData

Contains the position of rooms and paths.


- Map.Generator

The base class of all ScriptableObjects that Generate MapFeatData.


- Map.Renderer

The base class of all ScriptableObjects that spawn tile entities according to a given MapFeatData.

# MutableValue
- MutableValue.FunctionChain

Is a wrapper class of a float value(could be get by FunctionChain.Value). Functions could be added to morph the final value. To do this, subscribe to the OnEvalRequest event of the FunctionChain. When OnEvalRequest is called, call FunctionChain.AddFunction() to add an IFunction. EvalAtNextGet is a trigger; it should be turned true every time a change is anticipated for any IFunction in the chain. When Value is requested, every added IFunction will be evaluated only if EvalAtNextGet is true; otherwise, the old Value is returned unchanged. IFunction adder is responsible for turning on the trigger.


- IFunction

Interfaces a float to float function. Priority determines the order of IFunctions within a FunctionChain.

# Instruction

- Instruction.Instruction : PDObject

Instruction is what corresponds to abilities in other games. Every entity that acts in some way has an InstructionContainerHolder module, which has an Instruction.Container that contains all instruction of the entity. The task, which implements what is actually done by the instruction, is returned by the function Operation(), 

# Item
- Item.Item : PDObject

Items are not used anymore in the game; they are instead implemented through instructions(either passive or single-use). They can still return; items are stored in ItemContainer.

- Item.Weapon.Weapon : Item

Entities that have a module WeaponHolder can hold a single weapon. The weapon determines the damage of the entity's attacks. Such weapons are initialized by InitialWeapon module.  

# Buff
- Buff.Buff : PDObject

Every entity that can be inflicted a buff has an BuffContainerHolder module, which has a Buff.Container that contains all buff currently inflicted to the entity. 

#ScriptableObjects

Under Assets/ScriptableObject, you could find various data tables.

EnData: the data table of all entities

OpData: the data table of all instructions

BuData: the data table of all buffs
