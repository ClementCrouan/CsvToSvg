namespace CsvToSvg
{
    interface ITransformation
    {
        int IdElement { get; set; }
        string ToSvgTransform();
    }
}
