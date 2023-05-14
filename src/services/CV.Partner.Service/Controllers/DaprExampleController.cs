// ***********************************************************************
// Assembly         : CV.Partner.Service
// Author           : matias.ramsland
// Created          : 03-10-2023

// Last Modified By : matias.ramsland
// Last Modified On : 03-14-2023
// ***********************************************************************
// <copyright file="DaprExampleController.cs" company="CV.Partner.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
// using System.Text.Json;
// using CV.Partner.Service.Models;
// using Dapr;
// using Dapr.Client;
// using fields.Entities.Base.Fields;
// using fields.Factories;
// using libraries.Contracts.Bouvet.Rule.Engine.Service;
// using libraries.Extentions.BRulesEngine.Rules.BuisnessRules;
// using libraries.Extentions.BRulesEngine.Rules.Core;
// using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
// using libraries.Extentions.BRulesEngine.Rules.Core.Fields;
// using libraries.Extentions.DotNetCore.Communication.Dapr;
// using libraries.Extentions.DotNetCore.ExceptionHandling;
// using libraries.Extentions.MeditR.Console.CommandAndHandlers;
// using Microsoft.AspNetCore.Mvc;

// namespace CV.Partner.Service.Controllers {
//   / <summary>
//   / Class ExecuteWorkflowController.
//   / Implements the <see cref="ControllerBase" />
//   / </summary>
//   / <seealso cref="ControllerBase" />
//   [Route("api/")]
//   [ApiController]
//   public class DaprExampleController : ControllerBase {
//     private const string DAPR_PUBSUB_NAME = "ruleengine-pubsub";
//     private const string DAPR_PUBSUB_TOPIC = "WorkflowFinishedEvent";

//     / <summary>
//     / The logger
//     / </summary>
//     private readonly ILogger<DaprExampleController> _logger;
//     / <summary>
//     / The dapr client
//     / </summary>
//     private readonly DaprClient _daprClient;
//     / <summary>
//     / Initializes a new instance of the <see cref="DaprExampleController"/> class.
//     / </summary>
//     / <param name="logger">The logger.</param>
//     / <param name="daprClient">The dapr client.</param>
//     public DaprExampleController(
//     ILogger<DaprExampleController> logger,
//     DaprClient daprClient) {
//       _logger = logger;
//       _daprClient = daprClient;
//     }
//     / <summary>
//     / Executes the workflow.
//     / </summary>
//     / <returns>ActionResult&lt;PipelineResult&gt;.</returns>
//     [HttpPost("invoke")]
//     public async Task<IActionResult> Invoke() {
//       var rule = new NotNull();
//       var ExecuteWorkflowCommandContract = new ExecuteWorkflowCommandContract(RuleEngineFactory.CreateWorkflow(new List<BRule> { rule.Rule }, "ruleengine"), new InputMessage(FieldsFactory.StringField("http://localhost:3500/v1.0/invoke/bouvet-cv-service/method/api/getcv"), new GetCVQueryContract(FieldsFactory.StringField("TEST"))));
//       _logger.LogInformation(JsonSerializer.Serialize(ExecuteWorkflowCommandContract));

//       var request = _daprClient.CreateInvokeMethodRequest<ExecuteWorkflowCommandContract>(HttpMethod.Post, "bouvet-rule-engine-service", "api/executeworkflow", ExecuteWorkflowCommandContract);
//       var result = await _daprClient.InvokeMethodAsync<object>(request);
//       _logger.LogInformation(result.ToString());
//       return Ok(result);
//     }
//     Rule engine is the one handles this request
//     / <summary>
//     / Publishes this instance.
//     / </summary>
//     / <returns>IActionResult.</returns>
//     [HttpPost("WorkflowFinishedEvent")]
//     [Topic(DAPR_PUBSUB_NAME, DAPR_PUBSUB_TOPIC)]
//     public void SubscribeRuleEngine(WorkflowFinishedEvent @event) {
//       _logger.LogCritical("Event: {EventName} received", typeof(WorkflowFinishedEvent).Name);
//       _logger.LogCritical(JsonSerializer.Serialize(@event));
//     }
//     / <summary>
//     / Saves to state.
//     / </summary>
//     / <returns>IActionResult.</returns>
//     [HttpPost("saveToStateCV")]
//     public async Task<IActionResult> SaveToStateCV() {
//       var stateObject = new DaprStateObject();
//       stateObject.Description = FieldsFactory.StringField("Some CV date" + Guid.NewGuid().ToString());
//       stateObject.Author = FieldsFactory.StringField("Employee" + Guid.NewGuid().ToString());
//       await _daprClient.SaveStateAsync("ruleengine-statestore-cv", stateObject.Id.ToString(), stateObject);
//       return Ok(stateObject);
//     }
//     / <summary>
//     / Gets the state.
//     / </summary>
//     / <param name="id">The identifier.</param>
//     / <returns>IActionResult.</returns>
//     [HttpGet("getStateCV")]
//     public async Task<IActionResult> GetStateCV(string id) {
//       var stateObject = await _daprClient.GetStateAsync<object>("ruleengine-statestore-cv", id);
//       if (stateObject == null) {
//         return BadRequest();
//       }
//       return Ok(stateObject);
//     }

//     [HttpGet("getWorkflowResult")]
//     public async Task<IActionResult> GetWorkflowResult(string id) {
//       var contract = new GetWorkflowResultQueryContract(FieldsFactory.StringField(id));
//       var workflowResult = await _daprClient.InvokeMethodAsync<GetWorkflowResultQueryContract, OperationResult>("bouvet-rule-engine-service", "api/getworkflowresult", contract);
//       return Ok(workflowResult.Value);
//     }
//     / <summary>
//     / Gets the redis secret.
//     / </summary>
//     / <returns>IActionResult.</returns>
//     [HttpGet("getRedisSecret")]
//     public IActionResult GetRedisSecret() {
//       var secret = _daprClient.GetBulkSecretAsync("ruleengine-secretstore").Result;
//       if (secret == null) {
//         return BadRequest();
//       }
//       return Ok(secret);
//     }
//   }
// }

