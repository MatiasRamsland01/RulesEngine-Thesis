# Rule-engine Service

## Commands

- One workflow per cv

## Queries

- Create
- Read
- Update
- Delete

OR

## NOT

Let user define rules?

Do not let rule-engine be a "god" of the services.

Two endpoints

- Execute(GET).
- Query.

Rule-engine micro-service should validate the rules.
CQRS(Non-functional requirement)

## Events

- Subscribe
- Publish
  - In queue
  - Workflow process started
  - Workflow process ended(WorkflowId, Outcome)

# Bouvet CV Service

## Commands

- Create rule
- Update rule
- (Disable rule)

## Queries

- Fetch CV's from cv-partner
- Run when configured(Scheduler)
- Get rule(s)

## Events

- Publish
  - New CV fetched to store

CV Partner should be its own microservice.

# Notifikasjons service

## Features

1. Store key/value options about failures to update cv
   - key: email
   - value: {dayssincenotified: int, numerrors: int, list[] errors}
2. Connect to MQ
3. Share contracts with X
4. Listen to rule-engine execution finished

## Events

- Subscribe
  - Workflow process ended(WorkflowId, Outcome)
- Publish

## MQ

- Subscribe
  - CvCorrections
- Publish
  - CvErrors

## Commands

## Queries

- Fetch cv result from state
