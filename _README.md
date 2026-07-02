# Project1

Unity prototype. Resource-gathering / base-defense hybrid. Incomplete.

## Status

On hold. Last commit: 2026-06-02 ("Hiatus"). No completed game loop, no build pipeline, no documentation prior to this file.

## Stack

- Unity, Universal Render Pipeline
- Starter Assets third-person controller
- Custom shaders: Simple Toon, Flexible Cel Shader
- New Input System

## Implemented systems

| System | Scope |
|---|---|
| AI | Enemy targeting, base enemy behavior |
| Defensive Towers | Attack, area-of-effect, slow, per-tower leveling |
| Mine | Resource node spawning and leveling |
| Resources | Wood, stone, gold, gems; player inventory |
| Save/Load | Full save system: towers, mines, resources, inventory |
| Spawning | Enemy/wave spawner |
| Summoning System | Token-based summon economy, shop, transactions |
| Tree | Gatherable trees, stumps, axe interaction |
| Upgrade Logic | Gather speed and stat leveling |
| Wall System | Grid-based tiles, wall health, triggers |
| UI | World-space text updaters, notifications, debug overlay |

## Not implemented

- Win/loss condition
- Game loop tying systems together
- Build/export pipeline
- Tests

## License

All Rights Reserved
