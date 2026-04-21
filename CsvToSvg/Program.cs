using System;
using System.IO;

namespace CsvToSvg
{
    /// <summary>
    /// Point d'entree du programme de conversion CSV vers SVG.
    /// CHANGER LE FICHIER CSV : modifiez la constante FICHIER_CSV ci-dessous.
    /// Conseil : utilisez un chemin absolu ex. @"C:\TP\monDessin.csv"
    /// </summary>
    class Program
    {
        // ┌──────────────────────────────────────────────────────────┐
        // │   MODIFIEZ CES DEUX LIGNES POUR CHANGER LES FICHIERS     │
        // │   Chemin absolu recommande : @"C:\dossier\fichier.csv"   │
        // └──────────────────────────────────────────────────────────┘
        private const string FICHIER_CSV = "exemple2.csv";   // <- votre CSV ici
        private const string FICHIER_SVG = "exemple2.svg";   // <- SVG genere ici

        static void Main(string[] args)
        {
            Console.WriteLine("=== CsvToSvg ===");
            Console.WriteLine("Dossier courant : " + Directory.GetCurrentDirectory());
            Console.WriteLine("(Deposez votre CSV dans ce dossier si chemin relatif)");
            Console.WriteLine();

            string cheminCsv;
            string cheminSvg;

            if (args.Length >= 2)
            {
                cheminCsv = args[0];
                cheminSvg = args[1];
            }
            else if (args.Length == 1)
            {
                cheminCsv = args[0];
                cheminSvg = Path.ChangeExtension(cheminCsv, ".svg");
            }
            else
            {
                cheminCsv = FICHIER_CSV;
                cheminSvg = FICHIER_SVG;

                if (!File.Exists(cheminCsv))
                {
                    CreerCsvDemo(cheminCsv);
                    Console.WriteLine("Demo cree : " + Path.GetFullPath(cheminCsv));
                }
            }

            Console.WriteLine("Lecture  : " + Path.GetFullPath(cheminCsv));
            Console.WriteLine("Ecriture : " + Path.GetFullPath(cheminSvg));
            Console.WriteLine();

            try
            {
                var dessin = new Dessin();
                dessin.LireCsv(cheminCsv);
                dessin.EcrireSvg(cheminSvg);
                Console.WriteLine("Conversion reussie !");
                Console.WriteLine("SVG genere : " + Path.GetFullPath(cheminSvg));
            }
            catch (FichierException ex)
            {
                Console.Error.WriteLine("[ERREUR FICHIER] " + ex.Message);
            }
            catch (LigneCsvInvalideException ex)
            {
                Console.Error.WriteLine("[ERREUR CSV] " + ex.Message);
            }
            catch (FormeInconnueException ex)
            {
                Console.Error.WriteLine("[ERREUR FORME] " + ex.Message);
            }
            catch (CouleurInvalideException ex)
            {
                Console.Error.WriteLine("[ERREUR COULEUR] " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("[ERREUR INATTENDUE] " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Appuyez sur une touche pour fermer...");
            Console.ReadKey();
        }

        /// <summary>
        /// Genere un fichier CSV de demonstration contenant toutes les formes.
        /// </summary>
        private static void CreerCsvDemo(string chemin)
        {
            string contenu =
                "Cercle;1;100;50;40;255;255;0;1\n" +
                "Rectangle;2;110;70;300;100;0;0;255;2\n" +
                "Ellipse;3;250;200;80;40;255;0;0;3\n" +
                "Polygone;4;50,150 100,50 150,150;0;200;100;4\n" +
                "Chemin;5;M 200 300 L 300 300 L 250 200 Z;128;0;128;5\n" +
                "Texte;6;50;350;Bonjour SVG;0;0;0;6\n" +
                "Translation;2;20;10\n" +
                "Rotation;3;30;250;200\n";

            File.WriteAllText(chemin, contenu, System.Text.Encoding.UTF8);
        }
    }
}
