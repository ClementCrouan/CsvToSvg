namespace CsvToSvg
{
    public class Cercle : Forme
    {
        public double Cx { get; set; }
        public double Cy { get; set; }
        public double Rayon { get; set; }

        public override string ToSvg()
        {
            return $"<circle cx=\"{Cx}\" cy=\"{Cy}\" r=\"{Rayon}\" " +
                   $"style=\"fill:rgb({R},{G},{B})\"{GetTransformAttribute()} />";
        }
    }

    public class Ellipse : Forme
    {
        public double Cx { get; set; }
        public double Cy { get; set; }
        public double Rx { get; set; }
        public double Ry { get; set; }

        public override string ToSvg()
        {
            return $"<ellipse cx=\"{Cx}\" cy=\"{Cy}\" rx=\"{Rx}\" ry=\"{Ry}\" " +
                   $"style=\"fill:rgb({R},{G},{B})\"{GetTransformAttribute()} />";
        }
    }

    public class Rectangle : Forme
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Largeur { get; set; }
        public double Hauteur { get; set; }

        public override string ToSvg()
        {
            return $"<rect x=\"{X}\" y=\"{Y}\" width=\"{Largeur}\" height=\"{Hauteur}\" " +
                   $"style=\"fill:rgb({R},{G},{B})\"{GetTransformAttribute()} />";
        }
    }

    public class Polygone : Forme
    {
        public string Points { get; set; }

        public override string ToSvg()
        {
            return $"<polygon points=\"{Points}\" " +
                   $"style=\"fill:rgb({R},{G},{B})\"{GetTransformAttribute()} />";
        }
    }

    public class Chemin : Forme
    {
        public string PathData { get; set; }

        public override string ToSvg()
        {
            return $"<path d=\"{PathData}\" " +
                   $"style=\"fill:rgb({R},{G},{B})\"{GetTransformAttribute()} />";
        }
    }

    public class Texte : Forme
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Contenu { get; set; }

        public override string ToSvg()
        {
            return $"<text x=\"{X}\" y=\"{Y}\" fill=\"rgb({R},{G},{B})\"{GetTransformAttribute()}>" +
                   $"{Contenu}</text>";
        }
    }
}
