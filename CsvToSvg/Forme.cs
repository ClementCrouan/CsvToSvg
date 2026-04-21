namespace CsvToSvg
{
    public abstract class Forme
    {
        public int IdElement { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int Ordre { get; set; }
        public Translation TranslationForme { get; set; }
        public Rotation RotationForme { get; set; }

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

        public abstract string ToSvg();
    }
}
