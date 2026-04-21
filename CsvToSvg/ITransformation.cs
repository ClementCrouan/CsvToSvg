namespace CsvToSvg
{
    public interface ITransformation
    {
        int IdElement { get; set; }
        string ToSvgTransform();
    }
}
