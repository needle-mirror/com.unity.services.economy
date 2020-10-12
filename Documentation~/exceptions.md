# Exceptions

## Economy Exception

An `EconomyException` will be thrown when there is a problem with one of the operations in the SDK. These exceptions should 
be handled by calling code. The methods that can throw these exceptions are clearly marked in the method documentation.

The `EconomyException` has the following fields in addition to those normally provided by C# `Exception`:

- `Reason`: An `EconomyExceptionReason` is an enum value that describes what category of issue occurred. This is provided
to allow a code-friendly way of detecting and handling the different types of errors that can be thrown.

Inspect the `Message` field on an `EconomyException` for a human-readable description of the error that was thrown.

There are method specific exceptions that may also be thrown by the Economy SDK. These are specified in the method documentation.
