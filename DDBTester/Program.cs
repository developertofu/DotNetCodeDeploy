/*******************************************************************************
* Copyright 2009-2013 Amazon.com, Inc. or its affiliates. All Rights Reserved.
* 
* Licensed under the Apache License, Version 2.0 (the "License"). You may
* not use this file except in compliance with the License. A copy of the
* License is located at
* 
* http://aws.amazon.com/apache2.0/
* 
* or in the "license" file accompanying this file. This file is
* distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
* KIND, either express or implied. See the License for the specific
* language governing permissions and limitations under the License.
*******************************************************************************/

using System;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;

namespace DDBTester
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Setting up DynamoDB client");

            AmazonDynamoDBClient client = new AmazonDynamoDBClient();

            //CreateTablesWithData(client);
            //DefaultConcurrencyBehavior(client);
            ConditionalWriteSample(client);
        }

        private static void ConditionalWriteSample(AmazonDynamoDBClient client)
        {
            Table table = Table.LoadTable(client, "Actors");
            Document d = table.GetItem("Christian Bale");

            int version = d["Version"].AsInt();
            double height = d["Height"].AsDouble();
            Console.WriteLine("Retrieved Item Version #" + version.ToString());

            var request = new UpdateItemRequest
            {
                Key = new Dictionary<string, AttributeValue>() { { "Name", new AttributeValue { S = "Christian Bale" } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#H", "Height"},
                    {"#V", "Version"}
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":ht", new AttributeValue{N=(height+.01).ToString()}},
                    {":incr", new AttributeValue{N="1"}},
                    {":v", new AttributeValue{N=version.ToString()}}
                },
                UpdateExpression = "SET #V = #V + :incr, #H = :ht",
                ConditionExpression = "#V = :v",
                TableName = "Actors"
            };

            try
            {
                Console.ReadKey();
                var response = client.UpdateItem(request);
                Console.WriteLine("Updated succeeded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update failed. " + ex.Message);
            }

            Console.ReadKey();
        }

        private static void DefaultConcurrencyBehavior(AmazonDynamoDBClient client)
        {
            Table table = Table.LoadTable(client, "Actors");
            Document d = table.GetItem("Christian Bale");

            Document actor = new Document();
            actor["Name"] = d["Name"].AsString();
            actor["Height"] = d["Height"].AsDouble() + .01;

            Console.WriteLine("Freeze before update occurs.");
            Console.ReadKey();
            try
            {
                table.UpdateItem(actor);
                Console.WriteLine("Updated succeeded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update failed. " + ex.Message);
            }

            Console.ReadKey();
        }

        private static void CreateTablesWithData(AmazonDynamoDBClient client)
        {
            Console.WriteLine();
            Console.WriteLine("Creating sample tables");
            TableOperations.CreateSampleTables(client);

            Console.WriteLine();
            Console.WriteLine("Creating the context object");
            DynamoDBContext context = new DynamoDBContext(client);

            Console.WriteLine();
            Console.WriteLine("Running DataModel sample");
            RunDataModelSample(context);
        }
    }
}