# Réponses aux exercices

## Exercice 1 — Modélisation objet

### Hiérarchie de classes

```
Forme (abstract)
├── Cercle
├── Ellipse
├── Rectangle
├── Polygone
├── Chemin
└── Texte

Translation
Rotation
Dessin
```

### Propriétés communes (Forme)
- IdElement, R, G, B, Ordre
- TranslationForme (nullable)
- RotationForme (nullable)
- ToSvg() : méthode abstraite

### Rattachement lecture CSV / écriture SVG
- **Lecture CSV → classe Dessin** : c'est au niveau du fichier entier que les liens entre
  transformations (Translation, Rotation) et formes (via idElement) peuvent être résolus.
  Une forme seule ne sait pas lire un fichier.
- **Écriture SVG → classe Dessin** (pour l'enveloppe et le tri global) + **Forme.ToSvg()**
  (pour la balise individuelle). Chaque forme est responsable de sa propre représentation SVG,
  mais c'est Dessin qui orchestre l'ordre et l'enveloppe `<svg>...</svg>`.

### Garantir l'ordre d'affichage
Les formes stockées dans `List<Forme>` sont triées par `Ordre` (LINQ `.OrderBy(f => f.Ordre)`)
avant la génération SVG.

---

## Exercice 2 — Implémentation
Voir les fichiers : Forme.cs, Formes.cs, Transformations.cs, Dessin.cs, Program.cs

---

## Exercice 3 — Gestion d'erreurs par exceptions

Quatre exceptions personnalisées (Exceptions.cs) :
- **FichierException** : fichier introuvable ou illisible/non-inscriptible.
- **LigneCsvInvalideException** : ligne mal formée (colonnes manquantes, type non numérique…).
  Contient le numéro de ligne et le contenu brut pour faciliter le débogage.
- **FormeInconnueException** : type de forme non reconnu dans la première colonne.
- **CouleurInvalideException** : composante RGB hors intervalle [0, 255].

Program.cs attrape chacune séparément et affiche un message clair.

---

## Exercice 4 — Classes abstraites

**Forme** est abstraite car :
- Elle définit l'interface commune (ToSvg, IdElement, R, G, B, Ordre…).
- On ne peut pas instancier une "forme générique" : chaque forme a une géométrie propre.
- ToSvg() est abstraite : chaque sous-classe doit fournir sa balise SVG spécifique.

Les autres classes (Cercle, Rectangle, etc.) sont concrètes car elles représentent des
formes réelles instanciables.

---

## Exercice 5 — Documentation
Tout le code est documenté avec les balises XML de documentation C# (///) compatibles
avec Visual Studio et dotnet XML doc. Activer `<GenerateDocumentationFile>true</GenerateDocumentationFile>`
dans le .csproj pour générer le fichier XML de documentation.
