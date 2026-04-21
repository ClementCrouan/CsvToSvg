using System;

namespace CsvToSvg
{
    /// <summary>
    /// Représente une transformation de translation appliquée à une forme.
    /// csv : Translation;idElement;dx;dy
    /// </summary>
    public class Translation
    {
        /// <summary>Identifiant de l'élément cible.</summary>
        public int IdElement { get; set; }

        /// <summary>Déplacement horizontal.</summary>
        public double Dx { get; set; }

        /// <summary>Déplacement vertical.</summary>
        public double Dy { get; set; }

        /// <summary>
        /// Génère la valeur SVG de la transformation de translation.
        /// </summary>
        /// <returns>Chaîne translate(...) pour l'attribut transform SVG.</returns>
        public string ToSvgTransform()
        {
            return $"translate({Dx},{Dy})";
        }
    }

    /// <summary>
    /// Représente une transformation de rotation appliquée à une forme.
    /// csv : Rotation;idElement;alpha;cx;cy
    /// </summary>
    public class Rotation
    {
        /// <summary>Identifiant de l'élément cible.</summary>
        public int IdElement { get; set; }

        /// <summary>Angle de rotation en degrés.</summary>
        public double Alpha { get; set; }

        /// <summary>Coordonnée X du centre de rotation.</summary>
        public double Cx { get; set; }

        /// <summary>Coordonnée Y du centre de rotation.</summary>
        public double Cy { get; set; }

        /// <summary>
        /// Génère la valeur SVG de la transformation de rotation.
        /// </summary>
        /// <returns>Chaîne rotate(...) pour l'attribut transform SVG.</returns>
        public string ToSvgTransform()
        {
            return $"rotate({Alpha} {Cx},{Cy})";
        }
    }
}
