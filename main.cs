//http://www.infojegyzet.hu/vizsgafeladatok/okj-programozas/szoftverfejleszto-181002/
//﻿év;típus;keresztnév;vezetéknév
//2017;fizikai;Rainer;Weiss
//2017;béke;International Campaign to Abolish Nuclear Weapons (ICAN);

using System;                     // Console
using System.IO;                  // StreamReader
using System.Text;                // Encoding
using System.Collections.Generic; // List<>, Dictionary<>
using System.Linq;                // from where select

class Nobel{
    public int      ev          {get; private set;}
    public string   tipus       {get; private set;}
    public string   keresztnev  {get; private set;}
    public string   vezeteknev  {get; private set;}
    
    public Nobel(string sor){
        var s = sor.Split(';');
        ev          = int.Parse(s[0]);
        tipus       = s[1];
        keresztnev  = s[2];
        vezeteknev  = s[3];
    }
}

class Program {
    public static void Main (string[] args) {
        var f = new StreamReader("nobel.csv");
        var elsosor = f.ReadLine();
        var lista = new List<Nobel>();
        while(!f.EndOfStream){
            lista.Add(  new Nobel(f.ReadLine())  );
        } 
        // 3. feladat:  Arthur B.; McDonald
        var donald = (
            from sor in lista
            where sor.keresztnev == "Arthur B." 
            where sor.vezeteknev == "McDonald"
            select sor.tipus
            ).SingleOrDefault();
        Console.WriteLine($"3. feladat: {donald}");

        // 4. feladat:  2017 irodalmi
        var y2017 = (
            from sor in lista 
            where sor.tipus == "irodalmi"
            where sor.ev == 2017 
            select sor
            ).SingleOrDefault();
        Console.WriteLine($"4. feladat: {y2017.keresztnev} {y2017.vezeteknev} ");

        // 5. feladat:  szervezetek 90-től
        var szervezetek = (
            from sor in lista 
            where sor.vezeteknev == ""
            where sor.ev >= 1990 
            select sor
            );
        Console.WriteLine(    $"5. feladat:");  
        foreach(var szervezet in szervezetek){
            Console.WriteLine($"        {szervezet.ev}: {szervezet.keresztnev}");
        }

        // 6. feladat: Curie család
        var curies = (
            from sor in lista 
            where sor.vezeteknev.Contains("Curie") 
            select sor
            );
        Console.WriteLine(    $"6. feladat:");  
        foreach(var curie in curies){
            Console.WriteLine($"        {curie.ev}: {curie.keresztnev} {curie.vezeteknev} ({curie.tipus})");
        }
        // 7. feladat:
        var dijak = (
            from sor in lista 
            group sor by sor.tipus
            );
        Console.WriteLine(    $"7. feladat:");  
        foreach(var dij in dijak){
            Console.WriteLine($"        {dij.Key} {dij.Count()} db");
        }

        // 8. feladat            
        Console.WriteLine($"8. feladat: orvosi.txt");
        var orvosi = (
            from sor in lista
            where sor.tipus == "orvosi"
            orderby sor.ev
            select sor
        );
        var fw = new StreamWriter("orvosi.txt");
        foreach(var sor in orvosi){
            fw.WriteLine($"{sor.ev}:{sor.keresztnev} {sor.vezeteknev}");
        }
        fw.Close();
    // ------------------- 
    }
}