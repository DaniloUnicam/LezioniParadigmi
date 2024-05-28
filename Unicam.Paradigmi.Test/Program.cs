﻿//REFERENZIO I NAMESPACE O CON using O CON LA CLASSE SPECIFICA CON IL NOME DELLA CLASSE
//esempio senza using: var persona = new Unicam.Paradigmi.Test.Models.Persona();
using System.Collections;
using Unicam.Paradigmi.Test.Models;
using Unicam.Paradigmi.Test.Examples;
using Unicam.Paradigmi.Abstractions;

var examples = new List<IExample>();
//examples.Add(new FileManagmentExample());
//examples.Add(new InizialializzazioneClassiExample());
//examples.Add(new GestioneEventiExample());
examples.Add(new JsonSerializerExample());



foreach(var example in examples)
{
    example.RunExample();
}

var veicolo = new Veicolo("Macchina",40);
