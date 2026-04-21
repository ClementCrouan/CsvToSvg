using System;

namespace CsvToSvg
{
    /// <summary>
    /// Classe abstraite représentant une forme géométrique.
    /// Toutes les formes héritent de cette classe.
    /// </summary>
    public abstract class Forme
    {
        /// <summary>Identifiant unique de l'élément.</summary>
        public int IdElement { get; set; }

        /// <summary>Composante rouge de la couleur de remplissage (0-255).</summary>
        public int R { get; set; }

        /// <summary>Composante verte de la couleur de remplissage (0-255).</summary>
        public int G { get; set; }

        /// <summary>Composante bleue de la couleur de remplissage (0-255).</summary>
        public int B { get; set; }

        /// <summary>Ordre d'affichage dans le fichier SVG (1 = premier).</summary>
        public int Ordre { get; set; }

        /// <summary>Transformation de translation associée à cette forme (peut être null).</summary>
        public Translation TranslationForme { get; set; }

        /// <summary>Transformation de rotation associée à cette forme (peut être null).</summary>
        public Rotation RotationForme { get; set; }

        /// <summary>
        /// Génère la chaîne d'attribut de transformation SVG.
        /// Combine rotation et/ou translation si elles sont définies.
        /// </summary>
        /// <returns>Attribut transform SVG ou chaîne vide.</returns>
        protected string GetTransformAttribute()
        {
            string rotate = RotationForme != null ? RotationForme.ToSvgTransform() : null;
            string translate = TranslationForme != null ? TranslationForme.ToSvgTransform() : null;

            if (rotate != null && translate != null)
                return $" transform=\"{rotate} {translate}\"";
            if (rotate != null)
                return $" transform=\"{rotate}\"";
            if (translate != null)
                return $" transform=\"{translate}\"";
            return "";
        }

        /// <summary>
        /// Génère la représentation SVG de la forme.
        /// Doit être implémentée par chaque sous-classe.
        /// </summary>
        /// <returns>Balise SVG correspondant à la forme.</returns>
        public abstract string ToSvg();
    }
}
