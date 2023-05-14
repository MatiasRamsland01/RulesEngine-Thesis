# Epics

- Create Bouvet.CV.Service service
- Create Bouvet.RuleEngine.Service service
- Create Bouvet.Notification.Service service
- Create Bouvet.Mitm2Client.Service service
- Create Dapr Configuration
- Create Fields Library
- Create RuleEngine Library

## Features

## Templates

1. Requirement (Functional or Non-Functional)
    1. User story: Story points
       1. Task:  Story Points
       2. Task:  Story Points
       3. Task:  Story Points

### What is functional requirements

- Calculations, performed by the system
- Data processing and consumption
- Use cases for the system

### What is non-functional requirements

- Easy to interact with a product
- Legal or regulatory requirements
- Product's vulnerable points and solutions
- Speed of reaction

## Functional Requirements

1. It should be possible to execute a workflow
   1. As a user I want a system that can run a workflow to ensure a given standard on the cv.
      1. Create workflow class
      2. Create rules class
      3. Crease generic ruleengine that can execute workflows with given input
   2. As a user I want to execute workflow on any input because the ruleengine migth be needed for multiple purposes
      1. Expand ruleengine to be able to run on any given input
2. It should be able to dynamically fetch data from websites.
   1. As a user I want to get data even if a website changes their structure because website structure may change in the future.
      1. Use a proxy service to store the response from an api
      2. Use a script that sends requests to a given website through the proxy
      3. Use a program to extract the open api 3.0 specification from the response
      4. Create a script which extracts all the components in the open api 3.0 specification
      5. Use a program which generates an http client from the open api 3.0 specification
      6. Automate all of the given steps in a containerized manner
3. It should be possible to get workflow result
   1. As a user I want to retrieve workflow result because other services might be interested in it.
      1. Create an endpoint where user can get workflow result with given ID
   2. As a user I want detailed information about the result because it is given.
      1. Create a resultclass that generates a quality result for the user
4. It should be possible to notify other services when workflow execution is completed.
   1. As a user I want to notify other services because other services depends on the workflow execution.
      1. Set up application to use DAPR pub/sub using rabbitMQ
      2. When workflow execution is finished send an event(WorkflowExecutionCompleted) to the eventbus
5. It should be possible to notify other services when workflow execution has started.
   1. As a user I want to notify other services because for other services this information migth be necessary.
      1. When workflow execution has started send an event(WorkflowExecutionStarted) to the eventbus
6. It should be possible to integrate with other notification services.
   1. As a user I want to be able to notify employees in my organization with the organization's own communication mediums because the employees should be notified about their info.
      1. Create connection to an Azure eventbus that handles the notification and distribute it to the receivers.
7. It should be able to persist data
   1. As a user I want to store cv in a database because I want historical data of cv.
      1. Create document database (MongoDB)
      2. Connect DAPR statestore to database
      3. Create functionality to store multiple cv's with the same key in database({ key=email, value=[]listOfCvs} )
      4. Create functionality to save cv to database if cv do not exist or cv that exist have been updated
   2. As a user I want to see if and when a cv has changed because I want to know if an employee changes the cv and when it was changed.
      1. Create functionality to see if a cv has been changed in regards to the cv that was before(See delta between two cv's)
   3. As a user I want to store the workflow result in a database because I might want to access it later.
      1. Create document database for storing workflow result(MongoDB)
      2. Connect DAPR statestore to this database
      3. Functionality to store workflow result execution in database
   4. As a user I want store the rules in a database for easier access, such that I do not need to write new rules every time.
      1. Create relational database for storing rules(SQL)
      2. Create tables for database (Rules, Workflows and RuledataField) and the relationships between them
      3. Connect database to SQL-server using DAPR secret store
      4. Connect database context to Microsoft Entity Framework
8. It should be possible to modify a workflow
   1. As a user I want to add new rules because of different needs in the future and different needs for different workflows
      1. Create endpoint that saves a new rule to the ruleengine
   2. As a user I want to be able to modify a rule because the rules may need to be modified for different kind of cv checks. And it may need to be more effective to modify an existing rule instead of making a new one.
      1. Create endpoint that modifies a rule in the ruleengine service
   3. As a user I want to be able to delete a rule because I migth not need a specific rule anymore and I do not want to keep it cluttering up all the other rules if I no longer need it.
      1. Create endpoint that deletes a rule in the ruleengine service
9. The program should come with given business rules.
   1. As a business unit manager I want to be able to check wether a cv has been updated in the last three months because I want to maintain an updated standard on the cv.
      1. Create a rule that checks the last_updated field in the cv with the current date
   2. As a business unit manager I want to check that the email address is valid because valid email is important from a business perspective.
      1. Create a rule that the email address is a valid organization email address(should have @organization.prefix)
   3. As a business unit manager I want the cv to contain a summary longer that x words because a summary should not be shorter than x words to properly describe an employee.
      1. Create a rule that checks that the summary text of a cv is longer than x words long.
   4. As a business unit manager I want the cv to contain a summary which have good grammar/spell check check because good grammar/spell check check is important for professionals and our organizations customers.
      1. Create a rule that checks the grammar/spell check in the summary section
      2. Create a spell checker/grammar checker or use external program or api
   5. As a business unit manager I want the cv to contain the eduction of the employee because it is important to the organizations customers to know the eduction of the organizations employees.
      1. Create a rule that verifies the education of employees using a program that contains valid universities and schools
   6. As a business unit manager I want to check that the qualifications are accepted by the organization because there should only be valid qualifications.
      1. Create a rule that verifies that all qualifications are valid to the organization
   7. As a business unit manager I want the cv to contain a profile picture of the employee because the organizations customer should be able to see what the employee looks like.
      1. Create a program that checks if there exists a picture in the cv.
10. It should have restricted access
    1. As a business unit manager I want only access for authorized users to use program because unauthorized personell should not change the program.
       1. Implement RBAC (Role-Based Access Control)
11. It should use a library containing reusable code
    1. As a user I want to be able to reuse code in other programs because it could save time and be more efficient in future projects.
       1. Create Stringfield
       2. Create Integerfield
       3. Create Charfield
       4. Create Datefield
       5. Create DateRangeField
       6. Create Idfield
       7. Create Namefield
       8. Create Boolfield
       9. Create Agefield
       10. Create Limitfield
    2. As a user I want to be able to get the reusable code from a nuget package
       1. Get organization permission to publish a nuget package to organization github
       2. Publish the library as a nuget package such that other developers can reuse the code
       3. Test package
       4. Create documentation
12. It should be possible configure the application using a configuration file at runtime
    1. As a user I want to be able to configure the different services in the application without stopping the application
        1. Set up configuration for application
           1. Configure configuration for Rule Engine Service and its Dapr sidecar
           2. Configure configuration for CV Service and its Dapr sidecar
           3. Configure configuration for Notification Service and its Dapr sidecar

## Non-Functional

1. It should be possible to track a bug using a logger.
    1. As a service I want to statically validate ExecuteWorkflowCommand.cs, because it is given, because it benefits utilizing hardware and reduce cost
       1. Create validator for ExecuteWorkflowCommand
       2. Create integration test for validator
       3. Create documentation for validator
2. Should be possible to set logging level dynamically(Option Pattern++)
    1. As a service I want to be able to set configure the logging level dynamically using a configuration file because I migth need various logging levels for various occasions of debug or running the program.
       1. Create configurationfile which contains the various configuration for different logging levels
       2. Create functionality for the services to use the logging configuration file
       3. Create tests for logging
       4. Create documentation for logging
3. Should have functionality for distributed logging
   1. As a service I want to be able to monitor different activities because I want to have an overview of what is happening in the application and its flow.
      1. Create connection between Dapr and Zipkin
      2. Set up Zipkin
4. Set limit on logging stored on disk
    1. As a service I need a limit of logging that is stored on disk because otherwise the disk may become full and cause instability, issues and it saves space on disk.
       1. Configure logging service to have a maximum storage size
       2. Create functionality for the logging to overwrite old logging if disk space becomes full
5. Should implement a chain of responsibility pattern for executing of workflow
    1. As a service I want to split up as much of the logic as possible because I want to adhere to the single responsibility pattern.
       1. Create logging pipeline behaviour
       2. Create validation pipeline behaviour
       3. Create ExecuteWorkFlowCommandHandler pipeline behaviour
       4. Create tests for logging pipeline behaviour
       5. Create tests for validation pipeline behaviour
       6. Create tests for ExecuteWorkFlowCommandHandler
       7. Create documentation for the above
6. Should implement a background service that implements the rule-engine.
   1. As a service I want to run workflows on another thread because then I can focus on multiple different tasks at once.
      1. Create channel queue
      2. Create hosted service that implements the rule engine
      3. Create method in hosted service that executes tasks in the queue
      4. Write tests on the channel queue
      5. Write tests on the hosted service
      6. Write tests on the method in hosted service that executes tasks in the queue
      7. Document the above
7. Should implement a background service that fetches cv's on a given time interval
   1. As a service I want to fetch cv's on regular time interval because the cv's will be controlled on a fixed interval of time.
      1. Create background service that uses a http client at a given schedule
      2. Create test for http client background service
      3. Write documentation
      4. Store validation for cv partner requests in key store
      5. Get API key for cv-partner.
      6. Create functionality that limits the requests to CV Partner
8. It should be General Data Protection Regulation(GDPR) compliant
   1. As an application I want to comply to the GDPR guidelines, because I want to follow todays security standards.
      1. Create HTTPS connection between all services and Dapr
      2. Limit the storage of user data to a minimum
      3. Remove storage of employee data if employee quits the organization
      4. Do not store user data longer than necessary.
9. It should connect to Azure eventbus during production
   1. As a service I want to connect to Azure eventbus because the service should be able to publish events to other external subscribers
      1. Extend notification service functionality to be able to publish external events
      2. Create documentation for notification service
      3. Create tests for notification external service
10. It should use a secret store
    1. As a service I want to use Dapr secret store my secrets in a safe place
       1. Implement Dapr secret store
          1. In production:
             - Connect to Azure keyvault
          2. In development
             - Use a JSON file
       2. Create functionality for the app to use Dapr secret store
          1. Set up connection-strings
          2. Set up API-keys/cookies
11. It should follow microservices design
    1. As a service I want to communicate with other services with using Dapr because Dapr abstract away the complexity of building distributed applications.
       1. Configure application to use Dapr
          1. Configure Dapr for Rule Engine Service
          2. Configure Dapr for CV Service
          3. Configure Dapr for Notification Service
          4. Configure Dapr for Zipkin Service
          5. Configure Dapr for MongoDB Service
          6. Configure Dapr for Seq Service
12. It should be containerized
    1. As a service I want to run in a containerized environment because containerized environments are consistent, portable, efficient and secure.
        1. Configure Docker compose
           1. Configure Dockerfile for Rule Engine Service
           2. Configure Dockerfile for CV Service
           3. Configure Dockerfile for Notification Service
           4. Configure Dockerfile for Zipkin Service
           5. Configure Dockerfile for MongoDB Service
           6. Configure Dockerfile for Seq Service
           7. Set up container network
           8. Configure ports
           9. Configure shared volume
           10. Configure network
13. It should be developed using Continous Integration/Continous Deployment(CI/CD)
    1. As a service I want to use Github Actions because it automate tasks such as building, testing, and deploying applications.
       1. Create workflow to run unit tests and build the application
14. It should implement health check on services and Dapr sidecars
    1. As a service I want to be able to check the health of services and sidecars running in the application because I want to be notified if one or more of the services or sidecars have problems.
       1. Configure health check
           1. Configure health check for Rule Engine Service and its Dapr sidecar
           2. Configure health check for CV Service and its Dapr sidecar
           3. Configure health check for Notification Service and its Dapr sidecar
           4. Configure health check for Zipkin Service and its Dapr sidecar
           5. Configure health check for MongoDB Service and its Dapr sidecar
           6. Configure health check for Seq Service and its Dapr sidecar
