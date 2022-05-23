using System.Collections.Generic;
Dictionary<string, Dictionary<string, int>[]> graph = new();
Dictionary<string, int> fromStartToA = new();
fromStartToA.Add("A", 6);
Dictionary<string, int> fromStartToB = new();
fromStartToB.Add("B", 2);
Dictionary<string, int>[] fromStart = { fromStartToA, fromStartToB };
graph.Add("start", fromStart);
Dictionary<string, int> fromAToFinish = new();
fromAToFinish.Add("finish", 1);
Dictionary<string, int>[] fromA = { fromAToFinish };
graph.Add("A", fromA);
Dictionary<string, int> fromBToA = new();
fromBToA.Add("A", 3);
Dictionary<string, int> fromBToFinish = new();
fromBToFinish.Add("finish", 5);
Dictionary<string, int>[] fromB = { fromBToA, fromBToFinish };
graph.Add("B", fromB);
Dictionary<string, string> parents = new();
parents.Add("A", "start");
parents.Add("B", "start");
Dictionary<string, int> costs = new();
costs.Add("A", 6);
costs.Add("B", 2);
costs.Add("finish", 999);
Dictionary<string, int> costs1 = new();
costs1.Add("A", 6);
costs1.Add("B", 2);
costs1.Add("finish", 999);
Dictionary<string, int> costs2 = new();
costs2.Add("A", 6);
costs2.Add("B", 2);

Dictionary<string, int>.ValueCollection costsValues = costs1.Values;
int min = 999;
Dictionary<string, int>.KeyCollection allNots = costs2.Keys;
foreach (string k in allNots)
{
    min = 999;
    foreach (int i in costsValues)
    {
        if (i <= min)
            min = i;
    }
    string nearestNot = costs1.FirstOrDefault(x => x.Value == min).Key;

    foreach (Dictionary<string, int> l in graph[nearestNot])
    {
        int newRoute = 0;
        foreach (KeyValuePair<string, int> kvp in l)
        {
            newRoute = min + kvp.Value;
            if (costs1[kvp.Key] > newRoute)
            {
                costs1[kvp.Key] = newRoute;
                Dictionary<string, string>.KeyCollection parentsKeys = parents.Keys;
                if (parentsKeys.Contains(kvp.Key))
                {
                    parents[kvp.Key] = nearestNot;
                }
                else
                {
                    parents.Add(kvp.Key.ToString(), nearestNot);
                }
            }
        }

    }
    costs1.Remove(nearestNot);
}
string a = "finish";
Console.Write(a + " - ");
for (int i = 0; i < parents.Count; i++)
{
    string previous = parents[a];
    a = previous;
    Console.Write(a + " - ");
}
Console.WriteLine();
Console.WriteLine(costs1["finish"]);