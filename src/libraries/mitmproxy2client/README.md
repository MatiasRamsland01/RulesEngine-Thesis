# Mitm2Client

Mitm2Client is a command-line program that generate a C# HTTP client from API responses using different tools. The program is useful when you want to create an HTTP client from an API but not want to do it manually. The combination of different software tools makes it easy to create and use the http client.

## Commands

Commands for running the program after setup.

### Building

```bash
docker compose --profile mitmproxy2client up --build
```

### Running

```bash
docker compose --profile mitmproxy2client up
```

## Examples of endpoints need to be set in .evn for API generation

BASE_URL="<https://bouvet.cvpartner.com/api/>"
END_POINT="/v1/users"

BASE_URL="<https://rest.tv2.no/weather-dw-rest/>"
END_POINT="forecast/places?place=135295493&days=3"

## Setup

### Environmental variables (`.env`)

The program comes with an `.env` file used to set up the different environment variables on your system. From .env you can set up if you want to store previous generated files for examle. You may also change the default location where the files are stored.
Below are the different variables that can be altered from the `.env` file.

```bash
# Save the previous FLOW file(response) to backup folder(If old FLOW file exists).
SAVE_OLD_FLOW=true
# Save the previous MITM2SWAGGER file(response) to backup folder(If old MITM2SWAGGER file exists).
SAVE_OLD_MITM2SWAGGER_GEN=true
# Save the previous COMPONENTS file(response) to backup folder(If old COMPONENTS file exists).
SAVE_OLD_COMPONENTS_GEN=true
# Save the previous C# Client file(response) to backup folder(If old C# Client file exists).
SAVE_OLD_CS_CLIENT=true
# Save the previous C# Client Contract file(response) to backup folder(If old C# Client Contract file exists).
SAVE_OLD_CS_CLIENT_CONTRACT=true
# Save the previous SWAGGEROPENAPI file(response) to backup folder(If old SWAGGEROPENAPI file exists).
# !! Currently not implemented !!
SAVE_OLD_SWAGGEROPENAPI=true
# Currently in use for CV partner requests.
IS_CV_PARTNER=true
# The auth cookie for the requests (If needed).
REQUEST_HEADER_COOKIE=""
# The auth CSRF token for the requests (If needed).
REQUEST_HEADER_CSRF=""
# The user agent for the requests.
REQUEST_HEADER_USER_AGENT='Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/110.0'
# The content type for the requests.
REQUEST_HEADER_CONTENT_TYPE='application/json'
# Path for the client confirguration file in docker container.
NSWAG_CLIENT_CONFIG_PATH="/src/shared/net7CSharpClient.nswag"
# Path for the SSL certificates inside docker container.
SSL_CERT_PATH="/certs/mitmproxy-ca-cert.pem"
# Shared path for containers.
SHARED_FOLDER_PATH="/src/shared/"
# Shared backup path for containers.
BACKUP_FOLDER_PATH="/src/shared/backup/"
# The base URL for the request (Example from www.storm.no: https://rest.tv2.no/weather-dw-rest/)
BASE_URL=""
# The enpoint for the request (Example from www.storm.no: forecast/places?place=135295493&days=3)
END_POINT=""
# For CV Partner, sets the max number of CV's to fetch.
NUM_CVS_TO_FETCH=2500
# For CV Partner, sets the offset(index) starting point of fetching CV's. 0 = from start.
START_FETCH_FROM_INDEX=0
# Sets the timeout for requests.
REQUESTS_TIMEOUT=60
```

### NSwag configuration (`nswagconfig.nswag`)

Also included is a configuration file for the HttpClient generator NSwag. `nswagconfig.nswag` contains many options for creating your Http Client. The format of the `nswagconfig.nswag` is defined by NSwag which is JSON. The file could also be saved with the `.json` suffix.

The following is a base NSwag configuration used in Mitm2Client.

```JSON
{
  "runtime": "Net70",
  "defaultVariables": "InputSpec=Spec",
  "documentGenerator": {
    "fromDocument": {
      "json": "$(InputSpec)",
      "url": "",
      "output": null,
      "newLineBehavior": "Auto"
    }
  },
  "codeGenerators": {
    "openApiToCSharpClient": {
      "clientBaseClass": null,
      "configurationClass": null,
      "generateClientClasses": true,
      "generateClientInterfaces": true,
      "clientBaseInterface": null,
      "injectHttpClient": true,
      "disposeHttpClient": true,
      "protectedMethods": [],
      "generateExceptionClasses": true,
      "exceptionClass": "ApiException",
      "wrapDtoExceptions": true,
      "useHttpClientCreationMethod": false,
      "httpClientType": "System.Net.Http.HttpClient",
      "useHttpRequestMessageCreationMethod": false,
      "useBaseUrl": true,
      "generateBaseUrlProperty": true,
      "generateSyncMethods": false,
      "generatePrepareRequestAndProcessResponseAsAsyncMethods": false,
      "exposeJsonSerializerSettings": false,
      "clientClassAccessModifier": "public",
      "typeAccessModifier": "public",
      "generateContractsOutput": true,
      "contractsNamespace": "NSwagClientContractNameSpace",
      "contractsOutputFilePath": "./src/shared/NSwagClientContract.cs",
      "parameterDateTimeFormat": "s",
      "parameterDateFormat": "yyyy-MM-dd",
      "generateUpdateJsonSerializerSettingsMethod": true,
      "useRequestAndResponseSerializationSettings": false,
      "serializeTypeInformation": false,
      "queryNullValue": "",
      "className": "{controller}Client",
      "operationGenerationMode": "SingleClientFromOperationId",
      "additionalNamespaceUsages": [],
      "additionalContractNamespaceUsages": [],
      "generateOptionalParameters": false,
      "generateJsonMethods": true,
      "enforceFlagEnums": false,
      "parameterArrayType": "System.Collections.Generic.IEnumerable",
      "parameterDictionaryType": "System.Collections.Generic.IDictionary",
      "responseArrayType": "System.Collections.Generic.ICollection",
      "responseDictionaryType": "System.Collections.Generic.IDictionary",
      "wrapResponses": true,
      "wrapResponseMethods": [],
      "generateResponseClasses": true,
      "responseClass": "SwaggerResponse",
      "namespace": "NSwagClientNameSpace",
      "requiredPropertiesMustBeDefined": true,
      "dateType": "System.DateTimeOffset",
      "jsonConverters": null,
      "anyType": "object",
      "dateTimeType": "System.DateTimeOffset",
      "timeType": "System.TimeSpan",
      "timeSpanType": "System.TimeSpan",
      "arrayType": "System.Collections.Generic.ICollection",
      "arrayInstanceType": "System.Collections.ObjectModel.Collection",
      "dictionaryType": "System.Collections.Generic.IDictionary",
      "dictionaryInstanceType": "System.Collections.Generic.Dictionary",
      "arrayBaseType": "System.Collections.ObjectModel.Collection",
      "dictionaryBaseType": "System.Collections.Generic.Dictionary",
      "classStyle": "Poco",
      "jsonLibrary": "SystemTextJson",
      "generateDefaultValues": true,
      "generateDataAnnotations": false,
      "excludedTypeNames": [],
      "excludedParameterNames": [],
      "handleReferences": false,
      "generateImmutableArrayProperties": false,
      "generateImmutableDictionaryProperties": false,
      "jsonSerializerSettingsTransformationMethod": null,
      "inlineNamedArrays": false,
      "inlineNamedDictionaries": false,
      "inlineNamedTuples": true,
      "inlineNamedAny": false,
      "generateDtoTypes": true,
      "generateOptionalPropertiesAsNullable": true,
      "generateNullableReferenceTypes": true,
      "templateDirectory": null,
      "typeNameGeneratorType": null,
      "propertyNameGeneratorType": null,
      "enumNameGeneratorType": null,
      "serviceHost": null,
      "serviceSchemes": null,
      "output": "./src/shared/NSwagClient.cs",
      "newLineBehavior": "Auto"
    }
  }
}
```

### API Client Configuration Options

This documentation outlines the available configuration options for an API client. These options can be set in a JSON configuration file or through code.

#### `runtime`

- Type: string
- Default: "Net70"

Specifies the .NET runtime version to be used by the API client. By default, the Net70 runtime version is used.

#### `defaultVariables`

- Type: object
- Default: null

Specifies default variables to be used by the API client. This can be useful for setting values that are commonly used across requests.

#### `documentGenerator`

- Type: object
- Default: { "fromDocument": { "url": "", "output": null, "newLineBehavior": "Auto" } }

Specifies options for generating API documentation. By default, the documentGenerator option includes a fromDocument object that specifies the URL for the documentation, the output format, and the behavior for new lines.

#### `url`

    Type: string
    Default: ""

Specifies the URL for the API documentation. This is used when generating documentation for the API client.

#### `output`

    Type: string
    Default: null

Specifies the output format for the generated documentation. By default, this option is set to null, which means that the output format will be determined automatically.

#### `newLineBehavior`

    Type: string
    Default: "Auto"

Specifies the behavior for new lines when generating documentation. This option can be set to "Auto", "CRLF", or "LF". By default, the "Auto" option is used, which means that the behavior will be determined automatically based on the operating system.

#### `codeGenerators`

- Type: object
- Default: "openApiToCSharpClient"

Specifies options for generating code for the API client. By default, the codeGenerators option includes an openApiToCSharpClient object that specifies various options for generating a C# client from an OpenAPI specification.

#### `clientBaseClass`

- Type: string
- Default: null

Specifies the name of a base class to be used for the generated client classes. If this option is not set, the client classes will not inherit from any base class.

#### `configurationClass`

- Type: string
- Default: null

Specifies the name of a configuration class to be used for the generated client classes. If this option is not set, the client classes will not have a separate configuration class.

#### `generateClientClasses`

- Type: boolean
- Default: true

Set this option to false if you do not want to generate client classes for the API client. By default, the API client will generate client classes.

#### `generateClientInterfaces`

- Type: boolean
- Default: false

Set this option to true if you want to generate client interfaces instead of client classes. By default, the API client will generate client classes.

#### `clientBaseInterface`

- Type: string
- Default: null

Specifies the name of a base interface to be used for the generated client interfaces. If this option is not set, the client interfaces will not inherit from any base interface.

#### `injectHttpClient`

- Type: boolean
- Default: true

Set this option to false if you do not want to inject the HttpClient instance into the generated client classes or interfaces. By default, the API client will inject the HttpClient instance.

#### `disposeHttpClient`

- Type: boolean
- Default: true

Set this option to false if you do not want the generated client classes or interfaces to dispose of the HttpClient instance when they are disposed. By default, the API client will dispose of the HttpClient instance.

#### `protectedMethods`

- Type: array
- Default: []

Specifies an array of method names to be marked as protected in the generated client classes or interfaces. By default, no methods are marked as protected.

#### `generateExceptionClasses`

- Type: boolean
- Default: true

Set this option to false if you do not want to generate exception classes for the API client. By default, the API client will generate exception classes.

#### `exceptionClass`

- Type: string
- Default: "ApiException"

Specifies the name of the base exception class to be used for the generated exception classes. If this option is not set, the generated exception classes will inherit from the default ApiException class.

#### `wrapDtoExceptions`

- Type: boolean
- Default: true

Set this option to false if you do not want to wrap DTO exceptions in the generated exception classes. By default, the API client will wrap DTO exceptions in the generated exception classes.

#### `useHttpClientCreationMethod`

- Type: boolean
- Default: false

Set this option to true to use a custom method to create the HttpClient instance. By default, the API client will use the HttpClient constructor to create a new instance.

#### `httpClientType`

- Type: string
- Default: "System.Net.Http.HttpClient"

This option specifies the type of HttpClient to be used by the API client. By default, the System.Net.Http.HttpClient type is used.

#### `useHttpRequestMessageCreationMethod`

- Type: boolean
- Default: false

Set this option to true to use a custom method to create the HttpRequestMessage instance. By default, the API client will use the HttpRequestMessage constructor to create a new instance.

#### `useBaseUrl`

- Type: boolean
- Default: true

Set this option to false if you do not want to use a base URL for the API client. By default, the API client will use a base URL.

#### `generateBaseUrlProperty`

- Type: boolean
- Default: true

Set this option to false if you do not want the API client to generate a BaseUrl property. By default, the API client will generate a BaseUrl property based on the specified base URL.

#### `generateSyncMethods`

- Type: boolean
- Default: false

Set this option to true if you want to generate synchronous methods in addition to the asynchronous methods. By default, only asynchronous methods are generated.

#### `generatePrepareRequestAndProcessResponseAsAsyncMethods`

- Type: boolean
- Default: false

Set this option to true if you want to generate PrepareRequestAsync and ProcessResponseAsync methods for each request. By default, these methods are not generated.

#### `exposeJsonSerializerSettings`

- Type: boolean
- Default: false

Set this option to true if you want to expose the JsonSerializerSettings instance used by the API client. By default, this instance is not exposed.

#### `clientClassAccessModifier`

- Type: string
- Default: "public"

Specifies the access modifier for the generated client class. By default, the access modifier is set to public.

#### `typeAccessModifier`

- Type: string
- Default: "public"

Specifies the access modifier for generated types (such as models and exceptions). By default, the access modifier is set to public.

#### `generateContractsOutput`

- Type: boolean
- Default: false

Set this option to true if you want to generate a contracts output file. By default, this file is not generated.

#### `contractsNamespace`

- Type: string
- Default: null

Specifies the namespace to be used for the contracts output file. If this option is not set, the namespace will be determined automatically.

#### `contractsOutputFilePath`

- Type: string
- Default: null

Specifies the file path for the contracts output file. If this option is not set, the contracts output file will not be generated.

#### `parameterDateTimeFormat`

- Type: string
- Default: "s"

Specifies the format to be used for DateTime parameters in the generated methods. By default, the ISO 8601 format is used.

#### `parameterDateFormat`

- Type: string
- Default: "yyyy-MM-dd"

Specifies the format to be used for DateTimeOffset parameters in the generated methods. By default, the yyyy-MM-dd format is used.

#### `generateUpdateJsonSerializerSettingsMethod`

- Type: boolean
- Default: true

Set this option to false if you do not want to generate an UpdateJsonSerializerSettings method for the API client. By default, this method is generated.

#### `useRequestAndResponseSerializationSettings`

- Type: boolean
- Default: false

Set this option to true if you want to use separate JsonSerializerSettings instances for request and response serialization. By default, a single JsonSerializerSettings instance is used for both request and response serialization.

#### `serializeTypeInformation`

- Type: boolean
- Default: false

Set this option to true if you want to serialize type information in the generated JSON. By default, type information is not serialized.

#### `queryNullValue`

- Type: string
- Default: ""

Specifies the value to be used for null query parameters in the generated requests. By default, an empty string is used.

#### `className`

- Type: string
- Default: "{controller}Client"

Specifies the name of the generated API client class. By default, the class name is based on the name of the OpenAPI controller.

#### `operationGenerationMode`

- Type: string
- Default: "MultipleClientsFromOperationId"

Specifies how the client methods are generated. By default, the API client will generate multiple clients for each operation ID.

#### `additionalNamespaceUsages`

- Type: array
- Default: []

Specifies an array of additional namespaces to be used in the generated code.

#### `additionalContractNamespaceUsages`

- Type: array
- Default: []

Specifies an array of additional namespaces to be used in the contracts output file.

#### `generateOptionalParameters`

- Type: boolean
- Default: false

Set this option to true if you want to generate optional parameters in the generated client methods. By default, optional parameters are not generated.

#### `generateJsonMethods`

- Type: boolean
- Default: false

Set this option to true if you want to generate ToJson and FromJson methods for the models in the generated code. By default, these methods are not generated.

#### `enforceFlagEnums`

- Type: boolean
- Default: false

Set this option to true if you want to enforce the use of flag enums in the generated code. By default, flag enums are not enforced.

#### `parameterArrayType`

- Type: string
- Default: "System.Collections.Generic.IEnumerable"

Specifies the type to be used for arrays in the generated client method parameters. By default, the System.Collections.Generic.IEnumerable type is used.

#### `parameterDictionaryType`

- Type: string
- Default: "System.Collections.Generic.IDictionary"

Specifies the type to be used for dictionaries in the generated client method parameters. By default, the System.Collections.Generic.IDictionary type is used.

#### `responseArrayType`

- Type: string
- Default: "System.Collections.Generic.ICollection"

Specifies the type to be used for arrays in the generated client method responses. By default, the System.Collections.Generic.ICollection type is used.

#### `responseDictionaryType`

- Type: string
- Default: "System.Collections.Generic.IDictionary"

Specifies the type to be used for dictionaries in the generated client method responses. By default, the System.Collections.Generic.IDictionary type is used.

#### `wrapResponses`

- Type: boolean
- Default: false

Set this option to true if you want to wrap the responses from the server in a custom response object. By default, responses are not wrapped.

#### `wrapResponseMethods`

- Type: array
- Default: []

Specifies an array of method names to be wrapped in custom response objects. By default, no methods are wrapped.

#### `generateResponseClasses`

- Type: boolean
- Default: true

Set this option to false if you do not want to generate response classes for the API client. By default, response classes are generated.

#### `responseClass`

- Type: string
- Default: "SwaggerResponse"

Specifies the name of the base response class to be used for the generated response classes. If this option is not set, the generated response classes will inherit from the default SwaggerResponse class.

#### `namespace`

- Type: string
- Default: "MyNamespace"

Specifies the namespace to be used for the generated API client code.

#### `requiredPropertiesMustBeDefined`

- Type: boolean
- Default: true

Set this option to false if you want to allow models with required properties that are not defined in the OpenAPI schema. By default, models with required properties that are not defined in the schema will cause a build error.

#### `dateType`

- Type: string
- Default: "System.DateTimeOffset"

Specifies the type to be used for date values in the generated code. By default, the System.DateTimeOffset type is used.

#### `jsonConverters`

- Type: array
- Default: null

Specifies an array of custom JsonConverter instances to be used for JSON serialization and deserialization.

#### `anyType`

- Type: string
- Default: "object"

Specifies the type to be used for any values in the generated code. By default, the object type is used.

#### `dateTimeType`

- Type: string
- Default: "System.DateTimeOffset"

Specifies the type to be used for DateTime values in the generated code. By default, the System.DateTimeOffset type is used.

#### `timeType`

- Type: string
- Default: "System.TimeSpan"

Specifies the type to be used for TimeSpan values in the generated code. By default, the System.TimeSpan type is used.

#### `timeSpanType`

- Type: string
- Default: "System.TimeSpan"

Specifies the type to be used for TimeSpan values in the generated code. By default, the System.TimeSpan type is used.

#### `arrayType`

- Type: string
- Default: "System.Collections.Generic.ICollection"

Specifies the type to be used for arrays in the generated code. By default, the System.Collections.Generic.ICollection type is used.

#### `arrayInstanceType`

- Type: string
- Default: "System.Collections.ObjectModel.Collection"

Specifies the instance type to be used for arrays in the generated code. By default, the System.Collections.ObjectModel.Collection type is used.

#### `dictionaryType`

- Type: string
- Default: "System.Collections.Generic.IDictionary"

Specifies the type to be used for dictionaries in the generated code. By default, the System.Collections.Generic.IDictionary type is used.

#### `dictionaryInstanceType`

- Type: string
- Default: "System.Collections.Generic.Dictionary"

Specifies the instance type to be used for dictionaries in the generated code. By default, the System.Collections.Generic.Dictionary type is used.

#### `arrayBaseType`

- Type: string
- Default: "System.Collections.ObjectModel.Collection"

Specifies the base type to be used for arrays in the generated code. By default, the System.Collections.ObjectModel.Collection type is used.

#### `dictionaryBaseType`

- Type: string
- Default: "System.Collections.Generic.Dictionary"

Specifies the base type to be used for dictionaries in the generated code. By default, the System.Collections.Generic.Dictionary type is used.

#### `classStyle`

- Type: string
- Default: "Poco"

Specifies the style to be used for the generated classes. By default, the POCO (Plain Old CLR Object) style is used.

#### `jsonLibrary`

- Type: string
- Default: "NewtonsoftJson"

Specifies the JSON library to be used for JSON serialization and deserialization. By default, the Newtonsoft.Json library is used.

#### `generateDefaultValues`

- Type: boolean
- Default: true

Set this option to false if you do not want to generate default values for the properties in the generated models. By default, default values are generated.

#### `generateDataAnnotations`

- Type: boolean
- Default: true

Set this option to false if you do not want to generate data annotations for the properties in the generated models. By default, data annotations are generated.

#### `excludedTypeNames`

- Type: array
- Default: []

Specifies an array of type names to be excluded from the generated code.

#### `excludedParameterNames`

- Type: array
- Default: []

Specifies an array of parameter names to be excluded from the generated client methods.

#### `handleReferences`

- Type: boolean
- Default: false

Set this option to true if you want the API client to handle references in the OpenAPI schema. By default, references are not handled.

#### `generateImmutableArrayProperties`

- Type: boolean
- Default: false

Set this option to true if you want the API client to generate immutable array properties for the generated models. By default, mutable array properties are generated.

#### `generateImmutableDictionaryProperties`

- Type: boolean
- Default: false

Set this option to true if you want the API client to generate immutable dictionary properties for the generated models. By default, mutable dictionary properties are generated.

#### `jsonSerializerSettingsTransformationMethod`

- Type: string
- Default: null

Specifies the method to be used for transforming the JsonSerializerSettings instance used for JSON serialization and deserialization.

#### `inlineNamedArrays`

- Type: boolean
- Default: false

Set this option to true if you want the API client to inline named arrays in the generated code. By default, named arrays are not inlined.

#### `inlineNamedDictionaries`

- Type: boolean
- Default: false

Set this option to true if you want the API client to inline named dictionaries in the generated code. By default, named dictionaries are not inlined.

#### `inlineNamedTuples`

- Type: boolean
- Default: true

Set this option to false if you do not want the API client to inline named tuples in the generated code. By default, named tuples are inlined.

#### `inlineNamedAny`

- Type: boolean
- Default: false

Set this option to true if you want the API client to inline named any values in the generated code. By default, named any values are not inlined.

#### `generateDtoTypes`

- Type: boolean
- Default: true

Set this option to false if you do not want to generate DTO (Data Transfer Object) types for the models in the generated code. By default, DTO types are generated.

#### `generateOptionalPropertiesAsNullable`

- Type: boolean
- Default: false

Set this option to true if you want to generate optional properties as nullable in the generated models. By default, optional properties are not generated as nullable.

#### `generateNullableReferenceTypes`

- Type: boolean
- Default: false

Set this option to true if you want to generate nullable reference types in the generated code. By default, nullable reference types are not generated.

#### `templateDirectory`

- Type: string
- Default: null

Specifies the directory to be used for custom templates for the generated code.

#### `typeNameGeneratorType`

- Type: string
- Default: null

Specifies the type to be used for generating type names in the generated code.

#### `propertyNameGeneratorType`

- Type: string
- Default: null

Specifies the type to be used for generating property names in the generated code.

#### `enumNameGeneratorType`

- Type: string
- Default: null

Specifies the type to be used for generating enum names in the generated code.

#### `serviceHost`

- Type: string
- Default: null

Specifies the base URL for the API service.

#### `serviceSchemes`

- Type: array
- Default: null

Specifies the URL schemes to be used for the API service.

#### `output`

- Type: string
- Default: null

Specifies the output file path for the generated code. If this option is not set, the generated code will be output to the console.

#### `newLineBehavior`

- Type: string
- Default: "Auto"

Specifies the behavior for new line characters in the generated code. By default, the behavior is set to Auto.
