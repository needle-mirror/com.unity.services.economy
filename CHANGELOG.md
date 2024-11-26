# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [3.5.1] - 2024-11-26
### Fixed
- Fixed inspector loading for service assets, below Unity 6
- Fixed button top open in dashboard

## [3.5.0] - 2024-11-12

### Added
- View in Deployment Window button for Economy resource files, dependent on Deployment package version 1.4.0.
- Added unity-installation-id in the header of the REST API request in order to return the overridden economy content.
- Added analytics-user-id in the header of the REST API request as it can be set as ExternalUserId if needed.

### Fixed
- Fix Economy deployment assets help URLs
- Fix redundant logging when an exception is thrown
- Raised log when deployment window is not available to error
- Added missing table of contents to package documentation

## [3.4.1] - 2024-06-10
* Remove feature flag for services instances global point of access
* Moved create configuration menu items under "Services"

## [3.4.0] - 2024-04-25
* Adding support for services instances and global point of access.
* Added Apple privacy manifest to comply with Apple's new privacy requirements. More details on how the Unity Engine supports this can be found [here](https://forum.unity.com/threads/apple-privacy-manifest-updates-for-unity-engine.1529026/).
* Upgraded `com.unity.services.core` from 1.12.0 to 1.12.5 to include their Apple privacy manifest.
* Upgraded `com.unity.services.authentication` from 3.1.0 to 3.3.1 to include their Apple privacy manifest.

## [3.3.0] - 2023-10-19
* Added schema field to economy resource files.
* Fixed a bug preventing from deploying economy resource containing schema field.
* Fixed a bug where deploying a local Currency resource file that omits `initial` and `max` would not update the remote resource to their default values.
* Fixed a bug where deploying a local resource file that omits `customData` would not update the remote resource to a default customData.
* Fixed a bug that would halt the deployment's execution when a local resource file's content failed to be deserialized.

## [3.2.1] - 2023-09-19
* Add Economy resource deployment support through the Deployment Window or Deployment api.

## [3.1.4] - 2023-08-02
* Fixed a bug that could cause SyncConfigurationAsync to freeze the game loop.

## [3.1.2] - 2023-03-13
* Fixed a bug that was throwing an exception when setting UnityServices.ExternalUserId.

## [3.1.1] - 2022-12-21
* Fixed a bug that was throwing an exception when fetching virtual purchases with custom data.
* Introduction of the new sync configuration workflow. Old methods have been deprecated. See docs for more info on new workflow.
* Fixed a bug that prevented the Store Identifiers field from being populated on fetch real money purchases requests
* Removed a redundant log message when fetching a non-existent currency from your configuration
* Fixed a bug which meant writeLocks weren't being set correctly

## [3.0.0] - 2022-08-02
* Removed remote-config-runtime dependency
* CustomDataDeserializable has been added to configuration items and is of type `IDeserializable`
* Added missing XmlDoc to public IEconomyConfigurationApiClient interface
* Added missing XmlDoc to public IEconomyPlayerBalancesApiClient interface
* Added missing XmlDoc to public IEconomyPlayerInventoryApiClient interface
* Added missing XmlDoc to public IEconomyPurchasesApiClientApi interface

## [2.0.3] - 2022-05-31
* Update dependencies

## [2.0.2] - 2022-05-24
* Update license

## [2.0.1] - 2022-05-16
**Breaking Change:** Instance data on players inventory items is now passed in as an `object` and returned as a `IDeserializable` 
* Pre/exp tags have been removed
* Removed update instance section in inventory UI sample
* Fixed backwards compatible interface for `Economy.Purchases`

## [2.0.1-exp.2] - 2022-05-11
* Add missing samples file

## [2.0.1-exp.1] - 2022-05-10
**Breaking Change:** Code in the `Unity.Services.Economy.Editor.Settings` namespace has been made internal as it was never meant to be public.
* Support has been added for the breaking changes in 2.0.0-pre.1 - the old interface will still work but is marked obsolete.

## [2.0.0-pre.2] - 2022-03-22
* Update license

## [2.0.0-pre.1] - 2022-03-14
### Breaking Changes
* The Economy service is now accessed using `EconomyService.Instance.<API>` e.g, `EconomyService.Instance.PlayerBalances`
* The `GetBalancesResult` and `GetInventoryResult` constructors have been made internal
* `Options` and `Args` classes have been extracted out of their parent classes so they are accessed differently. For example, `GetBalancesOptions` is accessed using `GetBalancesOptions` instead of `PlayerBalances.GetBalancesOptions`
* Moved all Economy exception types into the namespace `Unity.Services.Economy`

### New Features
* Support for Game Overrides added
* Added specific rate limited exception `EconomyRateLimitedException` with retry-after details
* Added the Project Settings tab with link to Economy dashboard
* Renamed UI sample and added stripped down code sample

## [1.0.0-pre.8] - 2022-01-20
* Fixed a bug that was causing errors when users signed in, signed out and then signed in again as a new user.
* Removed Economy’s assembly dependency on the Authentication package

## [1.0.0-pre.7] - 2021-12-13
* Added more detailed logging for exceptions.
* Fixed a bug that was causing the created and modified dates to be set incorrectly in the SetBalancesAsync function.

## [1.0.0-pre.6] - 2021-10-20
* Fixed the UI samples and made them responsive to screen size.
* Added a new exception type `EconomyValidationException` that inherits from `EconomyException`, see documentation for more details.
* Fixed leak warnings.

## [1.0.0-pre.5] - 2021-10-12
* Some models have been made internal as they were not designed to be used externally. This has meant we have needed to change some property types and rename some classes. Functionality change is minimal, with the exception of the `GoogleStore` object, detailed below. Here is a full list of changes:
* The `Data` property on the `EconomyAppleAppStorePurchaseFailedException` class has changed type from `PlayerPurchaseAppleappstoreResponse` to `RedeemAppleAppStorePurchaseResult`.
* The `Data` property on the `EconomyGooglePlayStorePurchaseFailedException` class has changed type from `PlayerPurchaseGoogleplaystoreResponse` to `RedeemGooglePlayPurchaseResult`.
* The `Verification` property on the `RedeemAppleAppStorePurchaseResult` has changed type from `Verification` to `AppleVerification`.
  * The `Store` property on the `AppleVerification` has changed type from `Store` to `AppleStore`.
* The `Verification` property on the `RedeemGooglePlayPurchaseResult` has changed type from `Verification` to `GoogleVerification`.
    * The `Store` property on the `GoogleVerification` has changed type from `Store` to `GoogleStore`. There is a functional change here - `GoogleStore` no longer contains the `Code` and `Message` properties - it only contains the `Receipt` property. `AppleStore` still contains the `Code` and `Message` properties. For more information on these models, see the documentation.
* Updated Core and Authentication dependency versions

## [1.0.0] - 2021-08-23
* Open Beta release
* Renaming changes - Instance -> PlayersInventoryItem and Item -> InventoryItem
* Allows users to redeem Google Play Store in-app purchases
* Introduces options and arguments objects for API calls
* Removed all current obsolete methods

## [0.7.0-preview] - 2021-07-30

### New Features

* Improved error handling and detail in Economy exceptions
* Economy will now check a user is signed in via Authentication before making service requests
* Obsolete method `MakeVirtualPurchase` has been removed, use MakeVirtualPurchaseAsync instead
* Obsolete method `GetReferencedItem<ConfigurationItemDefinition>` has been removed, use GetReferencedConfigurationItem instead

## [0.6.0-preview] - 2021-07-07

### New Features

* Allows users to make real money Apple App Store purchases

## [0.5.0-preview] - 2021-06-17

### New Features

* New Scriptable Objects to allow using Economy features with Game Objects
* Helper methods for quickly accessing different parts of your configuration / making purchases

### Fixed

* Dependencies have been updated

## [0.4.0-preview] - 2021-05-26

### New Features

* Events available for SDK balance and inventory item updates

### Bug Fix

* When a currency or inventory item configuration cannot be found, null will now be returned instead of throwing an exception
* Purchase, Player Balance and Player Inventory methods will now function correctly on iOS
* Configurations will now deserialise correctly on iOS
* Documentation has been improved with more detail

## [0.3.0-preview] - 2021-05-17

### New Features

* Access and make virtual purchases
* Updated documentation
* Two importable samples have been added, one basic script example and one with UI
* Core SDK integration - Economy now follows the Core initialisation and authentication flows

### Bug Fix

* Resolved editor warnings around unused editor folder

## [0.2.2-preview] - 2021-05-07

### Bug Fix

* Fixes configurations to work with the new structure as sent by the Economy service

Note, that in order to use this and subsequent versions of the SDK you need to republish your Economy configuration in the dashboard.

## [0.2.1-preview] - 2021-05-06

### Bug Fix

* Fixes a clash between Utiltiies and Auth by removing the unneeded dependency

## [0.2.0-preview] - 2021-05-05

### New Features

* Access the current inventory item configuration
* Access and update the currently signed in player's inventory item instances
* Improved error handling - methods will now throw consistent exceptions
* Improved documentation

## [0.1.1-preview] - 2021-04-14

### Bug Fix

* Resolve an issue with Utilities version not matching scoped registry.

## [0.1.0-preview] - 2021-04-14

This is the initial release of the Game Economy SDK. Note that this version is only available from the candidates registry.

### New Features

* Access the current currency configuration
* Access the currently signed in player's currency balances
* Set, increment or decrement the currently signed in player's balances

### Known Issues

* Some exceptions (e.g. exceeding the max amount for a currency) return a generic error rather than a specific error message
