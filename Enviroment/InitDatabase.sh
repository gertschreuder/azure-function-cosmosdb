#!/bin/bash

# Set variables for the new SQL API account, database, and container
resourceGroupName='[Enter ResourceGroupName Here]'
location='eastasia'
accountName='[Eneter AccountName Here]' #needs to be lower case
databaseName='DemoQuest'
containerQuestProgress='QuestProgress'


# Create a resource group
az group create \
    --name $resourceGroupName \
    --location $location


# Create a SQL API Cosmos DB account with session consistency and multi-master enabled
az cosmosdb create \
    --resource-group $resourceGroupName \
    --name $accountName \
    --kind GlobalDocumentDB \
    --locations "East Asia"=0 "China East"=1 \
    --default-consistency-level "Session" \
    --enable-multiple-write-locations true


# Create a database
az cosmosdb database create \
    --resource-group $resourceGroupName \
    --name $accountName \
    --db-name $databaseName


# Create a SQL API containers with a partition key and 1000 RU/s
az cosmosdb collection create \
    --resource-group $resourceGroupName \
    --collection-name $containerQuestProgress \
    --name $accountName \
    --db-name $databaseName \
    --partition-key-path /mypartitionkey \
    --throughput 1000