using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvToSvg
{
    public class Dessin
    {
        private readonly List<Forme> _formes = new List<Forme>();
        private readonly CollectionTransformations _transformations = new CollectionTransformations();

        public void LireCsv(string cheminCsv)
        {
            _formes.Clear();

            string[] lignes;
            try
            {
                lignes = File.ReadAllLines(cheminCsv, Encoding.UTF8);
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
            {
                throw new FichierException(cheminCsv, "Fichier introuvable.", ex);
            }
            catch (IOException ex)
            {
                throw new FichierException(cheminCsv, "Erreur de lecture.", ex);
            }

            var indexFormes = new Dictionary<int, Forme>();

            for (int i = 0; i < lignes.Length; i++)
            {
                string ligne = lignes[i].Trim();
                if (string.IsNullOrWhiteSpace(ligne) || ligne.StartsWith("//"))
                    continue;

                int numeroLigne = i + 1;
                string[] colonnes = ligne.Split(';');
                string type = colonnes[0].Trim();

                try
                {
                    switch (type.ToLowerInvariant())
                    {
                        case "cercle":      ParseCercle(colonnes, numeroLigne, ligne, indexFormes);     break;
                        case "ellipse":     ParseEllipse(colonnes, numeroLigne, ligne, indexFormes);    break;
                        case "rectangle":   ParseRectangle(colonnes, numeroLigne, ligne, indexFormes);  break;
                        case "polygone":    ParsePolygone(colonnes, numeroLigne, ligne, indexFormes);   break;
                        case "chemin":      ParseChemin(colonnes, numeroLigne, ligne, indexFormes);     break;
                        case "texte":       ParseTexte(colonnes, numeroLigne, ligne, indexFormes);      break;
                        case "translation": _transformations.Ajouter(ParseTranslation(colonnes, numeroLigne, ligne)); break;
                        case "rotation":    _transformations.Ajouter(ParseRotation(colonnes, numeroLigne, ligne));    break;
                        default:            throw new FormeInconnueException(type);
                    }
                }
                catch (LigneCsvInvalideException) { throw; }
                catch (FormeInconnueException)    { throw; }
                catch (CouleurInvalideException)  { throw; }
                catch (Exception ex)
                {
                    throw new LigneCsvInvalideException(numeroLigne, ligne, "Erreur inattendue.", ex);
                }
            }

            AppliquerTransformations(indexFormes);
        }

        private void AppliquerTransformations(Dictionary<int, Forme> indexFormes)
        {
            foreach (ITransformation t in _transformations)
            {
                if (!indexFormes.TryGetValue(t.IdElement, out Forme cible))
                    continue;

                if (t is Translation tr)
                    cible.TranslationForme = tr;
                else if (t is Rotation ro)
                    cible.RotationForme = ro;
            }
        }

        public void EcrireSvg(string cheminSvg)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\">");

            foreach (Forme f in _formes.OrderBy(f => f.Ordre))
            {
                sb.AppendLine("  " + f.ToSvg());
            }

            sb.AppendLine("</svg>");

            try
            {
                File.WriteAllText(cheminSvg, sb.ToString(), Encoding.UTF8);
            }
            catch (IOException ex)
            {
                throw new FichierException(cheminSvg, "Erreur lors de l'écriture.", ex);
            }
        }

        private void EnregistrerForme(Forme f, Dictionary<int, Forme> index)
        {
            _formes.Add(f);
            index[f.IdElement] = f;
        }

        private static void ValiderCouleur(int r, int g, int b)
        {
            if (r < 0 || r > 255) throw new CouleurInvalideException("R", r);
            if (g < 0 || g > 255) throw new CouleurInvalideException("G", g);
            if (b < 0 || b > 255) throw new CouleurInvalideException("B", b);
        }

        private static void AssertColonnes(string[] col, int attendu, int nl, string l)
        {
            if (col.Length < attendu)
                throw new LigneCsvInvalideException(nl, l,
                    $"Colonnes insuffisantes (attendu {attendu}, obtenu {col.Length}).");
        }

        private void ParseCercle(string[] c, int nl, string l, Dictionary<int, Forme> idx)
        {
            AssertColonnes(c, 9, nl, l);
            try
            {
                int id = int.Parse(c[1].Trim());
                double cx = double.Parse(c[2].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                double cy = double.Parse(c[3].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                double rayon = double.Parse(c[4].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                int r = int.Parse(c[5].Trim()), g = int.Parse(c[6].Trim()), b = int.Parse(c[7].Trim());
                int ordre = int.Parse(c[8].Trim());
                ValiderCouleur(r, g, b);
                EnregistrerForme(new Cercle { IdElement = id, Cx = cx, Cy = cy, Rayon = rayon, R = r, G = g, B = b, Ordre = ordre }, idx);
            }
            catch (FormatException ex) { throw new LigneCsvInvalideException(nl, l, "Valeur numérique invalide.", ex); }
        }

        private void ParseEllipse(string[] c, int nl, string l, Dictionary<int, Forme> idx)
        {
            AssertColonnes(c, 10, nl, l);
            try
            {
                int id = int.Parse(c[1].Trim());
                double cx = double.Parse(c[2].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                double cy = double.Parse(c[3].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                double rx = double.Parse(c[4].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                double ry = double.Parse(c[5].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                int r = int.Parse(c[6].Trim()), g = int.Parse(c[7].Trim()), b = int.Parse(c[8].Trim());
                int ordre = int.Parse(c[9].Trim());
                ValiderCouleur(r, g, b);
                EnregistrerForme(new Ellipse { IdElement = id, Cx = cx, Cy = cy, Rx = rx, Ry = ry, R = r, G = g, B = b, Ordre = ordre }, idx);
            }
            catch (FormatException ex) { throw new LigneCsvInvalideException(nl, l, "Valeur numérique invalide.", ex); }
        }

        private void ParseRectangle(string[] c, int nl, string l, Dictionary<int, Forme> idx)
        {
            AssertColonnes(c, 10, nl, l);
            try
            {
                int id = int.Parse(c[1].Trim());
                double x = double.Parse(c[2].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                double y = double.Parse(c[3].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                double larg = double.Parse(c[4].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                double haut = double.Parse(c[5].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                int r = int.Parse(c[6].Trim()), g = int.Parse(c[7].Trim()), b = int.Parse(c[8].Trim());
                int ordre = int.Parse(c[9].Trim());
                ValiderCouleur(r, g, b);
                EnregistrerForme(new Rectangle { IdElement = id, X = x, Y = y, Largeur = larg, Hauteur = haut, R = r, G = g, B = b, Ordre = ordre }, idx);
            }
            catch (FormatException ex) { throw new LigneCsvInvalideException(nl, l, "Valeur numérique invalide.", ex); }
        }

        private void ParsePolygone(string[] c, int nl, string l, Dictionary<int, Forme> idx)
        {
            AssertColonnes(c, 7, nl, l);
            try
            {
                int id = int.Parse(c[1].Trim());
                string points = c[2].Trim();
                int r = int.Parse(c[3].Trim()), g = int.Parse(c[4].Trim()), b = int.Parse(c[5].Trim());
                int ordre = int.Parse(c[6].Trim());
                ValiderCouleur(r, g, b);
                EnregistrerForme(new Polygone { IdElement = id, Points = points, R = r, G = g, B = b, Ordre = ordre }, idx);
            }
            catch (FormatException ex) { throw new LigneCsvInvalideException(nl, l, "Valeur numérique invalide.", ex); }
        }

        private void ParseChemin(string[] c, int nl, string l, Dictionary<int, Forme> idx)
        {
            AssertColonnes(c, 7, nl, l);
            try
            {
                int id = int.Parse(c[1].Trim());
                string path = c[2].Trim();
                int r = int.Parse(c[3].Trim()), g = int.Parse(c[4].Trim()), b = int.Parse(c[5].Trim());
                int ordre = int.Parse(c[6].Trim());
                ValiderCouleur(r, g, b);
                EnregistrerForme(new Chemin { IdElement = id, PathData = path, R = r, G = g, B = b, Ordre = ordre }, idx);
            }
            catch (FormatException ex) { throw new LigneCsvInvalideException(nl, l, "Valeur numérique invalide.", ex); }
        }

        private void ParseTexte(string[] c, int nl, string l, Dictionary<int, Forme> idx)
        {
            AssertColonnes(c, 9, nl, l);
            try
            {
                int id = int.Parse(c[1].Trim());
                double x = double.Parse(c[2].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                double y = double.Parse(c[3].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                string contenu = c[4].Trim();
                int r = int.Parse(c[5].Trim()), g = int.Parse(c[6].Trim()), b = int.Parse(c[7].Trim());
                int ordre = int.Parse(c[8].Trim());
                ValiderCouleur(r, g, b);
                EnregistrerForme(new Texte { IdElement = id, X = x, Y = y, Contenu = contenu, R = r, G = g, B = b, Ordre = ordre }, idx);
            }
            catch (FormatException ex) { throw new LigneCsvInvalideException(nl, l, "Valeur numérique invalide.", ex); }
        }

        private static Translation ParseTranslation(string[] c, int nl, string l)
        {
            AssertColonnes(c, 4, nl, l);
            try
            {
                return new Translation
                {
                    IdElement = int.Parse(c[1].Trim()),
                    Dx = double.Parse(c[2].Trim(), System.Globalization.CultureInfo.InvariantCulture),
                    Dy = double.Parse(c[3].Trim(), System.Globalization.CultureInfo.InvariantCulture)
                };
            }
            catch (FormatException ex) { throw new LigneCsvInvalideException(nl, l, "Valeur numérique invalide.", ex); }
        }

        private static Rotation ParseRotation(string[] c, int nl, string l)
        {
            AssertColonnes(c, 5, nl, l);
            try
            {
                return new Rotation
                {
                    IdElement = int.Parse(c[1].Trim()),
                    Alpha = double.Parse(c[2].Trim(), System.Globalization.CultureInfo.InvariantCulture),
                    Cx = double.Parse(c[3].Trim(), System.Globalization.CultureInfo.InvariantCulture),
                    Cy = double.Parse(c[4].Trim(), System.Globalization.CultureInfo.InvariantCulture)
                };
            }
            catch (FormatException ex) { throw new LigneCsvInvalideException(nl, l, "Valeur numérique invalide.", ex); }
        }
    }
}
