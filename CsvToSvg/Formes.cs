using System;

namespace CsvToSvg
{
    /// <summary>
    /// Représente un cercle.
    /// csv : Cercle;idElement;cx;cy;r;R;G;B;ordre
    /// svg : &lt;circle cx="..." cy="..." r="..." style="fill:rgb(...)" /&gt;
    /// Transformations : translation uniquement.
    /// </summary>
    public class Cercle : Forme
    {
        /// <summary>Coordonnée X du centre.</summary>
        public double Cx { get; set; }

        /// <summary>Coordonnée Y du centre.</summary>
        public double Cy { get; set; }

        /// <summary>Rayon du cercle.</summary>
        public double Rayon { get; set; }

        /// <inheritdoc/>
        public override string ToSvg()
        {
            return $"<circle cx=\"{Cx}\" cy=\"{Cy}\" r=\"{Rayon}\" " +
                   $"style=\"fill:rgb({R},{G},{B})\"{GetTransformAttribute()} />";
        }
    }

    /// <summary>
    /// Représente une ellipse.
    /// csv : Ellipse;idElement;cx;cy;rx;ry;R;G;B;ordre
    /// svg : &lt;ellipse cx="..." cy="..." rx="..." ry="..." style="fill:rgb(...)" /&gt;
    /// Transformations : translation, rotation.
    /// </summary>
    public class Ellipse : Forme
    {
        /// <summary>Coordonnée X du centre.</summary>
        public double Cx { get; set; }

        /// <summary>Coordonnée Y du centre.</summary>
        public double Cy { get; set; }

        /// <summary>Rayon horizontal.</summary>
        public double Rx { get; set; }

        /// <summary>Rayon vertical.</summary>
        public double Ry { get; set; }

        /// <inheritdoc/>
        public override string ToSvg()
        {
            return $"<ellipse cx=\"{Cx}\" cy=\"{Cy}\" rx=\"{Rx}\" ry=\"{Ry}\" " +
                   $"style=\"fill:rgb({R},{G},{B})\"{GetTransformAttribute()} />";
        }
    }

    /// <summary>
    /// Représente un rectangle.
    /// csv : Rectangle;idElement;x;y;l;h;R;G;B;ordre
    /// svg : &lt;rect x="..." y="..." width="..." height="..." style="fill:rgb(...)" /&gt;
    /// Transformations : translation, rotation.
    /// </summary>
    public class Rectangle : Forme
    {
        /// <summary>Coordonnée X du coin supérieur gauche.</summary>
        public double X { get; set; }

        /// <summary>Coordonnée Y du coin supérieur gauche.</summary>
        public double Y { get; set; }

        /// <summary>Largeur du rectangle.</summary>
        public double Largeur { get; set; }

        /// <summary>Hauteur du rectangle.</summary>
        public double Hauteur { get; set; }

        /// <inheritdoc/>
        public override string ToSvg()
        {
            return $"<rect x=\"{X}\" y=\"{Y}\" width=\"{Largeur}\" height=\"{Hauteur}\" " +
                   $"style=\"fill:rgb({R},{G},{B})\"{GetTransformAttribute()} />";
        }
    }

    /// <summary>
    /// Représente un polygone plein défini par une série de points.
    /// csv : Polygone;idElement;points;R;G;B;ordre
    /// svg : &lt;polygon points="..." style="fill:rgb(...)" /&gt;
    /// Transformations : rotation uniquement.
    /// </summary>
    public class Polygone : Forme
    {
        /// <summary>Série de points du polygone (format SVG : "x1,y1 x2,y2 ...").</summary>
        public string Points { get; set; }

        /// <inheritdoc/>
        public override string ToSvg()
        {
            return $"<polygon points=\"{Points}\" " +
                   $"style=\"fill:rgb({R},{G},{B})\"{GetTransformAttribute()} />";
        }
    }

    /// <summary>
    /// Représente un chemin (path) SVG arbitraire.
    /// csv : Chemin;idElement;path;R;G;B;ordre
    /// svg : &lt;path d="..." style="fill:rgb(...)" /&gt;
    /// Transformations : rotation uniquement.
    /// </summary>
    public class Chemin : Forme
    {
        /// <summary>Données de chemin SVG (attribut d).</summary>
        public string PathData { get; set; }

        /// <inheritdoc/>
        public override string ToSvg()
        {
            return $"<path d=\"{PathData}\" " +
                   $"style=\"fill:rgb({R},{G},{B})\"{GetTransformAttribute()} />";
        }
    }

    /// <summary>
    /// Représente un texte affiché à une position donnée.
    /// csv : Texte;idElement;x;y;contenu;R;G;B;ordre
    /// svg : &lt;text x="..." y="..." fill="rgb(...)"&gt;...&lt;/text&gt;
    /// Transformations : translation, rotation.
    /// </summary>
    public class Texte : Forme
    {
        /// <summary>Coordonnée X de la position du texte.</summary>
        public double X { get; set; }

        /// <summary>Coordonnée Y de la position du texte.</summary>
        public double Y { get; set; }

        /// <summary>Contenu textuel à afficher.</summary>
        public string Contenu { get; set; }

        /// <inheritdoc/>
        public override string ToSvg()
        {
            return $"<text x=\"{X}\" y=\"{Y}\" fill=\"rgb({R},{G},{B})\"{GetTransformAttribute()}>" +
                   $"{Contenu}</text>";
        }
    }
}
