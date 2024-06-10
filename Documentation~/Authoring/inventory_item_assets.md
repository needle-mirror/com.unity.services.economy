# Inventory Item assets

Inventory Items are objects that a player can own in-game. You don’t need to set any property for your resources, but it is always possible to add Custom data to them.

An Inventory Item asset file has `.eci` for extension.

## Creation

Right-click on the `Project Window` then select `Create > Services > Economy Inventory Configuration` to create a Inventory Item asset.

Once created, a corresponding item will appear in the deployment window, and will allow you to deploy the newly created Inventory Item file.

## Edition

Any text editor can modify an Economy file, however, it is always preferred to choose an IDE that [supports JSON Schema definitions](https://json-schema.org/implementations#editors) to benefit from code completion.

## Deletion

Deleting an Economy asset isn't enough to remove the resouce from the service, it is also needed to delete it using the Unity dashboard.

## Format and schema

An Inventory Item asset is written in JSON, and its schema is defined [here](https://ugs-config-schemas.unity3d.com/v1/economy/economy-inventory.schema.json).

Here is an example of a Inventory Item asset content.

```json
{
  "name": "My Item",
  "customData": {
    "custom": "data"
  }
}
```

## Naming Restrictions

The name of the created file will be used as the ID for the Inventory Item object, therefore it can only contain uppercase letters, numbers, and underscores.
