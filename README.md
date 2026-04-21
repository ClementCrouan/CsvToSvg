# CsvToSvg — Conversion CSV → SVG

## Description
Programme C# convertissant des fichiers CSV décrivant des formes géométriques en fichiers SVG affichables dans un navigateur.

## Usage
```
CsvToSvg.exe fichier.csv fichier.svg   # conversion explicite
CsvToSvg.exe fichier.csv              # svg = même nom que le csv
CsvToSvg.exe                          # génère demo.csv + demo.svg
```

## Formes supportées
| CSV | Paramètres |
|-----|-----------|
| Cercle | idElement;cx;cy;r;R;G;B;ordre |
| Ellipse | idElement;cx;cy;rx;ry;R;G;B;ordre |
| Rectangle | idElement;x;y;l;h;R;G;B;ordre |
| Polygone | idElement;points;R;G;B;ordre |
| Chemin | idElement;path;R;G;B;ordre |
| Texte | idElement;x;y;contenu;R;G;B;ordre |
| Translation | idElement;dx;dy |
| Rotation | idElement;alpha;cx;cy |

## Réponses aux exercices
Voir README_Exercices.md
