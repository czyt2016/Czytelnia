using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Interfejs zawiera jedynie deklaracje metod, czyli nazwy, parametry i zwracany typ
//metod, które mają się znaleźć w klasie implementującej ten interfejs. Ponieważ repozytorium
//posiada tylko jedną metodę PobierzKsiazki(), interfejs będzie zawierał tylko jedną deklarację
namespace Repository.RInterface
{
    public interface IKsiazkaRepo
    {
        IQueryable<Ksiazka> PobierzKsiazki();
        Ksiazka GetKsiazkaById(int id);
        void UsunKsiazka(int id);
        void SaveChanges();
        void Dodaj(Ksiazka ksiazka);
        void Aktualizuj(Ksiazka ksiazka);

        //metoda będzie pobierała wybraną liczbę kolejnych elementów dla poszczególnych numerów stron
        IQueryable<Ksiazka> PobierzStrone(int? page, int? pageSize);
    }
}