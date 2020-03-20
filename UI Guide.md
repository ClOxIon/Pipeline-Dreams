# Pipeline Dreams UI Guide
UI scripts are currently fragmentary. Read each scripts to understand their functions. The rule of thumb is to make each UI scripts independent from the others, and to make them not affect game scripts in any way except raising events.
Below are commonly used classes.
- IIndividualUI<T> where T is PDObject
Displays a single T.
- ISelectableIndividualUI<T> : IIndividualUI<T>
T can be selected through mouse/keyboard input. The selection could be used to obtain, throw away, examine, etc.
- ICollectionInfoUI
- CollectionInfoUI<T> : ICollectionInfoUI where T is PDObject 
Displays an information about a single T. Used with a CollectionUI consists of ISelectableIndividualUIs.
- CollectionUI<T> where T is PDObject
Displays a collection of IIndividualUIs.
- CollectionUILimitedCapacity<T> : ObjectContainerUI<T>
Only a certain number of IIndividualUIs can be in this collection UI.