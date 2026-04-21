namespace CsvToSvg
{
    public class Translation : ITransformation
    {
        public int IdElement { get; set; }
        public double Dx { get; set; }
        public double Dy { get; set; }

        public string ToSvgTransform()
        {
            return $"translate({Dx},{Dy})";
        }
    }

    public class Rotation : ITransformation
    {
        public int IdElement { get; set; }
        public double Alpha { get; set; }
        public double Cx { get; set; }
        public double Cy { get; set; }

        public string ToSvgTransform()
        {
            return $"rotate({Alpha} {Cx},{Cy})";
        }
    }
}
