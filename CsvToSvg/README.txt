================================================
  CsvToSvg — Convertisseur CSV vers SVG
================================================

---[ CHANGER LE FICHIER CSV ]---

Ouvrez Program.cs et modifiez les deux constantes en haut de la classe :

    private const string FICHIER_CSV = "demo.csv";   ← votre CSV ici
    private const string FICHIER_SVG = "demo.svg";   ← SVG de sortie ici

ATTENTION au chemin :
  - Chemin relatif : le fichier doit être dans bin\Debug\net6.0\
    (copiez-y votre CSV avant de lancer le programme)
  - Chemin absolu  : ex.  @"C:\Users\Moi\Documents\monDessin.csv"
    (le @ devant évite les problèmes avec les antislashs)

Exemple avec chemin absolu :
    private const string FICHIER_CSV = @"C:\TP\ExempleTout.csv";
    private const string FICHIER_SVG = @"C:\TP\ExempleTout.svg";


---[ USAGE EN LIGNE DE COMMANDE ]---

Vous pouvez aussi passer les fichiers directement au lancement :

    CsvToSvg.exe monfichier.csv              → génère monfichier.svg
    CsvToSvg.exe monfichier.csv sortie.svg   → génère sortie.svg


---[ FORMAT DU FICHIER CSV ]---

Séparateur : point-virgule ( ; )
Une ligne = une forme ou une transformation.

FORMES :
  Cercle    ;idElement;cx;cy;r;R;G;B;ordre
  Ellipse   ;idElement;cx;cy;rx;ry;R;G;B;ordre
  Rectangle ;idElement;x;y;largeur;hauteur;R;G;B;ordre
  Polygone  ;idElement;points;R;G;B;ordre
  Chemin    ;idElement;pathData;R;G;B;ordre
  Texte     ;idElement;x;y;contenu;R;G;B;ordre

TRANSFORMATIONS (après les formes) :
  Translation ;idElement;dx;dy
  Rotation    ;idElement;angle;cx;cy

R, G, B = valeurs entre 0 et 255 (couleur de remplissage)
ordre   = entier, les éléments sont dessinés du plus petit au plus grand


---[ EXEMPLE DE CSV ]---

Cercle;1;100;50;40;255;255;0;1
Rectangle;2;110;70;300;100;0;0;255;2
Ellipse;3;250;200;80;40;255;0;0;3
Texte;4;50;350;Bonjour;0;0;0;4
Translation;2;20;10
Rotation;3;30;250;200


---[ STRUCTURE DES FICHIERS .CS ]---

  Forme.cs          → classe abstraite de base (Exercice 4)
  Formes.cs         → Cercle, Ellipse, Rectangle, Polygone, Chemin, Texte
  Transformations.cs→ Translation, Rotation
  Dessin.cs         → lecture CSV + écriture SVG (Exercice 1)
  Exceptions.cs     → FichierException, LigneCsvInvalideException,
                       FormeInconnueException, CouleurInvalideException (Exercice 3)
  Program.cs        → point d'entrée, constantes FICHIER_CSV / FICHIER_SVG


---[ RÉPONSES AUX EXERCICES ]---

Exercice 1 — Modélisation
  Forme est la classe abstraite parente de toutes les formes.
  Lecture CSV → attachée à Dessin (car c'est le fichier entier qui relie
                les transformations aux formes via idElement).
  Écriture SVG → Dessin génère l'enveloppe <svg>, chaque Forme génère
                 sa propre balise via ToSvg() (méthode abstraite).
  Ordre d'affichage → tri LINQ .OrderBy(f => f.Ordre) avant génération.

Exercice 2 — Code
  Tout est implémenté dans les fichiers listés ci-dessus.

Exercice 3 — Exceptions
  4 exceptions personnalisées dans Exceptions.cs.
  Program.cs les attrape et affiche un message clair dans la console.

Exercice 4 — Classe abstraite
  Forme est abstraite : on ne peut pas instancier une "forme générique",
  et ToSvg() doit obligatoirement être définie par chaque sous-classe.

Exercice 5 — Documentation
  Tout le code est documenté avec les balises XML /// compatibles
  avec Visual Studio (IntelliSense et génération de doc XML).

================================================
