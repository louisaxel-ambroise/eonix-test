# ConsoleApplication
[![ConsoleApp](https://github.com/louisaxel-ambroise/eonix-test/actions/workflows/consoleapplication-dotnet.yml/badge.svg)](https://github.com/louisaxel-ambroise/eonix-test/actions/workflows/consoleapplication-dotnet.yml)

Les classes imposées par l'énoncé ont été respectées: `Singe`, `Spectateur` et `Dresseur`.
D'autres classes ont été ajoutées, telles que `Tour` et `TypeDeTour` (enum) qui permettent de représenter les tours connus par les singes

### Choix techniques

Le projet a été développé en respectant un maximum les principes [SOLID](https://en.wikipedia.org/wiki/SOLID).

Le spectateur n'a pas de réactions prédéfinies à certains types de tour, mais possède un paramètre de contstructeur qui permet de spécifier ses réactions. 
De même les tours connus du `Singe` lui sont passés en paramètre de constructeur, ce qui permet une plus grande réusabilité.

Une interface `IArtiste` a été introduite, ce qui permet de découpler le Spectateur de son implémentation. Cette interface possède un event "Reactions", auquel le spectateur ajoute sa propre méthode de réaction. Ainsi, les implémentations de l'interface artiste n'a pas de dépendence au Spectateur.
Ce découplage rend possible d'ajouter de nouveaux types d'artistes sans avoir à modifier l'implémentation du Spectateur.

L'absence de référence explicite à l'enum `TypeDeTour` aussi bien dans les implementation de d'`IArtiste` et de `Spectateur`  permet d'ajouter ou de retirer des types sans avoir à changer l'implémentation des classes existantes.

Des classes builder (pattern monteur) pour les entités `Singe` et `Spectateur` ont égalemnet été crées pour faciliter la création des instances de ces classes.

### HowTo

```
> dotnet build Eonix.ConsoleApplication\Eonix.ConsoleApplication.csproj
> dotnet run --project Eonix.ConsoleApplication\Eonix.ConsoleApplication.csproj
```


Le projet contient également des tests unitaires:

```
> dotnet test
```