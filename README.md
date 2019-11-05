# Practice 01

We have to make an FPS Game and it need to have the following features:

## "Mandatory" features
* **Terrain:** Using the Terrain tool, we have to do a mountain zone with a little forest.
* **Scenario:** Using the store assets, on that Terrain must we make a playable scenario that adapt to the needs of the practice.
* **First Person Controller:** Will allow the player to move through the Terrain and look around himself.
* **Weapon / Shoot / Ammo / Shield:** The player will have a weapon to shoot with. Every x number of shots the player will have to reload. The player will have a Shield. For each impact received, the shield receives 75% of the damage, and the life  a 25%. When the shield is empty, each impact received do the 100% of damage to life.
* **HUD (life / ammo / shield):** The life values, loader ammo, remaining ammo, and shield should be seen through the screen always with number values and an icon that represents them.
* **Shooting Gallery:** After the initial forest, the player must reach a shooting zone, where he will have to demonstrate his aim and reflexes. The objectives will appear in a fixed sequence for a limited time, where some will be fixed and others will be in motion. When a target is hit, it will be destroyed and the player will earn points for it. We must, then, add in the HUD, only in that place, the player's score. The player can repeat this game as many times as he wants.
* **Items (Health, Ammo, Shield):** Spread across the stage, the player will be able to collect 3 types of items that will fill his life, ammo or shield, as the case may be. If any of the indicators is at maximum, the item should not be able to be collected.
* **Enemies:** Once the shooting gallery passes, the player will enter a corridors zone patrolled by soldier drones. These drones will have different damage zones. We can only damage it if they are shot in the central eye or in the propellers.
* **IA:** The enemies must have the following states:
	* **IDLE:** Stopped in the air.
	* **PATROL:** Moving in a loop along a preset route.
	* **ALERT:** When the enemy ‘hears’ the player near him, he will be put on ‘alert’ and will start rotating on himself until he 'sees' the player. If it does not see the player after a full turn, the enemie will return to **PATROL**.
	* **CHASE:** 
	* **ATTACK:**
	* **HIT:**
	* **DIE:**