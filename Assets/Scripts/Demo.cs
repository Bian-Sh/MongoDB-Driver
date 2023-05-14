using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.ObjectModel;
// foo  ˝æ›ø‚’À∫≈√‹¬Î
// unityuser
// Unity_2023
public class Demo : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("foo");
        var collection = database.GetCollection<BsonDocument>("bar");

        await collection.InsertOneAsync(new BsonDocument("Name", "Jack"));

        var list = await collection.Find(new BsonDocument("Name", "Jack"))
            .ToListAsync();

        foreach (var document in list)
        {
            Debug.Log($"{nameof(Demo)}:  document  col of Name:   {document["Name"]}");
        }


        var collection2 = database.GetCollection<Person>("bar");

        await collection2.InsertOneAsync(new Person { Name = "Jack" });

        var list2 = await collection2.Find(x => x.Name == "Jack")
            .ToListAsync();

        foreach (var person in list2)
        {
            Debug.Log($"{nameof(Demo)}: person name: {person.Name}");
        }
    }
}
public class Person
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
}