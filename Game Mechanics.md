# Pipeline Dreams Game Mechanics

# Stage System
A stage is an interconnected system of room and pipes. Different Stages are connected through stations. Multiple stages can be connected to a stage; it is player's choice to select which stage to go next. Stages differ by their rooms, topology, enemies, and possibly more(such as liquids and gravity, that are currently not implemented). Some stages have bosses. Each run, the player's goal is to beat the farthest stage the player has reached. This unlocks more stages.

# Soul system
When the player kills enemies, he gets souls(the in-game name of this resource is undetermined yet). Harder enemies drops more souls. Souls could be spent in certain places for Max HP, Energy, new instruction, etc. The player should keep seeking such places, which are quite rare. The chance of such places being generated depends on the stage; certain stages spawn certain features more.

# Energy system
Whenever the player performs an action, energy is spent. This includes moving, rotating, and attacking. The player dies when energy is zero, so he should find a shelter before that. A shelter is the only reliable source of energy; it refills energy to the maximum point. However, shelters could be only used once. The player should keep finding a new shelter. 

# Spawn system
Spawning tiles are generated at the point of mapgen. They spawn enemies constantly. The spawn rate increases as player kills more enemies in the region. The chance of spawning harder enemies increases too. This prevents the player from farming enemies.