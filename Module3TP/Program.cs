using Module3TP.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3TP
{
    public class Program
    {
        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        static void Main(string[] args)
        {
            InitialiserDatas();

            //1- Afficher la liste des prénoms des auteurs dont le nom commence par "G"
            var authorFirstNameWithNameStartByG = ListeAuteurs.Where(a => a.Nom.ToUpper().StartsWith("G")).Select(a => a.Prenom);
            Console.WriteLine("1- Liste des prénoms des auteurs dont le nom commence par \"G\" : ");
            foreach (var firstName in authorFirstNameWithNameStartByG)
            {
                Console.WriteLine(firstName);
            }


            //2- Afficher l'auteur ayant écrit le plus de livres
            var authorWithMostBooks = ListeLivres.GroupBy(l => l.Auteur).OrderByDescending(n => n.Count()).FirstOrDefault();
            Console.WriteLine($"\n2- L'auteur ayant écrit le plus de livres est {authorWithMostBooks.Key.Prenom} {authorWithMostBooks.Key.Nom} ({authorWithMostBooks.Count()} livres)");


            //3- Afficher le nombre moyen de pages par livre par auteur
            var numberOfBooksByAuthor = ListeLivres.GroupBy(l => l.Auteur);
            Console.WriteLine("\n3- Nombre moyen de pages par livre par auteur :");
            foreach (var author in numberOfBooksByAuthor)
            {
                Console.WriteLine($"Nombre moyen de pages des livres de {author.Key.Prenom} {author.Key.Nom} : {author.Average(l => l.NbPages)} pages");
            }


            //4- Afficher le titre du livre avec le plus de pages
            var bookWithMostPage = ListeLivres.OrderByDescending(l => l.NbPages).FirstOrDefault();
            Console.WriteLine($"\n4- Le titre du livre avec le plus de pages est {bookWithMostPage.Titre} ({bookWithMostPage.NbPages} pages)");


            //5- Afficher combien ont gagné les auteurs en moyenne (moyenne des factures)
            Console.WriteLine("\n5- Combien ont gagné les auteurs en moyenne :");
            foreach (var author in ListeAuteurs)
            {
                if (author.Factures.Count != 0)
                {
                    Console.WriteLine($"La moyenne des factures de {author.Prenom} {author.Nom} est de {author.Factures.Average(f => f.Montant)} euros");
                } else
                {
                    Console.WriteLine($"{author.Prenom} {author.Nom} n'a pas de facture");
                }
            }


            //6- Afficher les auteurs et la liste de leurs livres
            Console.WriteLine("\n6- Liste des auteurs et de leurs livres");
            var booksByAuthor = ListeLivres.GroupBy(l => l.Auteur);

            foreach (var item in booksByAuthor)
            {
                Console.WriteLine($"Livres écrits par {item.Key.Prenom} {item.Key.Nom} :");
                foreach (var book in item)
                {
                    Console.WriteLine($" - {book.Titre}");
                }
            }


            //7- Afficher les titres de tous les livres triés par ordre alphabétique
            var booksOrdredByTitle = ListeLivres.OrderBy(l => l.Titre.ToUpper()).Select(l => l.Titre);
            Console.WriteLine("\n7- Titres de tous les livres triés par ordre alphabétique :");
            foreach (var title in booksOrdredByTitle)
            {
                Console.WriteLine(title);
            }


            //8- Afficher la liste des livres dont le nombre de pages est supérieur à la moyenne
            double averageNumberPages = ListeLivres.Select(l => l.NbPages).Average();
            var booksMorePagesThanAverage = ListeLivres.Where(l => l.NbPages > averageNumberPages);
            Console.WriteLine($"\n8- Liste des livres dont le nombre de pages est supérieur à la moyenne (moyenne = {Math.Round(averageNumberPages)} pages) :");
            foreach (var book in booksMorePagesThanAverage)
            {
                Console.WriteLine($"{book.Titre} de {book.Auteur} ({book.NbPages} pages)");
            }


            //9- Afficher l'auteur ayant écrit le moins de livres
            var authorWithLessBooks = ListeLivres.GroupBy(l => l.Auteur).OrderBy(n => n.Count()).FirstOrDefault();
            Console.WriteLine($"\n9- L'auteur ayant écrit le moins de livres est {authorWithLessBooks.Key.Prenom} {authorWithLessBooks.Key.Nom} ({authorWithLessBooks.Count()} livres)");


            Console.ReadKey();

        }

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).AddFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).AddFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).AddFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).AddFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).AddFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }

    }
}
