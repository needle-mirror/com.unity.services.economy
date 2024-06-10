# Economy Authoring

This module allows users to author, modify, and deploy Economy assets directly from the Unity Editor.

> NOTE1: Economy Authoring is only supported on Unity 2021.3 and above.
>
> NOTE2: Economy Authoring is enabled only if the [Deployment package](https://docs.unity3d.com/Packages/com.unity.services.deployment@latest) is installed.

## Deployment Window

The Deployment Window is a core feature of the Deployment package.

The purpose of the Deployment Window is to allow all services
to have a single cohesive interface for Deployment needs.

The Deployment Window provides a uniform deployment interface for all services.
It allows you to upload cloud assets for your respective cloud service.

For more information, consult the [com.unity.services.deployment](https://docs.unity3d.com/Packages/com.unity.services.deployment@latest) package documentation.

## Create Economy Assets

There are 4 types of Economy assets that can be created and deployed using the deployment window.
They are all using the JSON format, and support a JSON Schema definition.

### Currency assets

Right-click on the `Project Window` then select `Create > Services > Economy Currency Configuration` to create a Currency file.

The Deployment Window automatically detects these files to be deployed at a later time.

For more information on how to create and modify Currency Assets,
please see the [Currency assets](./currency_assets.md) documentation.

### Inventory Item assets

Right-click on the `Project Window` then select `Create > Services > Economy Inventory Item Configuration` to create an Inventory Item file.

The Deployment Window automatically detects these files to be deployed at a later time.

For more information on how to create and modify Inventory Item Assets,
please see the [Inventory Item assets](./inventory_item_assets.md) documentation.

### Virtual Purchase assets

Right-click on the `Project Window` then select `Create > Services > Economy Virtual Purchase Configuration` to create a Virtual Purchase file.

The Deployment Window automatically detects these files to be deployed at a later time.

For more information on how to create and modify Virtual Purchase Assets,
please see the [Virtual Purchase assets](./virtual_purchase_assets.md) documentation.

### Money Purchase assets

Right-click on the `Project Window` then select `Create > Services > Economy Money Purchase Configuration` to create a Money Purchase file.

The Deployment Window automatically detects these files to be deployed at a later time.

For more information on how to create and modify Money Purchase Assets,
please see the [Money Purchase assets](./money_purchase_assets.md) documentation.
