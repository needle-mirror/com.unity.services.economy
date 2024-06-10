# Virtual Purchase assets

A virtual purchase is a transactional resource used to implement a shop or trade feature. Virtual purchases allow players to purchase previously defined in-game currencies or inventory items using their in-game currency balances or items already in their inventory, rather than real money.

A Virtual Purchase asset file has `.ecv` for extension.

## Creation

Right-click on the `Project Window` then select `Create > Services > Economy Virtual Purchase Configuration` to create a Virtual Purchase asset.

Once created, a corresponding item will appear in the deployment window, and will allow you to deploy the newly created Virtual Purchase file.

## Edition

Any text editor can modify an Economy file, however, it is always preferred to choose one that [supports JSON Schema definitions](https://json-schema.org/implementations#editors) to benefit from code completion.

## Deletion

Deleting an Economy asset isn't enough to remove the resouce from the service, it is also needed to delete it using the Unity dashboard.

## Format and schema

A Virtual Purchase asset is written in JSON, and its schema is defined [here](https://ugs-config-schemas.unity3d.com/v1/economy/economy-virtual-purchase.schema.json).

Here is an example of a Virtual Purchase asset content.

```json
{
  "name": "My Virtual Purchase",
  "costs": [
    {
      "resourceId": "MY_RESOURCE_ID",
      "amount": 2
    }
  ],
  "rewards": [
    {
      "resourceId": "MY_RESOURCE_ID_2",
      "amount": 6,
      "defaultInstanceData": null
    }
  ]
}
```

## Naming Restrictions

The name of the created file will be used as the ID for the Virtual Purchase object, therefore it can only contain uppercase letters, numbers, and underscores.
