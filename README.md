# Game Quest Engine
## Getting Started
* Restore all missing nuget packages via VS and rebuild solution
* Install [Azure Cosmos DB Emulator](https://aka.ms/cosmosdb-emulator)
    * Ensure that the local connection details is the same in the config files
* Install [Postman](https://www.getpostman.com/)
    * Run InitializeCosmosDB test to initialize the database and table in emulator
    * Open the files in AzureFunction.WS.Tests/Postman folder in Postman
    * Run the AzureFunction.WS project
    * Once up and running the end points can be tested via Postman
* Alternatively there are Unit Tests to run (I use Resharper & dotCover to run test and check code coverage) 

## Configuration
### Location: AzureFunction.WS\local.settings.json & AzureFunction.WS.Tests\app.config
* QuestRateFromBet: Used in quest point formula.
* QuestLevelBonusRate: Used in quest point formula.
* TotalQuestPointsNeeded: The total quest points needed to complete a quest
* AmountQuestMilestones: Quests can have n amount of milestones.
* MilestoneCompletionChipAward: Milestone completion can award the player chips.

## Further Considerations
* Quest variables in local.settings.json should be setup as enviroment variables in Azure Function to be managed with easy in every enviroment(Dev,QA,Prod etc)
* Setup deployment pipleline from source control. It would be gated deployments through various enviroments (Dev,QA,Prod etc) via Git, TFS and Azure Devops.
* SSL setup
* Authentication Provider (EasyAuth)
* The Enviroment folder contains a bash script to setup the database and table required - this would be adisable for deployment pipeline
* An Azure template should also be included here to automate the creation of the Serverless Enviroment(App Service Plan, Resource Group etc). This would make deploying to different enviroments (Dev,QA,Prod etc) seemless.