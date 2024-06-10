# Money Purchase assets

A real money purchase is a transactional resource allowing players to purchase in-game currencies or items using real money through an app store.

A Money Purchase asset file has `.ecr` for extension.

## Creation

Right-click on the `Project Window` then select `Create > Services > Economy Money Purchase Configuration` to create a Money Purchase asset.

Once created, a corresponding item will appear in the deployment window, and will allow you to deploy the newly created Money Purchase file.

## Edition

Any text editor can modify an Economy file, however, it is always preferred to choose one that [supports JSON Schema definitions](https://json-schema.org/implementations#editors) to benefit from code completion.

## Deletion

Deleting an Economy asset isn't enough to remove the resouce from the service, it is also needed to delete it using the Unity dashboard.

## Format and schema

A Money Purchase asset is written in JSON, and its schema is defined [here](https://ugs-config-schemas.unity3d.com/v1/economy/economy-real-purchase.schema.json).

Here is an example of a Money Purchase asset content.

```json
{
  "name": "My Money Purchase",
  "storeIdentifiers":
    {
      "appleAppStore": "my_id",
      "googlePlayStore": "my_id"
    },
  "rewards": [
    {
      "resourceId": "MY_RESOURCE_ID",
      "amount": 6,
      "defaultInstanceData": null
    }
  ]
}
```

## Naming Restrictions

The name of the created file will be used as the ID for the Money Purchase object, therefore it can only contain uppercase letters, numbers, and underscores.
