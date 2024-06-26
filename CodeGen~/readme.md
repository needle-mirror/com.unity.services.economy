# Generating the Economy API SDK

1. Download the latest version of the code generator https://github.com/Unity-Technologies/openapi-generators/releases
2. Clone the spec from the repo here https://github.com/Unity-Technologies/unity-services-api-docs
3. Update the sample spec here to point at an output directory that exists, and an input directory from the economy yaml file in the above repo
4. Run `java -jar /path/to/generator.jar generate -c /path/to/the/spec.yaml`
5. Replace the contents of the code generated code in this repo with the generated output

# Custom changes to the Generated Economy Admin API

In `Client/Models`, for `ItemRequests` and `ResourceRequests`:
- In the constructor, CustomData was changed to be equal to `(IDeserializable)(customData is null ? new JsonObject(new object()) : JsonObject.GetNewJsonObjectResponse(customData));`
- Resource members with the `DataMember` tag were also changed so that their `EmitDefaultValue` is `true`
