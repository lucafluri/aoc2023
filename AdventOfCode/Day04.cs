﻿using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string[] _input;
    
    private int total = 0;
    
    
    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath);
    }
    
    

    public override ValueTask<string> Solve_1()
    {
        foreach (var line in _input)
        {
            string inp = line;
            inp = Regex.Replace(inp, @"\s+", " ");
            Regex rgx = new Regex(".*:(.*)\\|(.*)");
            var match = rgx.Match(inp);
            List<int> nums = new List<int>();
            var l1 = match.Groups[1].ToString().Trim().Split(" ").ToList();
            var l2 = match.Groups[2].ToString().Trim().Split(" ").ToList();
            
            
            var l3 = l1.Intersect(l2);
            if(l3.Any())
                total += (int) Math.Floor(Math.Pow(2, l3.Count()-1));    
            // nums = match.Groups[1].ToString().Trim().Split(" ").ToList();
        }
       
        return new ValueTask<string>(total.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        total = 0;
        var idx = 0;
        var cards = new int[300];
        var q = new Queue<int>();
        var m = new Dictionary<int, List<int>>();
        
        //Save into map
        foreach (var line in _input)
        {
            idx++;
            
            string inp = line;
            inp = Regex.Replace(inp, @"\s+", " ");
            Regex rgx = new Regex(".*:(.*)\\|(.*)");
            var match = rgx.Match(inp);
            var l1 = match.Groups[1].ToString().Trim().Split(" ").ToList();
            var l2 = match.Groups[2].ToString().Trim().Split(" ").ToList();
            
            var l3 = l1.Intersect(l2);
            var l3Ints = l3.Select(int.Parse).ToList();
            //each num from idx to idx + l3Ints.Count() as list
            var count = l3.Count();
            var wins = new List<int>();
            for(var i = idx+1; i <= idx + count; i++)
            {
                wins.Add(i);
            }
            
            m.Add(idx, wins);
            q.Enqueue(idx);
        }
        
        
        int current;
        while (q.Any())
        {
            current = q.Dequeue(); 
            cards[current]++;
            
            //Iterate over all won cards 
            foreach(var i in m[current])
            {
                q.Enqueue(i);
            }
        }
        

        total = cards.Sum();
        
        return new ValueTask<string>(total.ToString());
    } 
}