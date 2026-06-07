# Pixygon — Addressables

Helpers around Unity Addressables: load/unload content and scenes, manage audio, and
self-clean spawned objects.

## Key types

| Type | What it is |
|---|---|
| **`AddressableLoader`** | Load/release addressable assets. |
| **`AddressableSceneLoader`** | Load/unload scenes by address. |
| **`AudioMaster` / `AudioFixer`** | Addressable audio loading + fix-ups. |
| **`SelfCleanup`** | Auto-release/destroy a spawned addressable when done. |

## Dependencies

`com.unity.addressables`, `com.pixygon.debugtool`.

## Status

`0.5.0`. Content-loading utility; depended on by `effects` and `pagedcontent`.
