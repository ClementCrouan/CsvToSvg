using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvToSvg
{
    class Dessin
    {
        private List<Forme> _formes = new List<Forme>();
        private CollectionTransformations _transformations = new CollectionTransformations();

        public void LireCsv(string cheminCsv)
        {
            _formes.Clear();

            string[] lignes = File.ReadAllLines(cheminCsv, Encoding.UTF8);
            Dictionary<int, Forme> indexFormes = new Dictionary<int, Forme>();

            foreach (string ligne in lignes)
            {
                string ligneNette = ligne.Trim();
                if (ligneNette == "") continue;

                string[] colonnes = ligneNette.Split(';');
                string type = colonnes[0].Trim();

                if (type == "Cercle")
                {
                    Cercle c = new Cercle();
                    c.IdElement = int.Parse(colonnes[1]);
                    c.Cx        = double.Parse(colonnes[2]);
                    c.Cy        = double.Parse(colonnes[3]);
                    c.Rayon     = double.Parse(colonnes[4]);
                    c.R         = int.Parse(colonnes[5]);
                    c.G         = int.Parse(colonnes[6]);
                    c.B         = int.Parse(colonnes[7]);
                    c.Ordre     = int.Parse(colonnes[8]);
                    _formes.Add(c);
                    indexFormes[c.IdElement] = c;
                }
                else if (type == "Ellipse")
                {
                    Ellipse e = new Ellipse();
                    e.IdElement = int.Parse(colonnes[1]);
                    e.Cx        = double.Parse(colonnes[2]);
                    e.Cy        = double.Parse(colonnes[3]);
                    e.Rx        = double.Parse(colonnes[4]);
                    e.Ry        = double.Parse(colonnes[5]);
                    e.R         = int.Parse(colonnes[6]);
                    e.G         = int.Parse(colonnes[7]);
                    e.B         = int.Parse(colonnes[8]);
                    e.Ordre     = int.Parse(colonnes[9]);
                    _formes.Add(e);
                    indexFormes[e.IdElement] = e;
                }
                else if (type == "Rectangle")
                {
                    Rectangle r = new Rectangle();
                    r.IdElement = int.Parse(colonnes[1]);
                    r.X         = double.Parse(colonnes[2]);
                    r.Y         = double.Parse(colonnes[3]);
                    r.Largeur   = double.Parse(colonnes[4]);
                    r.Hauteur   = double.Parse(colonnes[5]);
                    r.R         = int.Parse(colonnes[6]);
                    r.G         = int.Parse(colonnes[7]);
                    r.B         = int.Parse(colonnes[8]);
                    r.Ordre     = int.Parse(colonnes[9]);
                    _formes.Add(r);
                    indexFormes[r.IdElement] = r;
                }
                else if (type == "Polygone")
                {
                    Polygone p = new Polygone();
                    p.IdElement = int.Parse(colonnes[1]);
                    p.Points    = colonnes[2];
                    p.R         = int.Parse(colonnes[3]);
                    p.G         = int.Parse(colonnes[4]);
                    p.B         = int.Parse(colonnes[5]);
                    p.Ordre     = int.Parse(colonnes[6]);
                    _formes.Add(p);
                    indexFormes[p.IdElement] = p;
                }
                else if (type == "Chemin")
                {
                    Chemin ch = new Chemin();
                    ch.IdElement = int.Parse(colonnes[1]);
                    ch.PathData  = colonnes[2];
                    ch.R         = int.Parse(colonnes[3]);
                    ch.G         = int.Parse(colonnes[4]);
                    ch.B         = int.Parse(colonnes[5]);
                    ch.Ordre     = int.Parse(colonnes[6]);
                    _formes.Add(ch);
                    indexFormes[ch.IdElement] = ch;
                }
                else if (type == "Texte")
                {
                    Texte t = new Texte();
                    t.IdElement = int.Parse(colonnes[1]);
                    t.X         = double.Parse(colonnes[2]);
                    t.Y         = double.Parse(colonnes[3]);
                    t.Contenu   = colonnes[4];
                    t.R         = int.Parse(colonnes[5]);
                    t.G         = int.Parse(colonnes[6]);
                    t.B         = int.Parse(colonnes[7]);
                    t.Ordre     = int.Parse(colonnes[8]);
                    _formes.Add(t);
                    indexFormes[t.IdElement] = t;
                }
                else if (type == "Translation")
                {
                    Translation tr = new Translation();
                    tr.IdElement = int.Parse(colonnes[1]);
                    tr.Dx        = double.Parse(colonnes[2]);
                    tr.Dy        = double.Parse(colonnes[3]);
                    _transformations.Ajouter(tr);
                }
                else if (type == "Rotation")
                {
                    Rotation ro = new Rotation();
                    ro.IdElement = int.Parse(colonnes[1]);
                    ro.Alpha     = double.Parse(colonnes[2]);
                    ro.Cx        = double.Parse(colonnes[3]);
                    ro.Cy        = double.Parse(colonnes[4]);
                    _transformations.Ajouter(ro);
                }
            }

            AppliquerTransformations(indexFormes);
        }

        private void AppliquerTransformations(Dictionary<int, Forme> indexFormes)
        {
            foreach (ITransformation t in _transformations)
            {
                if (indexFormes.ContainsKey(t.IdElement))
                {
                    Forme cible = indexFormes[t.IdElement];
                    if (t is Translation)
                        cible.TranslationForme = (Translation)t;
                    else if (t is Rotation)
                        cible.RotationForme = (Rotation)t;
                }
            }
        }

        public void EcrireSvg(string cheminSvg)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\">");

            List<Forme> formesTriees = _formes.OrderBy(f => f.Ordre).ToList();
            foreach (Forme f in formesTriees)
            {
                sb.AppendLine("  " + f.ToSvg());
            }

            sb.AppendLine("</svg>");
            File.WriteAllText(cheminSvg, sb.ToString(), Encoding.UTF8);
        }
    }
}
