---
title: Merge PhysBone
weight: 1
---

# Merge PhysBone

This component merges multiple PhysBone Components into one Component with MultiChildType == Ignore.

## Settings

![component.png](component.png)

### Components

The list of PhysBone Component.

Drop to None element at the bottom to add PhysBone and reset to None to remove from the list.

### Overrides

Below the configurations, there's configurations like VRCPhysBone.
For each property, you may select `Copy` to copy value from source property
(only available if values from the sources are same) or `Override` to set new value instead.

For colliders, you can select `Merge` to merge colliders array from source physbones.

For Endpoint Position, you can select `Clear` to apply [Clear Endpoint Position](../clear-endpoint-position)
