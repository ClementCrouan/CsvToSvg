using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CsvToSvg
{
    public class CollectionTransformations : IEnumerable<ITransformation>
    {
        private readonly List<ITransformation> _transformations = new List<ITransformation>();

        public int Count => _transformations.Count;

        public void Ajouter(ITransformation t)
        {
            _transformations.Add(t);
        }

        public IEnumerable<ITransformation> ObtenirPour(int idElement)
        {
            return _transformations.Where(t => t.IdElement == idElement);
        }

        public IEnumerator<ITransformation> GetEnumerator()
        {
            return _transformations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
