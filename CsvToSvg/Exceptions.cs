using System;

namespace CsvToSvg
{
    /// <summary>
    /// Exception levée lorsqu'un type de forme inconnu est rencontré dans le CSV.
    /// </summary>
    public class FormeInconnueException : Exception
    {
        /// <summary>
        /// Initialise une nouvelle instance de <see cref="FormeInconnueException"/>.
        /// </summary>
        /// <param name="typeForme">Le type de forme non reconnu.</param>
        public FormeInconnueException(string typeForme)
            : base($"Type de forme inconnu : '{typeForme}'")
        {
        }
    }

    /// <summary>
    /// Exception levée lorsqu'une ligne CSV est mal formée (nombre de colonnes incorrect,
    /// valeurs non numériques, etc.).
    /// </summary>
    public class LigneCsvInvalideException : Exception
    {
        /// <summary>Numéro de la ligne fautive dans le fichier CSV.</summary>
        public int NumeroLigne { get; }

        /// <summary>Contenu brut de la ligne fautive.</summary>
        public string LigneBrute { get; }

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="LigneCsvInvalideException"/>.
        /// </summary>
        /// <param name="numeroLigne">Numéro de la ligne (base 1).</param>
        /// <param name="ligneBrute">Contenu brut de la ligne.</param>
        /// <param name="message">Message décrivant le problème.</param>
        /// <param name="innerException">Exception interne éventuelle.</param>
        public LigneCsvInvalideException(int numeroLigne, string ligneBrute,
                                         string message, Exception innerException = null)
            : base($"Ligne {numeroLigne} invalide : {message} | Contenu : '{ligneBrute}'",
                   innerException)
        {
            NumeroLigne = numeroLigne;
            LigneBrute = ligneBrute;
        }
    }

    /// <summary>
    /// Exception levée lorsqu'une couleur RGB contient une valeur hors intervalle [0, 255].
    /// </summary>
    public class CouleurInvalideException : Exception
    {
        /// <summary>
        /// Initialise une nouvelle instance de <see cref="CouleurInvalideException"/>.
        /// </summary>
        /// <param name="composante">Nom de la composante fautive (R, G ou B).</param>
        /// <param name="valeur">Valeur erronée.</param>
        public CouleurInvalideException(string composante, int valeur)
            : base($"Composante couleur {composante} invalide : {valeur} (doit être entre 0 et 255)")
        {
        }
    }

    /// <summary>
    /// Exception levée lorsqu'un fichier CSV ou SVG ne peut pas être lu ou écrit.
    /// </summary>
    public class FichierException : Exception
    {
        /// <summary>
        /// Initialise une nouvelle instance de <see cref="FichierException"/>.
        /// </summary>
        /// <param name="chemin">Chemin du fichier concerné.</param>
        /// <param name="message">Message décrivant le problème.</param>
        /// <param name="innerException">Exception interne éventuelle.</param>
        public FichierException(string chemin, string message, Exception innerException = null)
            : base($"Erreur sur le fichier '{chemin}' : {message}", innerException)
        {
        }
    }
}
