using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonopolyGameManager : MonoBehaviour
{
    // The dictionary of tiles that the player can land on. This is static so all players use the same synced dictionary
    public static Dictionary<int, MonopolySpace> boardSpaces = new Dictionary<int, MonopolySpace>();

    void Start()
    {
        // Create Pass Go Space
        MonopolySpace startspot = new MonopolySpace();
        startspot.name = "startspot";
        startspot.passCollect = 200;
        boardSpaces.Add(0, startspot);

        // Create Old Kent Road
        MonopolySpace oldkentroad = new MonopolySpace();
        oldkentroad.name = "oldkentroad";
        oldkentroad.purchasePrice = 60;
        oldkentroad.rent = 2;
        oldkentroad.owner = -1;
        oldkentroad.set = "Dark Blue";
        boardSpaces.Add(1, oldkentroad);

        // Create Community Chest 1
        MonopolySpace chest1 = new MonopolySpace();
        chest1.name = "chest1";
        boardSpaces.Add(2, chest1);

        // Create Whitechapel
        MonopolySpace whitechapel = new MonopolySpace();
        whitechapel.name = "whitechapel";
        whitechapel.purchasePrice = 60;
        whitechapel.rent = 4;
        whitechapel.owner = -1;
        whitechapel.set = "Dark Blue";
        boardSpaces.Add(3, whitechapel);

        // Create Income Tax
        MonopolySpace incometax = new MonopolySpace();
        incometax.name = "incometax";
        incometax.amount = 200;
        boardSpaces.Add(4, incometax);

        // Create Station 1
        MonopolySpace station1 = new MonopolySpace();
        station1.name = "station1";
        station1.purchasePrice = 200;
        station1.rent = 25;
        station1.owner = -1;
        station1.set = "Railroad";
        boardSpaces.Add(5, station1);

        // Create The Angel Islington
        MonopolySpace theangelislington = new MonopolySpace();
        theangelislington.name = "theangelislington";
        theangelislington.purchasePrice = 100;
        theangelislington.rent = 6;
        theangelislington.owner = -1;
        theangelislington.set = "Light Blue";
        boardSpaces.Add(6, theangelislington);

        // Create Chance 1
        MonopolySpace chance1 = new MonopolySpace();
        chance1.name = "chance1";
        boardSpaces.Add(7, chance1);

        // Create Euston Road
        MonopolySpace eustonroad = new MonopolySpace();
        eustonroad.name = "eustonroad";
        eustonroad.purchasePrice = 100;
        eustonroad.rent = 6;
        eustonroad.owner = -1;
        eustonroad.set = "Light Blue";
        boardSpaces.Add(8, eustonroad);

        // Create Pentonville
        MonopolySpace pentonville = new MonopolySpace();
        pentonville.name = "pentonville";
        pentonville.purchasePrice = 120;
        pentonville.owner = -1;
        pentonville.rent = 8;
        boardSpaces.Add(9, pentonville);

        // Create Jail
        MonopolySpace jail = new MonopolySpace();
        jail.name = "jail";
        boardSpaces.Add(10, jail);

        // Create Pall Mall
        MonopolySpace pallmall = new MonopolySpace();
        pallmall.name = "pallmall";
        pallmall.purchasePrice = 140;
        pallmall.rent = 10;
        pallmall.owner = -1;
        pallmall.set = "Magenta";
        boardSpaces.Add(11, pallmall);

        // Create Electric Company
        MonopolySpace electric = new MonopolySpace();
        electric.name = "electric";
        electric.purchasePrice = 150;
        electric.owner = -1;
        electric.set = "Utility";
        boardSpaces.Add(12, electric);

        // Create Whitehall
        MonopolySpace whitehall = new MonopolySpace();
        whitehall.name = "whitehall";
        whitehall.purchasePrice = 140;
        whitehall.rent = 10;
        whitehall.owner = -1;
        whitehall.set = "Magenta";
        boardSpaces.Add(13, whitehall);

        // Create Northumberland Avenue
        MonopolySpace northhumrld = new MonopolySpace();
        northhumrld.name = "northhumrld";
        northhumrld.purchasePrice = 160;
        northhumrld.rent = 12;
        northhumrld.owner = -1;
        northhumrld.set = "Magenta";
        boardSpaces.Add(14, northhumrld);

        // Create Station 2
        MonopolySpace station2 = new MonopolySpace();
        station2.name = "station2";
        station2.purchasePrice = 200;
        station2.rent = 25;
        station2.owner = -1;
        station2.set = "Railroad";
        boardSpaces.Add(15, station2);

        // Create Bow Street
        MonopolySpace bowstreet = new MonopolySpace();
        bowstreet.name = "bowstreet";
        bowstreet.purchasePrice = 180;
        bowstreet.rent = 14;
        bowstreet.owner = -1;
        bowstreet.set = "Orange";
        boardSpaces.Add(16, bowstreet);

        // Create Community Chest 2
        MonopolySpace chest2 = new MonopolySpace();
        chest2.name = "chest2";
        boardSpaces.Add(17, chest2);

        // Create Marlborough Street
        MonopolySpace marlborough = new MonopolySpace();
        marlborough.name = "marlborough";
        marlborough.purchasePrice = 180;
        marlborough.rent = 14;
        marlborough.owner = -1;
        marlborough.set = "Orange";
        boardSpaces.Add(18, marlborough);

        // Create Vine Street
        MonopolySpace vinestreet = new MonopolySpace();
        vinestreet.name = "vinestreet";
        vinestreet.purchasePrice = 200;
        vinestreet.rent = 16;
        vinestreet.owner = -1;
        vinestreet.set = "Orange";
        boardSpaces.Add(19, vinestreet);

        // Create Free Parking
        MonopolySpace freeparking = new MonopolySpace();
        freeparking.name = "freeparking";
        freeparking.landCollect = 100;
        boardSpaces.Add(20, freeparking);

        // Create Strand
        MonopolySpace strand = new MonopolySpace();
        strand.name = "strand";
        strand.purchasePrice = 220;
        strand.rent = 18;
        strand.owner = -1;
        strand.set = "Red";
        boardSpaces.Add(21, strand);

        // Create Chance 2
        MonopolySpace chance2 = new MonopolySpace();
        chance2.name = "chance2";
        boardSpaces.Add(22, chance2);

        // Create Fleet Street
        MonopolySpace fleetstreet = new MonopolySpace();
        fleetstreet.name = "fleetstreet";
        fleetstreet.purchasePrice = 220;
        fleetstreet.rent = 18;
        fleetstreet.owner = -1;
        fleetstreet.set = "Red";
        boardSpaces.Add(23, fleetstreet);

        // Create Trafalgar Square
        MonopolySpace trafalgar = new MonopolySpace();
        trafalgar.name = "trafalgar";
        trafalgar.purchasePrice = 240;
        trafalgar.rent = 20;
        trafalgar.owner = -1;
        trafalgar.set = "Red";
        boardSpaces.Add(24, trafalgar);

        // Create Station 3
        MonopolySpace station3 = new MonopolySpace();
        station3.name = "station3";
        station3.purchasePrice = 200;
        station3.rent = 25;
        station3.owner = -1;
        station3.set = "Railroad";
        boardSpaces.Add(25, station3);

        // Create Leicester Square
        MonopolySpace leicester = new MonopolySpace();
        leicester.name = "leicester";
        leicester.purchasePrice = 260;
        leicester.rent = 22;
        leicester.owner = -1;
        leicester.set = "Yellow";
        boardSpaces.Add(26, leicester);

        // Create Coventry Street
        MonopolySpace coventrystreet = new MonopolySpace();
        coventrystreet.name = "coventrystreet";
        coventrystreet.purchasePrice = 260;
        coventrystreet.rent = 22;
        coventrystreet.owner = -1;
        coventrystreet.set = "Yellow";
        boardSpaces.Add(27, coventrystreet);

        // Create Water Works
        MonopolySpace waterwork = new MonopolySpace();
        waterwork.name = "waterwork";
        waterwork.purchasePrice = 150;
        waterwork.owner = -1;
        waterwork.set = "Utility";
        boardSpaces.Add(28, waterwork);

        // Create Piccadilly
        MonopolySpace piccadilly = new MonopolySpace();
        piccadilly.name = "piccadilly";
        piccadilly.purchasePrice = 280;
        piccadilly.rent = 24;
        piccadilly.owner = -1;
        piccadilly.set = "Yellow";
        boardSpaces.Add(29, piccadilly);

        // Create Go to Jail
        MonopolySpace gotojail = new MonopolySpace();
        gotojail.name = "gotojail";
        boardSpaces.Add(30, gotojail);

        // Create Regent Street
        MonopolySpace regentstreet = new MonopolySpace();
        regentstreet.name = "regentstreet";
        regentstreet.purchasePrice = 300;
        regentstreet.rent = 26;
        regentstreet.owner = -1;
        regentstreet.set = "Green";
        boardSpaces.Add(31, regentstreet);

        // Create Oxford Street
        MonopolySpace oxfordstreet = new MonopolySpace();
        oxfordstreet.name = "oxfordstreet";
        oxfordstreet.purchasePrice = 300;
        oxfordstreet.rent = 26;
        oxfordstreet.owner = -1;
        oxfordstreet.set = "Green";
        boardSpaces.Add(32, oxfordstreet);

        // Create Community Chest 3
        MonopolySpace chest3 = new MonopolySpace();
        chest3.name = "chest3";
        boardSpaces.Add(33, chest3);

        // Create Bond Street
        MonopolySpace bondstreet = new MonopolySpace();
        bondstreet.name = "bondstreet";
        bondstreet.purchasePrice = 320;
        bondstreet.rent = 28;
        bondstreet.owner = -1;
        bondstreet.set = "Green";
        boardSpaces.Add(34, bondstreet);

        // Create Station 4
        MonopolySpace station4 = new MonopolySpace();
        station4.name = "station4";
        station4.purchasePrice = 200;
        station4.rent = 25;
        station4.set = "Railroad";
        station4.owner = -1;
        boardSpaces.Add(35, station4);

        // Create Chance 3
        MonopolySpace chance3 = new MonopolySpace();
        chance3.name = "chance3";
        boardSpaces.Add(36, chance3);

        // Create Park Lane
        MonopolySpace parklane = new MonopolySpace();
        parklane.name = "parklane";
        parklane.purchasePrice = 350;
        parklane.rent = 35;
        parklane.owner = -1;
        parklane.set = "Royal Blue";
        boardSpaces.Add(37, parklane);

        // Create Super Tax
        MonopolySpace supertax = new MonopolySpace();
        supertax.name = "supertax";
        supertax.amount = 100;
        boardSpaces.Add(38, supertax);

        // Create Mayfair
        MonopolySpace mayfair = new MonopolySpace();
        mayfair.name = "mayfair";
        mayfair.purchasePrice = 400;
        mayfair.rent = 50;
        mayfair.owner = -1;
        mayfair.set = "Royal Blue";
        boardSpaces.Add(39, mayfair);

    }
}

// Class for tiles
public class MonopolySpace
{
    public string name;
    public int purchasePrice;
    public int rent;
    public int owner;
    public string set;
    public int amount;
    public int passCollect;
    public int landCollect;
    // Add specific properties or methods for Chance spaces here
}

