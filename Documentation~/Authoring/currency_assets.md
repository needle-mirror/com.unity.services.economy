# Currency assets

A Currency in Economy defines virtual money that exists within your game.

A Currency asset file has `.ecc` for extension.

## Creation

Right-click on the `Project Window` then select `Create > Services > Economy Currency Configuration` to create a Currency asset.

Once created, a corresponding item will appear in the deployment window, and will allow you to deploy the newly created Currency file.

## Edition

Any text editor can modify an Economy file, however, it is always preferred to choose one that [supports JSON Schema definitions](https://json-schema.org/implementations#editors) to benefit from code completion.

## Deletion

Deleting an Economy asset isn't enough to remove the resouce from the service, it is also needed to delete it using the Unity dashboard.

## Format and schema

A Currency asset is written in JSON, and its schema is defined [here](https://ugs-config-schemas.unity3d.com/v1/economy/economy-currency.schema.json).

Here is an example of a Currency asset content.

```json
{
  "name": "My Currency",
  "initial": 1,
  "max": 50
}
```

## Naming Restrictions

The name of the created file will be used as the ID for the Currency object, therefore it can only contain uppercase letters, numbers, and underscores.
