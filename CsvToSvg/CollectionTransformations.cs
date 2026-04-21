using System.Collections;
using System.Collections.Generic;

namespace CsvToSvg
{
    class CollectionTransformations : IEnumerable<ITransformation>
    {
        private List<ITransformation> _transformations = new List<ITransformation>();

        public int Count
        {
            get { return _transformations.Count; }
        }

        public void Ajouter(ITransformation t)
        {
            _transformations.Add(t);
        }

        public IEnumerable<ITransformation> ObtenirPour(int idElement)
        {
            List<ITransformation> resultat = new List<ITransformation>();
            foreach (ITransformation t in _transformations)
            {
                if (t.IdElement == idElement)
                    resultat.Add(t);
            }
            return resultat;
        }

        public IEnumerator<ITransformation> GetEnumerator()
        {
            return _transformations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _transformations.GetEnumerator();
        }
    }
}
