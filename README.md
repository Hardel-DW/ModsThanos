## Cliquer ici pour le readme en français :
<p align="center">
  <a href="https://github.com/Hardel-DW/ModsThanos/blob/main/README.fr.md">
    <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/c/c3/Flag_of_France.svg/langfr-338px-Flag_of_France.svg.png" />
  </a>
</p>

[![Hardel](https://discord.com/assets/e4923594e694a21542a489471ecffa50.svg)](https://discord.gg/AP9axbXXNC)


# Thanos Mod

The Thanos Mod, it's among Us mod, adding a role and new features.  
A random player, will be Thanos and will have to search for the 6 stones of infinity to unlock the finger snap, and finish the game.  
16 options are configurable in the lobby.

# Releases :
| Among Us - Version| Mod Version | Link |
|----------|-------------|-----------------|
| 2020.12.19s | V1.2 | [Download](https://github.com/Hardel-DW/ModsThanos/releases/download/V1.2.0/Among.Us.-.Thanos.zip) |
| 2020.12.19s | V1.1 | [Download](https://github.com/Hardel-DW/ModsThanos/releases/download/V1.1/Among.Us.-.Mods.Thanos.zip) |
| 2020.12.19s | V1.0c | [Download](https://github.com/Hardel-DW/ModsThanos/releases/download/V1.0c/Among.Us.-.Mods.Prod.zip) |
| 2020.12.19s | V0.1 | [Download](https://github.com/Hardel-DW/ModsThanos/releases/download/B%C3%AAta/Among.Us.-.Mods.Thanos.zip) |
<details><summary>Changelog</summary>
<p>

  # Version 1.2:  
  ## Features:
  * The tasks on the left menu for impostors are now visible.
  * The "Games Options" are now visible to everyone.
  * A new "Game Options", made are appearance allowing to deactivate the mod.
  * The right and left arrows for the visibility "games options" are no longer limiting.
  * The buttons are no longer usable during the voting phase.
  * After the voting phase, the buttons are set to their maximum pause times.
  * Impostors in the winds are not impacted by the winding effect.
  * Addition of the discord server in the lobby, and not in part.
  * Among us's LeaverBuster is deleted.
  * A new spawn location for stones on Polus has spawned.

  ## Bug Fixes:
  * On the map of Polus, stones could appear outside the limits of the game map.
  * When going back in time, if the impostor was in a wind, he could move while being invisible.
  * Sometimes the visibility "Game Options" does not work.
  * The "Cooldown" of the first use of stones was not that of "Game Options".

  ## Technical change :
  * Reactor is now used as a dependency for inter-modal compatibility.
  * Essentials-Reactor is now used as a dependency for inter-mod compatibility.
  * 80% of the files have been modified for a better development comfort and performance optimization.
  * Among Us pointer references are now almost all removed.
  * Packet sending for "role management" is no longer sent in several packets but in one.
</p>
</details>

# Installation

Download the zip file on the right side of Github.  
1. Find the folder of your game, for steams players you can right click in steam, on the game, a menu will appear proposing you to go to the folders.  
2. Make a copy of your game (not required but recommended)  
3. Drag or extract the files from the zip into your game, at the .exe level.  
4. Turn on the game.  
5. Play the game.  

![Install](https://i.imgur.com/pvBAyZN.png)

## Serveur

The Thanos Mod, being a mod that modifies a lot the behavior of the game, the official servers will not support the new features.  
That's why a private server is necessary.  
By default Thanos Mod, offers you the possibility to connect to the Cheeps-YT.com servers.  

![Server](https://i.imgur.com/opzh2BQ.png)

--------

## Thanos role

At the start of a game, a randomly chosen person will become Thanos, he will then have a purple screen,  
and the name to display in purple, only Thanos can see his name in color.  
In addition 6 stones are hidden through the map, Thanos will have to search and recover them without revealing his identity.  
Each stone offers a unique power, find all 6 and the mythical finger snap will be unlocked.  
The stones are synchronized between all the players.
![Thanos](https://i.imgur.com/1x5DshJ.png)

# Infinity Stone :
## Soul Stone  
It is an orange stone, the Crewmates have the particularity to see an arrow on their interfaces indicating the position of the stone.  
The main specificity of this stone is that the Crewmates can pick it up.  
When a "Crewmate" collects this stone, their name becomes orange and is visible to all.  
By killing the owner of the soul stone, this stone will then be randomly placed back on the map.  

## Stone of time:  
This is a green stone, which simply allows its activation to go back in time.  
All the movements and animation of the walk are reproduced exactly.  
In addition, if a Crewmate are dead, and the power is activated, This said Crewmate is then resurrected.  
(In case the Crewmate possesses the orange stone, it will lose it, and its name will become white again).  

## Reality Stone:  
It is a red stone, which makes it possible to be invisible during a period.  
Thanos will see an effect of transparency, and the other players won't see Thanos anymore.  

## Space Stone:  
It is a blue stone, which allows the action of this one, to pose a dimensional portai.  
When several portals are placed, Thanos can imprint them like vents, and travel from portal to portal.  

## Mind Stone:  
It is a yellow stone, which allows its activation to take the appearance of someone, pseudo, costume, hat, colors, and pet.  
It has a temporary duration.  
Once the duration is over, the player is transformed back into himself.  

## Stone of Power:  
It is a pink/purple stone, which allows its activation to kill all players in the proximity of Thanos.  

## Finger snap:  
When Thanos has the 6 stones, the finger snap is automatically unlocked when used, an animation will start and after a delay all players will die.  

![HUD](https://i.imgur.com/ivxlot9.png)
--------

## Games Options
| Game Option | Description | Type |
|----------|-------------|-----------------|
| Cooldown Time Stone | Modifies the reloading time of the time stone | Number |
| Cooldown Reality Stone | Modifies the reloading time of the reality stone | Number |
| Cooldown Space Stone | Modifies the reloading time of the space stone | Number |
| Cooldown Mind Stone | Modifies the reloading time of the mind stone | Number |
| Cooldown Soul Stone | Modifies the reloading time of the soul stone | Number |
| Cooldown Power Stone | Modifies the reloading time of the power stone | Number |
| Time Stone Visibility | Modifies the person(s) who can see the stone | Everyone/Thanos/Crewmate |
| Reality Stone Visibility | Modifies the person(s) who can see the stone | Everyone/Thanos/Crewmate |
| Power Stone Visibility | Modifies the person(s) who can see the stone | Everyone/Thanos/Crewmate |
| Soul Stone Visibility | Modifies the person(s) who can see the stone | Everyone/Thanos/Crewmate |
| Mind Stone Visibility | Modifies the person(s) who can see the stone | Everyone/Thanos/Crewmate |
| Space Stone Visibility | Modifies the person(s) who can see the stone | Everyone/Thanos/Crewmate |
| Time Duration | Changes the duration of the associated effect | Number |
| Reality Duration | Changes the duration of the associated effect | Number |
| Mind Duration | Changes the duration of the associated effect | Number |
| Max Portal | Modifies the maximum number of gates installed (Non-functional) | Number |
| Stone Size | Changes the size of the stone on the map (the higher the value, the smaller it will be, and vice versa). | Number |
| Thanos Enable | Enable thanos mod | True/False |

# Author and thanks
Creation of the mod by Hardel by request of FuzeIII.
Thanks, to the Reactor server, for all the help.
Thank you, Cheeps for giving permission to use his private server.

# Bugs/Feature suggestions
If you need to contact me, to request additional features, or to make bug or change requests.  
Go to this [Discord](https://discord.gg/AP9axbXXNC) server, or create a ticker on Github.

# Ressources
https://github.com/NuclearPowered/Reactor Le Framework que le mod utilise.  
https://github.com/BepInEx For loading the mods.    
https://github.com/DorCoMaNdO/Reactor-Essentials To create inter-mods game options.    
https://github.com/Woodi-dev/Among-Us-Sheriff-Mod For code snippets.  

# License
This software is distributed under the GNU GPLv3 License. BepinEx is distributed under LGPL-2.1 License.
