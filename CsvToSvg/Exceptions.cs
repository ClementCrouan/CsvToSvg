using System;

namespace CsvToSvg
{
    public class FormeInconnueException : Exception
    {
        public FormeInconnueException(string typeForme)
            : base($"Type de forme inconnu : '{typeForme}'") { }
    }

    public class LigneCsvInvalideException : Exception
    {
        public int NumeroLigne { get; }
        public string LigneBrute { get; }

        public LigneCsvInvalideException(int numeroLigne, string ligneBrute,
                                         string message, Exception innerException = null)
            : base($"Ligne {numeroLigne} invalide : {message} | Contenu : '{ligneBrute}'", innerException)
        {
            NumeroLigne = numeroLigne;
            LigneBrute = ligneBrute;
        }
    }

    public class CouleurInvalideException : Exception
    {
        public CouleurInvalideException(string composante, int valeur)
            : base($"Composante couleur {composante} invalide : {valeur} (doit être entre 0 et 255)") { }
    }

    public class FichierException : Exception
    {
        public FichierException(string chemin, string message, Exception innerException = null)
            : base($"Erreur sur le fichier '{chemin}' : {message}", innerException) { }
    }
}
