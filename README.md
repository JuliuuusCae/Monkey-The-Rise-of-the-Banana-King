# The Rise of Banana King

A 2D action RPG platformer built in Unity where players control a monkey warrior navigating through dangerous lands filled with enemies and bosses.

---

## Gameplay Overview

Players fight their way through multiple levels, battling diverse enemies, collecting loot, upgrading skills, and equipping gear — all while working toward confronting the final boss, the Minotaur.

### Core Features

- **2D Platformer Combat** — Melee attacks, aerial attacks, wall jumps, and counter-attacks
- **Skill System** — Three upgradeable active skills with branching upgrade paths
- **Equipment & Inventory** — Loot items, equip gear, and manage consumables
- **Merchant** — Buy and sell items between levels
- **Skill Tree** — Spend skill points to unlock and upgrade abilities
- **Save System** — Persistent progress saved to disk with optional encryption

---

## Levels

| Scene | Description |
|---|---|
| `MainMenu` | Title screen |
| `Level_0` | Tutorial / introduction area |
| `Level_1` | First combat zone |
| `Level_2` | Mid-game area |
| `Level_3` | Final area leading to boss |

---

## Enemies

| Enemy | Behavior |
|---|---|
| Boar | Charges the player |
| Snail | Can hide in shell to block damage |
| Small Bee | Flying enemy, ranged attack |
| Skeleton | Melee fighter with react state |
| Mushroom | Melee fighter with stun state |
| **Minotaur** | **Boss** — Final encounter |

All enemies use a finite state machine with states: Idle → Move → Battle → Attack → Stunned → Dead.

---

## Skills

### Dash
An evasive dash with upgrade paths that enhance it with clones or projectile shards.

| Upgrade | Effect |
|---|---|
| `Dash_CloneOnStart` | Creates a Time Echo at dash origin |
| `Dash_CloneOnStartAndArrival` | Creates echoes at both origin and destination |
| `Dash_ShardOnStart` | Fires a shard at dash origin |
| `Dash_ShardOnStartAndArrival` | Fires shards at both origin and destination |

### Time Echo
Summons a temporal clone that fights alongside the player.

| Upgrade | Effect |
|---|---|
| `TimeEcho_SingleAttack` | Clone performs one attack |
| `TimeEcho_MultiAttack` | Clone performs multiple attacks |
| `TimeEcho_ChanceToDuplicate` | Chance to create a second clone |
| `TimeEcho_HealWisp` | Clone becomes a wisp that heals on hit |
| `TimeEcho_CleanseWisp` | Wisp removes negative status effects |
| `TimeEcho_CooldownWisp` | Wisp reduces skill cooldowns |

### Time Shard
Launches a projectile at enemies.

| Upgrade | Effect |
|---|---|
| `Shard_MoveToEnemy` | Shard tracks the nearest enemy |
| `Shard_TripleCast` | Fires three shards at once |
| `Shard_Teleport` | Player teleports to shard location on impact |
| `Shard_TeleportAndHeal` | Teleport + heals the player |

---

## Stat System

Entities have the following stats, grouped by category:

**Major:** `Strength`, `Agility`, `Intelligence`, `Vitality`

**Offense:** `Damage`, `AttackSpeed`, `CritChance`, `CritPower`

**Defense:** `Armor`, `ArmorReduction`, `Evasion`

**Resource:** `MaxHealth`, `HealthRegen`

---

## Architecture

### State Machine
Both player and enemies use a generic `StateMachine` with `EntityState` as the base class. Player-specific states inherit from `PlayerState`; enemy states from `EnemyState`.

**Player states:** Idle, Move, Fall, Grounded, Aired, Jump Attack, Basic Attack, Counter Attack, Wall Slide, Wall Jump

**Enemy states:** Idle, Move, Battle, Attack, Grounded, Hide, Stunned, Dead

### Data-Driven Design
Game data is defined via ScriptableObjects:
- `Stat_SetupSO` — base stat configuration per entity
- `Skill_DataSO` — skill metadata and cooldowns
- `Item_DataSO` / `Equipment_DataSO` / `ConsumableItem_DataSO` — item definitions
- `ItemEffect_DataSO` — modular item effect definitions

### Interfaces
| Interface | Purpose |
|---|---|
| `IDamageable` | Any entity that can take damage |
| `ICounterable` | Enemies that can be countered by the player |
| `IInteractable` | Objects the player can interact with (NPCs, buffs) |
| `ISaveable` | Components that persist data to the save file |

### Save System
Game state is serialized to JSON using `JsonUtility` and saved to disk via `FileDataHandler`. Encryption is supported using XOR with a key, toggled at initialization.

---

## Requirements

- **Unity** 2022.3 LTS or newer (2D project)
- **TextMeshPro** (included in Assets)

---

## Getting Started

1. Clone or download the repository
2. Open the project in Unity Hub
3. Open `Assets/Scenes/MainMenu.unity`
4. Press **Play** in the Unity Editor

---

## Built With

- **Unity** — Game engine
- **C#** — Scripting language
- **TextMeshPro** — UI text rendering
- **Unity Physics 2D** — Collision and rigidbody simulation
