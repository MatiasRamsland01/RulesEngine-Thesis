# API - Bouvet CV Service

## Define Bouvet CV schema

CV Partner is an external system with its own data structure. Bouvet need its own schema

## GetCv

- Specification:
Should provide data structure consistent to Bouvet CV schema (Bouvet CV schema is a part of the scope of this Thesis)
- Why:
<!--https://mermaid.js.org/config/theming.html#sequence-diagram-variables-->
```mermaid
%%{
  init: {
    'theme': 'base',
    'themeVariables': {
      'primaryColor': '#BB2528',
      'primaryTextColor': '#fff',
      'primaryBorderColor': '#7C0000',
      'lineColor': '#F8B229',
      'secondaryColor': '#006100',
      'tertiaryColor': '#fff',
      'labelTextColor': '#F11',
      'actorTextColor': '#F8F'
    }
  }
}%%
  sequenceDiagram
    ExternSystem->>Api: GetCv(employee id) Web Request
    rect rgb(100, 0, 100)
    note right of Api: Internal processing in API service
        activate Handler
        Api->>Handler: Handle GetCV request
        Handler->>Dapr: Redirect request
    end
    Dapr->>CvPartner: Redirect request to CV Partner
    CvPartner-->>Dapr: Response
    rect rgb(100, 0, 100)
    Dapr-->>Handler: Process Response
    deactivate Handler
    activate Handler
        Handler-->>Api: Processed Response
    deactivate Handler
    end
    Api-->>ExternSystem: Processed Response
```

## Persist Bouvet CV

- Specification:
Should persist if CV have changes since last persisted date.
Should persist if CV is newly created
- Why:
To be able to query faster and without any restrictions (e.g. how many request per time interval)

```mermaid
  sequenceDiagram
  activate Consumer
  Consumer->>Handler: GetCV
  activate Handler
  rect rgb(100, 0, 100)
  Handler->>Handler: Cache?
  Handler->>CVPartner: GetCV
  end
  CVPartner->>Handler: Response
    activate Handler
        Handler->>Handler: Persist
    deactivate Handler
    activate Handler
    Handler->>TimeSerieDb: Persist into time-series database
    deactivate Handler
    Handler-->>Consumer: Processed Response (Output)
    deactivate Handler
deactivate Consumer
```

## Persist Bouvet CV in time series Database
