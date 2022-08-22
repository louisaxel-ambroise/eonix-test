# eonix-test

Test de compétences Eonix. L'énoncé se touve à l'adresse suivante: [enonce.pdf](https://raw.githubusercontent.com/louisaxel-ambroise/eonix-test/main/enonce.pdf)

[![ConsoleApp](https://github.com/louisaxel-ambroise/eonix-test/actions/workflows/consoleapplication-dotnet.yml/badge.svg)](https://github.com/louisaxel-ambroise/eonix-test/actions/workflows/consoleapplication-dotnet.yml)
[![API](https://github.com/louisaxel-ambroise/eonix-test/actions/workflows/api-dotnet.yml/badge.svg)](https://github.com/louisaxel-ambroise/eonix-test/actions/workflows/api-dotnet.yml)

## Introduction

Le répertoire estcomposé de 2 dossiers, chacun contenant une solution .NET 6:
  - [ConsoleApplication](https://github.com/louisaxel-ambroise/eonix-test/tree/main/ConsoleApplication): contient l'exercice 1 de l'énoncé
  - [API](https://github.com/louisaxel-ambroise/eonix-test/tree/main/API): contient l'exercice 2 de l'énoncé

## ConsoleApplication

Les classes imposées par l'énoncé ont été respectées: `Singe`, `Spectateur` et `Dresseur`.
D'autres classes ont été ajoutées, telles que `Tour` et `TypeDeTour` (enum) qui permettent de représenter les tours connus par les singes

### Choix techniques

Le projet a été développé en respectant un maximum les principes [SOLID](https://en.wikipedia.org/wiki/SOLID).

Le spectateur n'a pas de réactions prédéfinies à certains types de tour, mais possède un paramètre de contstructeur qui permet de spécifier ses réactions.
De même les tours connus du `Singe` lui sont passés en paramètre de constructeur, ce qui permet une plus grande réusabilité.

Une interface `IArtiste` a été introduite, ce qui permet de découpler le Spectateur de son implémentation. Cette interface possède un event "Reactions", auquel le spectateur ajoute sa propre méthode de réaction. Ainsi, les implémentations de l'interface artiste n'a pas de dépendence au Spectateur.
Ce découplage rend possible l'ajout de nouveaux types d'artistes sans avoir à modifier l'implémentation du Spectateur.

L'absence de référence explicite à l'enum `TypeDeTour` aussi bien dans les implementation de d'`IArtiste` et de `Spectateur`  permet d'ajouter ou de retirer des types sans avoir à changer l'implémentation des classes existantes.

Des classes builder (pattern monteur) pour les entités `Singe` et `Spectateur` ont égalemnet été crées pour faciliter la création des instances de ces classes.

### HowTo

```
> cd ConsoleApplication
> dotnet build Eonix.ConsoleApplication\Eonix.ConsoleApplication.csproj
> dotnet run --project Eonix.ConsoleApplication\Eonix.ConsoleApplication.csproj
```


Le projet contient également des tests unitaires:

```
> cd ConsoleApplication
> dotnet test
```

## API

Ce projet est une API web qui contient des endpoints CRUD qui permettent de gérer des entités `Personne`.
Les données sont conservées dans une base de données SQLite, et la persistence est assurée par EntityFramework Core.

### Choix techniques

La logique de récupération/stockage des données est gérée directement dans le controlleur.
Cette approche a été choisie car cette API est déstinée à être intégrée dans une application basée sur des micro-services. Chaque service devant être minimal et indépendant, la simplicité du modèle et des endpoints ne nécessitait pas une architecture plus complexe *(AMA)*.

Le controlleur accepte un `EonixContext` en paramètre (injecté par DI), et permet de manipuler les entités de la base de données grâce à la librairie EF Core.

L'API respecte l'architecture REST, et propose les endpoints suivants:
- GET: liste les entités/chercheune entité par ID
- POST: ajoute une entité dans la DB
- PUT: mise à jour d'une entité
- DELETE: suppression d'une entité

### Évolution

Si l'API devait venir à évoluer et/ou si le modèle venait à changer (plus d'entités, ajout de relations, etc), il serait à envisager d'utiliser une architecture plus complexe, probablement en utilisant [Mediatr](https://github.com/jbogard/MediatR) et [AutoMapper](https://github.com/AutoMapper/AutoMapper).

L'ajout d'une méthode d'authentification est également requise avant de déployer l'application.

### HowTo

Vérifier que la connectionstring pour la base de données est configurée dans le fichier `appSettings.json` (ou `appSettings.[environnement].json`). Le nom de la connectionString est `PersonnesDatabase`:
```
"ConnectionStrings": {
  "PersonnesDatabase": "Data Source=eonix.dev.db"
}
```

L'API peut ensuite être lancée avec les commandes:

```
> cd API
> dotnet build Eonix.Api\Eonix.Api.csproj
> dotnet run --project Eonix.Api\Eonix.Api.csproj --urls=http://localhost:5002
```

Les migrations EF Core sont appliquées automatiquement à la base de données au démarrage de l'API. Il est également possible de les appliquer manuellement avec la commande:

```
> cd API
> dotnet ef database update --project Eonix.Api\Eonix.Api.csproj
```


La documentation OpenAPI (Swagger) de l'application se trouve à l'URL [http://localhost:5002/swagger/index.html](http://localhost:5002/swagger/index.html)

Le projet contient également des tests unitaires:
```
> cd API
> dotnet test
```
