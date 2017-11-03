## README

### Changelog

#### 03.11.17
Divided the old *PlayerController* class into **PlayerController**, **PlayerInventory**, **PlayerHealth**, and **ActionController**.

#### PlayerController.cs
Class containing player-movement related stuff. 

#### PlayerInventory.cs
Class containing all inventory related stuff. Trigger events with ingame-pickups are handled here.

#### PlayerHealth.cs
Class containing all health-related stuff.

#### ActionController.cs
Class containing all player-activated behaviour (except Jump() which is in Controller). 



